// GameUI.cs — subscribes to GameManager events (Observer pattern listener)
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour {
    public static GameUI Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverReasonText;
    public GameObject winPanel;
    public TextMeshProUGUI exitPromptText;

    void Awake() { Instance = this; }

    void OnEnable() {
        GameManager.OnScoreChanged += _ => { }; // extend later
        GameManager.OnTaskProgressChanged += UpdateTaskUI;
        GameManager.OnGameOverEvent += ShowGameOver;
        GameManager.OnGameWon += ShowWin;
    }
    void OnDisable() {
        GameManager.OnTaskProgressChanged -= UpdateTaskUI;
        GameManager.OnGameOverEvent -= ShowGameOver;
        GameManager.OnGameWon -= ShowWin;
    }

    public void UpdateTimer(float seconds) {
        int m = Mathf.FloorToInt(seconds / 60);
        int s = Mathf.FloorToInt(seconds % 60);
        timerText.text = $"{m:00}:{s:00}";
    }

    void UpdateTaskUI(int completed, int total) {
        taskText.text = $"{completed}/{total}";
    }

    public void UpdateHealthUI(int health) {
        healthText.text = $"HP: {health}";
    }

    public void ShowExitPrompt() {
        if (exitPromptText) exitPromptText.gameObject.SetActive(true);
        exitPromptText.text = "All tasks complete! Reach the EXIT!";
    }

    public void ShowGameOver(string reason) {
        gameOverPanel.SetActive(true);
        gameOverReasonText.text = reason;
    }

    public void ShowWin() {
        winPanel.SetActive(true);
    }
}