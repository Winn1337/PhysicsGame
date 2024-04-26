using UnityEngine;

/// <summary>
/// Singleton timer
/// </summary>
public class Timer
{
    private static Timer instance;
    private Timer()
    {
        Reset();
    }

    public static Timer Instance()
    {
        if (instance == null) instance = new Timer();

        return instance;
    }

    private bool running;

    //time is:
    //if running, when the timer started
    //else, what the timer stopped at
    private float time;

    public void Start()
    {
        time = Time.time;
        running = true;
    }

    public void Stop()
    {
        time = Get();
        running = false;
    }

    public float Get()
    {
        if (running)
            return Time.time - time;
        else
            return time;
    }

    public void Reset()
    {
        running = false;
        time = 0f;
    }
}
