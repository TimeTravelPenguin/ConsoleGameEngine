using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleGameEngine.Enums;
using ConsoleGameEngine.Tiles;

namespace ConsoleGameEngine.Extensions
{
  public static class TileExtensions
  {
    public static T GetNeighborTile<T>(this T tile, Direction direction) where T : Tile<T>
    {
      if (tile is null)
      {
        throw new NullReferenceException();
      }

      var newTile = direction switch
      {
        Direction.Up => tile.TileUp,
        Direction.Down => tile.TileDown,
        Direction.Left => tile.TileLeft,
        Direction.Right => tile.TileRight,
        Direction.None => tile,
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
      };

      return newTile ?? tile;
    }

    public static T[] GetNeighborTiles<T>(this T tile) where T : Tile<T>
    {
      if (tile is null)
      {
        throw new NullReferenceException();
      }

      return new[] {Direction.Up, Direction.Down, Direction.Left, Direction.Right}
        .Select(tile.GetNeighborTile)
        .Where(neighborTile => !(neighborTile is null))
        .ToArray();
    }

    public static Stack<T> MapTraversal<T>(this T tile, T endTile, Stack<T> stack = null) where T : Tile<T>
    {
      if (stack is null)
      {
        stack = new Stack<T>();
      }

      if (!(tile.TileUp is null))
      {
        var s = tile.TileUp.MapTraversal(endTile, stack);
        if (s.Contains(endTile))
        {
          return s;
        }
      }

      if (!(tile.TileDown is null))
      {
        var s = tile.TileDown.MapTraversal(endTile, stack);
        if (s.Contains(endTile))
        {
          return s;
        }
      }

      if (!(tile.TileLeft is null))
      {
        var s = tile.TileLeft.MapTraversal(endTile, stack);
        if (s.Contains(endTile))
        {
          return s;
        }
      }

      if (!(tile.TileRight is null))
      {
        var s = tile.TileRight.MapTraversal(endTile, stack);
        if (s.Contains(endTile))
        {
          return s;
        }
      }

      return stack;
    }
  }
}