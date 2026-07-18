// PATTERN: Singleton — one global instance accessible from any script
// PATTERN: Observer — events notify UI and tasks when state changes
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public static event System.Action<int> OnScoreChanged;
    public static event System.Action<int, int> OnTaskProgressChanged;
    public static event System.Action<string> OnGameOverEvent;
    public static event System.Action OnGameWon;
    public static event System.Action<PlayerWeaponType> OnWeaponChanged;

    [Header("References")]
    public KillEnemyTask killTask;
    public List<MonoBehaviour> allTasks;

    [Header("Timer")]
    public float timeLimit = 180f;
    float timeRemaining;
    bool gameRunning = false;

    int score = 0;
    int tasksCompleted = 0;
    int totalTasks;
    int enemyCount = 0;
    int initialEnemyCount = 0;
    public static int InitialEnemyCount => Instance.initialEnemyCount;

    void Awake() {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start() {
        totalTasks = allTasks.Count;
        timeRemaining = timeLimit;
        gameRunning = true;

        foreach (var t in allTasks)
            (t as ITask)?.OnTaskStart();
    }

    void Update() {
        if (!gameRunning) {
            if (Input.GetKeyDown(KeyCode.R))
                RestartLevel();
            if (Input.GetKeyDown(KeyCode.Q))
                QuitGame();
            return;
        }
        timeRemaining -= Time.deltaTime;
        GameUI.Instance?.UpdateTimer(timeRemaining);
        if (timeRemaining <= 0)
            TriggerGameOver("Time's up!");
    }

    public void QuitGame() {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Called by NPC_Enemy when enemies spawn/die
    public static void AddToEnemyCount() {
        Instance.enemyCount++;
        Instance.initialEnemyCount++;
    }
    public static void RemoveEnemy() {
        Instance.enemyCount--;
        if (Instance.enemyCount <= 0)
            Instance.killTask?.CompleteTask();
    }
    public static void AddScore(int amount) {
        Instance.score += amount;
        OnScoreChanged?.Invoke(Instance.score);
    }

    // Called by PlayerBehavior
    public static void RegisterPlayerDeath() {
        Instance.TriggerGameOver("You were killed!");
    }
    public static void SelectWeapon(PlayerWeaponType weaponType) {
        OnWeaponChanged?.Invoke(weaponType);
    }
    public void UpdateHealthUI(int health) {
        GameUI.Instance?.UpdateHealthUI(health);
    }

    // Called by task scripts when they complete
    public void OnTaskCompleted() {
        tasksCompleted++;
        OnTaskProgressChanged?.Invoke(tasksCompleted, totalTasks);
        if (tasksCompleted >= totalTasks)
            TriggerWin();
    }

    public void TriggerWin() {
        gameRunning = false;
        OnGameWon?.Invoke();
        GameUI.Instance?.ShowWin();
    }

    public void TriggerGameOver(string reason) {
        if (!gameRunning) return;
        gameRunning = false;
        OnGameOverEvent?.Invoke(reason);
        GameUI.Instance?.ShowGameOver(reason);
    }

    public void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}