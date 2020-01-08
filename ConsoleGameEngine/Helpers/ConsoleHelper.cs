using System;
using ConsoleGameEngine.Sprites;

namespace ConsoleGameEngine.Helpers
{
  public static class ConsoleHelper
  {
    public static void Draw(int x, int y, char character = Characters.Blank)
    {
      Console.SetCursorPosition(x, y);
      Console.Write(character);
    }
  }
}