using UnityEngine;
using UnityEngine.Events;

/// <summary>Component that invokes a UnityAction(OnTick) after a set TickTime. Can be set to repeat.  
/// TickTime can be overriden by objects inheriting the TickTimerOverrider interface
/// </summary>
public class TimerComponent : MonoBehaviour
{
    public bool Repeating = true;
    public UnityAction OnTimerTick;
    [SerializeField]
    private float tickIntervalTime = 5f;
    private float lastTickTime;

    private ITimerTickOverrider timerTickOverrider;
    public float TickIntervalTime
    {
        get => timerTickOverrider == null ? tickIntervalTime : timerTickOverrider.TickIntervalTime;
        set => tickIntervalTime = value;
    }

    public ITimerTickOverrider TimerTickOverrider
    {
        set => timerTickOverrider = value;
    }

    void Update()
    {
        if (TickIntervalTime == 0)
        {
            Debug.LogWarning("TickTime is set to 0. onTimerTick fires every frame");
        }
        if (Time.time > lastTickTime + TickIntervalTime)
        {
            OnTimerTick?.Invoke();
            if (Repeating)
                lastTickTime = Time.time;
            else
                enabled = false;
        }
    }
}
public static class TimerUtility
{
    /// <summary>Adds a timercomponent to the gameobject calling the function, with the input arguments
    /// </summary>
    public static TimerComponent AddTimerComponent(this GameObject gameObject, float tickInterval = 5f, bool repeating = true)
    {
        var addedComp = gameObject.AddComponent<TimerComponent>();
        addedComp.TickIntervalTime = tickInterval;
        addedComp.Repeating = repeating;
        return addedComp;
    }
    /// <summary>Adds a timercomponent to the gameobject calling the function, which ticks at an interval set by the timerTickOverrider
    /// </summary>
    public static TimerComponent AddTimerComponentOverriden(this GameObject gameObject, ITimerTickOverrider timerTickOverrider, bool repeating = true)
    {
        var addedComp = gameObject.AddComponent<TimerComponent>();
        addedComp.TimerTickOverrider = timerTickOverrider;
        addedComp.Repeating = repeating;
        return addedComp;
    }
}
/// <summary>Interface that allows a component to override the Ticktime of the timer
/// </summary>
public interface ITimerTickOverrider
{
    /// <summary> sets the tickInterval time for the overriden TimerComponent
    /// </summary>
    float TickIntervalTime
    {
        get;
    }
}
