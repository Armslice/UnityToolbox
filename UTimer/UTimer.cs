using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UTimer : MonoBehaviour
{
    [SerializeField] private float runTime = 1;
    [SerializeField]
    [Tooltip("The frequency at which the timer decrements")]
    private float interval = .1f;
    [SerializeField] private bool autoStart = false;
    [Tooltip("This timer will self destruct upon completion")]
    [SerializeField] private bool destroyOnFinish = false;
    [Tooltip("Loads the objects/methods to execute at the moment the timer is finished ")]
    [SerializeField] private UnityEvent events;
    private bool running = false;
    public bool isRunning
    {
        get { return running; }
    }
    
    
    private Coroutine routine;
    public float timeLeft { get; private set; }


    /// <summary>
    /// Set up the running time to the start.
    /// Call run is autoStart is enabled.
    /// </summary>
    void Start()
    {
        timeLeft = runTime;
        if (autoStart)
        {
            Run();
        }
    }

    /// <summary>
    /// Begins execution. Call this manually if autoStart is disabled.
    /// Also use to resume a paused timer.
    /// </summary>
    public void Run()
    {
        running = true;
        routine = StartCoroutine(runTimer());
    }

/// <summary>
/// The coroutine that yeild every interval and decrements the time.
/// A delta is calculated becuase the actual time between calls is not
/// exactly the same at the interval. Once timeLeft <= 0, invoke events.
/// </summary>
/// <returns>IEnumerator for next execution</returns>
    private IEnumerator runTimer()
    {
        float delta;
        float lastTime;
        while (timeLeft > 0)
        {
            lastTime = Time.time;
            yield return new WaitForSeconds(Mathf.Min(interval,timeLeft));
            delta = Time.time - lastTime;
            timeLeft -= delta;
        }
        running = false;
        events.Invoke();
    }
    /// <summary>
    /// Returns the timer to RunTime (starting time)
    /// </summary>
    public void Reset()
    {
        Reset(false);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pause">If true, pauses the timer upon reset</param>
    public void Reset(bool pause)
    {
        Pause();
        timeLeft = runTime;
        if (!pause)
        {
            Run();
        }
        
    }

    public void Pause()
    {
        if (running)
        {
            StopCoroutine(routine);
            running = false;
        }
        
    }

    public void ResetAndPause()
    {
        Reset(true);
    }

    override public string ToString()
    {
        return gameObject.name + " - time left: " + timeLeft;
    }

}
