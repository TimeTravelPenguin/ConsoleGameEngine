using System;
using ConsoleGameEngine.Tiles;

namespace DemoConsoleGame
{
  internal static class Program
  {
    private static GameShell _gameShell;

    private static void Main()
    {
      _gameShell = new GameShell();
      _gameShell.Start();

      Console.ReadKey(true);
    }
  }
}