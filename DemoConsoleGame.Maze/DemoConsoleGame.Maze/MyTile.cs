using System;
using ConsoleGameEngine.Enums;
using ConsoleGameEngine.Tiles;

namespace DemoConsoleGame
{
  internal class MyTile : Tile<MyTile>
  {
    public bool Traversed { get; set; }

    public void Update(MyTile tile)
    {
      Value = tile.Value;
      Color = tile.Color;
      Pos = tile.Pos;
    }

    public void AddTile(MyTile tile, Direction direction)
    {
      switch (direction)
      {
        case Direction.Up:
          TileUp.Update(tile);
          break;
        case Direction.Down:
          TileDown.Update(tile);
          break;
        case Direction.Left:
          TileLeft.Update(tile);
          break;
        case Direction.Right:
          TileRight.Update(tile);
          break;
        default:
          throw new ArgumentException();
      }
    }
  }
}