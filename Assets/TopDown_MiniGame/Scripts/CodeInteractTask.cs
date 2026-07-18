using UnityEngine;
using TMPro;

// Player presses E near the terminal to open a code-entry panel. The correct
// code is the total number of enemies placed in the level — the player has
// to count them across the rooms while playing to figure it out.
public class CodeInteractTask : MonoBehaviour, ITask {
    [SerializeField] string taskName = "Crack the Terminal Code";
    bool isComplete = false;
    bool playerNearby = false;

    [Header("UI")]
    public GameObject codePanel;
    public TMP_InputField codeInput;
    public TextMeshProUGUI hintText;
    public TextMeshProUGUI feedbackText;

    public string TaskName => taskName;
    public bool IsComplete => isComplete;
    public void OnTaskStart() {
        if (codePanel) codePanel.SetActive(false);
        if (hintText) hintText.text = "Hint: count the enemies across every room.";
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) playerNearby = true;
    }
    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) playerNearby = true;
    }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) playerNearby = false;
    }

    void Update() {
        if (playerNearby && !isComplete && Input.GetKeyDown(KeyCode.E) && !codePanel.activeSelf) {
            OpenPanel();
        }
    }

    void OpenPanel() {
        codePanel.SetActive(true);
        codeInput.text = "";
        if (feedbackText) feedbackText.text = "";
        codeInput.Select();
        codeInput.ActivateInputField();
    }

    // Hooked to the panel's Submit button
    public void SubmitCode() {
        int enteredCode;
        if (int.TryParse(codeInput.text, out enteredCode) && enteredCode == GameManager.InitialEnemyCount) {
            isComplete = true;
            codePanel.SetActive(false);
            GameManager.Instance.OnTaskCompleted();
        } else {
            if (feedbackText) feedbackText.text = "Incorrect code. Try again.";
        }
    }

    // Hooked to the panel's Exit button
    public void ClosePanel() {
        codePanel.SetActive(false);
    }
}