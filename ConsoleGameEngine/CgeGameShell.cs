namespace ConsoleGameEngine
{
  public abstract class CgeGameShell
  {
    protected readonly BackgroundTimer BgTimer;

    protected double GameLoops;

    protected CgeGameShell()
    {
      BgTimer = new BackgroundTimer();
    }

    public void Start()
    {
      OnStart();

      bool continueGame;
      do
      {
        continueGame = OnUpdate();
        GameLoops++;
      } while (continueGame);

      OnFinish();

      BgTimer.Stop();
    }

    protected abstract void OnStart();

    protected abstract bool OnUpdate();

    protected abstract void OnFinish();
  }
}