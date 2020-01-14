using System;

namespace DemoConsoleGame
{
  internal static class Program
  {
    private static GameShell _gameShell;

    private static void Main()
    {
      Console.Title = "Maze Generating Algorithm";
      Console.WriteLine("Press to start...");
      Console.ReadKey(true);

      _gameShell = new GameShell();
      _gameShell.Start();

      Console.ReadKey(true);
    }
  }
}