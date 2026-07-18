using UnityEngine;

// Optional secondary win condition — walking here after all tasks are
// done also triggers the win, same as finishing the last task directly.
public class ExitTrigger : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        bool allDone = true;
        foreach (var t in GameManager.Instance.allTasks)
            if (!(t as ITask).IsComplete) { allDone = false; break; }

        if (allDone)
            GameManager.Instance.TriggerWin();
    }
}