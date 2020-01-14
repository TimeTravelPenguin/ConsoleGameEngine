using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ConsoleGameEngine;
using ConsoleGameEngine.Enums;

namespace DemoConsoleGame
{
  internal class GameShell : CgeGameShell
  {
    private readonly Stopwatch _fpsStopwatch;
    private Direction _direction = Direction.None;
    private Maze _maze;

    public GameShell(ConsoleColor bgColor = ConsoleColor.Black, bool clearScreen = true)
      : base(bgColor, clearScreen)
    {
      _fpsStopwatch = Stopwatch.StartNew();
      InitialiseBackgroundFpsTracker();

      SetController(ReadKeyboardInput);
    }

    private void ReadKeyboardInput()
    {
      var key = Console.ReadKey(true).Key;

      if (key == ConsoleKey.E)
      {
        StopGame();
      }

      _direction = key switch
      {
        ConsoleKey.UpArrow => Direction.Up,
        ConsoleKey.DownArrow => Direction.Down,
        ConsoleKey.LeftArrow => Direction.Left,
        ConsoleKey.RightArrow => Direction.Right,
        _ => Direction.None
      };
    }

    private void InitialiseBackgroundFpsTracker()
    {
      BackgroundAction.Start(1000, () =>
      {
        _fpsStopwatch.Stop();
        Console.Title = "FPS: " + Math.Round(GameLoops / _fpsStopwatch.Elapsed.TotalSeconds);
        _fpsStopwatch.Restart();
        GameLoops = 0;
      });
    }

    protected override void OnStart()
    {
      _maze = Maze.CreateMaze(60, 30);
    }

    protected override bool OnUpdate()
    {
      var loop = !_maze.DrawMaze();
      Task.Delay(1).Wait();


      return loop;
    }

    protected override void OnFinish()
    {
      _maze.HighlightPath();

      Console.ReadKey(true);

      Dispose();

      Console.Title = "Application finished";
      Console.Clear();

      Console.SetCursorPosition(0, 0);
      Console.ForegroundColor = ConsoleColor.White;

      Console.WriteLine(Environment.NewLine + "Application has finished... Press any key to Exit.");
    }
  }
}