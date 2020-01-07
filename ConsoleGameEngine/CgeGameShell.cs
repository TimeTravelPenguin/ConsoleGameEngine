using System;

namespace ConsoleGameEngine
{
  public abstract class CgeGameShell
  {
    protected readonly BackgroundTimer BackgroundTimer;

    protected double GameLoops;

    protected CgeGameShell()
    {
      Console.CursorVisible = false;
      BackgroundTimer = new BackgroundTimer();
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

      BackgroundTimer.Stop();
    }

    protected abstract void OnStart();

    protected abstract bool OnUpdate();

    protected abstract void OnFinish();
  }
}