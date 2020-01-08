using System;
using ConsoleGameEngine.Enums;
using ConsoleGameEngine.Tiles;

namespace ConsoleGameEngine.Extensions
{
  public static class TileExtensions
  {
    public static Tile GetNeighborTile(this Tile tile, Direction direction)
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
  }
}