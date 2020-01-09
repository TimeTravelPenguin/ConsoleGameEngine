using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleGameEngine.Enums;
using ConsoleGameEngine.Extensions;
using ConsoleGameEngine.Helpers;
using ConsoleGameEngine.Tiles;

namespace DemoConsoleGame
{
  internal class Maze
  {
    private readonly Stack<MyTile> _path;
    private readonly MyTile[,] _tiles;
    private readonly Direction[] _validDirections = {Direction.Up, Direction.Down, Direction.Left, Direction.Right};
    private MyTile _currentTile;
    private Direction _lastMovement = Direction.None;
    private MyTile _lastTile;

    private Maze(int width, int height)
    {
      _tiles = TileMesh.NewTileMesh<MyTile>(width, height);
      _path = new Stack<MyTile>();

      InitialiseStart();
    }

    public static Maze CreateMaze(int width, int height)
    {
      return new Maze(width, height);
    }

    private void InitialiseStart()
    {
      _currentTile = _tiles.GetTile(0);
      _currentTile.Traversed = true;

      _path.Push(_currentTile);

      DrawMaze();
    }

    public bool DrawMaze()
    {
      _lastTile = _currentTile;

      if (IsDeadEnd(_currentTile))
      {
        _currentTile = _path.Pop();
      }
      else
      {
        MyTile randomTile;
        (_lastMovement, randomTile) = GetRandomNeighbor();
        _currentTile.AddTile(randomTile, _lastMovement);
        _currentTile = TraverseTile(randomTile);
        _path.Push(_currentTile);
      }

      _lastTile.Color = ConsoleColor.White;
      _lastTile.DrawTile();

      _currentTile.Color = ConsoleColor.Red;
      _currentTile.DrawTile();

      return MazeComplete();
    }

    private (Direction, MyTile) GetRandomNeighbor()
    {
      var tiles = _validDirections.Select(x => (x, _currentTile.GetNeighborTile(x)))
        .Where(x => x.Item2 != _currentTile)
        .Where(x => x.Item2.Traversed == false);
      return tiles.RandomIn();
    }

    private bool IsDeadEnd(MyTile currentTile)
    {
      var validDirections = new List<Direction> {Direction.Up, Direction.Down, Direction.Left, Direction.Right};

      return validDirections
        .SelectMany(validDirection => currentTile.GetNeighborTiles())
        .All(neighborTile => neighborTile.Traversed);
    }

    private MyTile TraverseTile(MyTile newTile)
    {
      if (newTile.Traversed)
      {
        throw new ArgumentException();
      }

      newTile.Traversed = true;

      return newTile;
    }

    private bool MazeComplete()
    {
      return _tiles.Cast<MyTile>().All(tile => tile.Traversed);
    }

    public void HighlightPath()
    {
      // Reset colors
      foreach (var myTile in _tiles)
      {
        if (myTile.Color != ConsoleColor.White)
        {
          myTile.Color = ConsoleColor.White;
          myTile.DrawTile();
        }
      }

      // Create solution path
      var start = _tiles[0, 0];
      var end = _tiles.GetTile(_tiles.Length - 1);

      var path = start.MapTraversal(end);

      foreach (var tile in path)
      {
        tile.Color = ConsoleColor.Red;
        tile.DrawTile();
      }
    }
  }
}