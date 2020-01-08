#region Title Header
// Name: Phillip Smith
// 
// Solution: RPGConsoleGame
// Project: ConsoleGameEngine
// File Name: ISprite.cs
// 
// Current Data:
// 2020-01-08 11:39 AM
// 
// Creation Date:
// -- 
#endregion

using System;

namespace ConsoleGameEngine.Sprites
{
  public interface ISprite
  {
    int Height { get; set; }
    int Width { get; set; }
    Glyph[,] Glyphs { get; set; }
    ConsoleColor Color { get; set; }
    Point Pos { get; set; }
    void UpdatePos(int xPos, int yPos);
  }
}