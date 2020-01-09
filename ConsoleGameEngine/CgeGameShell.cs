using System;

namespace ConsoleGameEngine
{
  public abstract class CgeGameShell
  {
    private readonly BackgroundAction _inputThread;
    protected readonly BackgroundAction BackgroundAction;
    private bool _continueGame;
    private bool _stopGame;
    protected double GameLoops;

    protected CgeGameShell(ConsoleColor bgColor = ConsoleColor.Black, bool clearScreen = true)
    {
      Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
      Console.CursorVisible = false;

      GameConfiguration.BackgroundColor = bgColor;

      if (clearScreen)
      {
        Console.Clear();
      }

      BackgroundAction = new BackgroundAction();
      _inputThread = new BackgroundAction();
    }

    protected void Dispose()
    {
      BackgroundAction?.Dispose();
    }

    protected void SetController(Action controllerAction)
    {
      _inputThread.Start(1, controllerAction);
    }

    public void Start()
    {
      if (BackgroundAction is null)
      {
        throw new NullReferenceException();
      }

      OnStart();

      do
      {
        _continueGame = !_stopGame && OnUpdate();
        GameLoops++;
      } while (_continueGame);

      OnFinish();
    }

    protected void StopGame()
    {
      _stopGame = true;
    }

    protected abstract void OnStart();

    protected abstract bool OnUpdate();

    protected abstract void OnFinish();
  }
}