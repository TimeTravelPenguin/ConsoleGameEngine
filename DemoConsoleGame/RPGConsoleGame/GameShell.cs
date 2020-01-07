using System;
using ConsoleGameEngine;
using ConsoleGameEngine.Draw;
using ConsoleGameEngine.Helpers;

namespace DemoConsoleGame
{
  internal class GameShell : CgeGameShell
  {
    private int _frameCount;

    public GameShell()
    {
      InitialiseBackgroundFpsTracker();
    }

    private void InitialiseBackgroundFpsTracker()
    {
      BackgroundTimer.Start(500, () =>
      {
        Console.Title = "FPS: " + Math.Round(GameLoops / BackgroundTimer.Elapsed * 1000);
        GameLoops = 0;
      });
    }

    protected override void OnStart()
    {
      _frameCount = 0;
      Console.WriteLine("Starting...");
    }

    Sprite s = Sprite.NewRectangle(2, 4, Point.Zero);
    protected override bool OnUpdate()
    {
      //const int loopTotal = 300000;
      //if (_frameCount > loopTotal)
      //{
      //  return false;
      //}

      //Console.WriteLine($"Loop: {_frameCount} ({Math.Round((double) _frameCount++ / loopTotal * 100)}%)");

      s.UpdatePos(RandomHelper.Rand(0,20), RandomHelper.Rand(0,20));

      var now = DateTime.Now;
      while (DateTime.Now-now < TimeSpan.FromMilliseconds(1000))
      {
        // delay
      }

      return true;
    }

    protected override void OnFinish()
    {
      Console.WriteLine(Environment.NewLine + "Application has finished... Press any key to Exit.");
      Console.ReadKey(true);
    }
  }
}