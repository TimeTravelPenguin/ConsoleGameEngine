using System;
using ConsoleGameEngine.Sprites;

namespace ConsoleGameEngine.Tiles
{
  public static class TileMesh
  {
    public static T[,] NewTileMesh<T>(int width, int height) where T : Tile<T>, new()
    {
      var tiles2d = new T[height, width];

      // Create objects
      for (var i = 0; i < height * width; i++)
      {
        tiles2d[i / width, i % width] = new T
        {
          Value = Characters.Square,
          Color = ConsoleColor.White,
          Pos = new Point(2 * (i % width), i / width)
        };
      }

      WireMesh(tiles2d, width, height);
      
      return tiles2d;
    }

    private static void WireMesh<T>(T[,] tiles2d, int width, int height) where T : Tile<T>, new()
    {
      // Assign neighbors
      for (var i = 0; i < height * width; i++)
      {
        var tile = tiles2d[i / width, i % width];
        // Assign Up
        if (i > width)
        {
          tile.TileUp = tiles2d[i / width - 1, i % width];
        }

        // Assign Down
        if (i < width * (height - 1))
        {
          tile.TileDown = tiles2d[i / width + 1, i % width];
        }

        // Assign Left
        if (i % width != 0)
        {
          tile.TileLeft = tiles2d[i / width, i % width - 1];
        }

        // Assign Right
        if (i % width != width - 1)
        {
          tile.TileRight = tiles2d[i / width, i % width + 1];
        }
      }
    }

    public static T GetTile<T>(this T[,] tiles, int value) where T : Tile<T>
    {
      if (value >= tiles.Length)
      {
        throw new ArgumentOutOfRangeException(nameof(value), value, null);
      }

      var width = tiles.GetLength(1);

      var tile = tiles[value / width, value % width];

      return tile;
    }
  }
}