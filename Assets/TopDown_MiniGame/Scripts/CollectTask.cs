// CollectTask.cs
// Inheritance via interface: implements ITask
// Player walks into a trigger collider to complete this task
using UnityEngine;

public class CollectTask : MonoBehaviour, ITask {
    [SerializeField] string taskName = "Collect Item";
    bool isComplete = false;

    public string TaskName => taskName;
    public bool IsComplete => isComplete;

    public void OnTaskStart() { gameObject.SetActive(true); }

    // Encapsulation: completion state only changes through this trigger
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !isComplete) {
            isComplete = true;
            gameObject.SetActive(false); // hide item
            GameManager.Instance.OnTaskCompleted();
        }
    }
}