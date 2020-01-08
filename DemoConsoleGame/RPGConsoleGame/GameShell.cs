using System;
using System.Diagnostics;
using ConsoleGameEngine;
using ConsoleGameEngine.Enums;
using ConsoleGameEngine.Extensions;
using ConsoleGameEngine.Tiles;

namespace DemoConsoleGame
{
  internal class GameShell : CgeGameShell
  {
    private readonly Stopwatch _fpsStopwatch;
    private readonly Tile[,] _tiles = TileMesh.NewTileMesh(12, 12);
    private Tile _currentTile;
    private Direction _direction = Direction.None;

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
      _currentTile = _tiles[0, 0];
      _currentTile.Color = ConsoleColor.Blue;

      for (var y = 0; y < _tiles.GetLength(0); y++)
      {
        for (var x = 0; x < _tiles.GetLength(1); x++)
        {
          var tile = _tiles[y, x];
          tile.DrawTile();
        }
      }
    }

    protected override bool OnUpdate()
    {
      if (_direction != Direction.None)
      {
        _currentTile.Color = ConsoleColor.White;
        _currentTile.DrawTile();

        _currentTile = _currentTile.GetNeighborTile(_direction);
        _currentTile.Color = ConsoleColor.Blue;
        _currentTile.DrawTile();

        _direction = Direction.None;
      }

      return true;
    }

    protected override void OnFinish()
    {
      Dispose();

      Console.Title = "Application finished";
      Console.Clear();

      Console.SetCursorPosition(0, 0);
      Console.ForegroundColor = ConsoleColor.White;

      Console.WriteLine(Environment.NewLine + "Application has finished... Press any key to Exit.");
    }
  }
}