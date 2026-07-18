// ITask.cs
// Abstraction: defines WHAT a task must do, not HOW
// Polymorphism: TaskManager can hold List<ITask> and call IsComplete() on any type
public interface ITask {
    string TaskName { get; }
    bool IsComplete { get; }
    void OnTaskStart();   // called when game begins
}