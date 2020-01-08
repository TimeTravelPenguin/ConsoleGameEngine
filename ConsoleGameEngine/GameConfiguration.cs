using System;

namespace ConsoleGameEngine
{
  public static class GameConfiguration
  {
    private static ConsoleColor _backgroundColor = ConsoleColor.Black;

    public static ConsoleColor BackgroundColor
    {
      get => _backgroundColor;
      set
      {
        _backgroundColor = value;
        Console.BackgroundColor = _backgroundColor;
      }
    }
  }
}