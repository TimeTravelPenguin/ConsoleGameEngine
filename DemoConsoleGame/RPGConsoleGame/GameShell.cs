using System;
using ConsoleGameEngine;

namespace DemoConsoleGame
{
  internal class GameShell : CgeGameShell
  {
    private int frameCount;

    public GameShell()
    {
      InitialiseBackgroundFpsTracker();
    }

    private void InitialiseBackgroundFpsTracker()
    {
      BgTimer.Start(500, () =>
      {
        Console.Title = "FPS: " + Math.Round(GameLoops / BgTimer.Elapsed * 1000);
        GameLoops = 0;
      });
    }

    protected override void OnStart()
    {
      frameCount = 0;
      Console.WriteLine("Starting...");
    }

    protected override bool OnUpdate()
    {
      const int loopTotal = 300000;
      if (frameCount > loopTotal)
      {
        return false;
      }

      Console.WriteLine($"Loop: {frameCount} ({Math.Round((double) frameCount++ / loopTotal * 100)}%)");

      return true;
    }

    protected override void OnFinish()
    {
      Console.WriteLine(Environment.NewLine + "Application has finished... Press any key to Exit.");
      Console.ReadKey(true);
    }
  }
}