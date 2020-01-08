using System;
using ConsoleGameEngine.Sprites;

namespace ConsoleGameEngine.Tiles
{
  public static class TileMesh
  {
    public static Tile[,] NewTileMesh(int width, int height)
    {
      var tiles2d = new Tile[height, width];

      // Create objects
      for (var i = 0; i < height * width; i++)
      {
        tiles2d[i / width, i % width] = new Tile
        {
          Value = Characters.Square.ToString(),
          Color = ConsoleColor.White,
          Pos = new Point(2* (i % width), i / width)
        };
      }

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
        if (i < height * (width - 1))
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

      return tiles2d;
    }
  }
}