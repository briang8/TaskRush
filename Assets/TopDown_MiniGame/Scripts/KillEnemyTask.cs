// KillEnemyTask.cs — completes when GameManager signals the tracked enemy is dead
// Since NPC_Enemy.Damage() already calls GameManager.RemoveEnemy() on death,
// this task is completed from GameManager rather than watching the enemy directly.
using UnityEngine;

public class KillEnemyTask : MonoBehaviour, ITask {
    [SerializeField] string taskName = "Eliminate the Guard";
    bool isComplete = false;

    public string TaskName => taskName;
    public bool IsComplete => isComplete;
    public void OnTaskStart() { isComplete = false; }

    // Called by GameManager.RemoveEnemy() when the tracked enemy dies
    public void CompleteTask() {
        if (!isComplete) {
            isComplete = true;
            GameManager.Instance.OnTaskCompleted();
        }
    }
}