public static class EventManager
{
    public delegate void EventHandler();
    public static event EventHandler OnTrainStarted;
    public static event EventHandler OnTrainStopped;
    public static event EventHandler OnTrainPassedNextRail;
    public static event EventHandler OnTrainCrashed;


    public static void TrainStarted()
    {
        OnTrainStarted?.Invoke();
    }

    public static void TrainStopped()
    {
        OnTrainStopped?.Invoke();
    }

    public static void TrainPassedNextRail()
    {
        OnTrainPassedNextRail?.Invoke();
    }

    public static void TrainCrashed()
    {
        OnTrainCrashed?.Invoke();
    }
}