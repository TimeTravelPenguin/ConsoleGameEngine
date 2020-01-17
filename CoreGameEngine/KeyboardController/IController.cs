#region Title Header

// Name: Phillip Smith
// 
// Solution: DemoConsoleGame.Maze
// Project: CoreGameEngine
// File Name: IController.cs
// 
// Current Data:
// 2020-01-16 2:21 AM
// 
// Creation Date:
// -- 

#endregion

using System;

namespace CoreGameEngine.KeyboardController
{
  public interface IController : IDisposable
  {
    void AddControl(ConsoleKey consoleKey, Func<Action> action);
    void RunController();
    void StopController();
  }
}