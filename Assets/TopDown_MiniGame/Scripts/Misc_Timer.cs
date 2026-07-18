// Misc_Timer.cs
// Encapsulation: wraps timer state so external scripts can't accidentally
// modify _startTime or _duration directly
public class Misc_Timer {
    float _startTime, _duration;
    bool _running;

    public void StartTimer(float duration) {
        _duration = duration;
        _startTime = UnityEngine.Time.time;
        _running = true;
    }
    public void UpdateTimer() { } // kept for API compatibility — Time.time is automatic
    public void StopTimer() { _running = false; }
    public bool IsFinished() => _running && UnityEngine.Time.time >= _startTime + _duration;

    // True while the timer is running and hasn't finished yet
    // (used by GameCamera for the screen-shake duration check)
    public bool IsActive() => _running && UnityEngine.Time.time < _startTime + _duration;
}