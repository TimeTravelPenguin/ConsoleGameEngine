#region Title Header
// Name: Phillip Smith
// 
// Solution: DemoConsoleGame.Maze
// Project: CoreGameEngine
// File Name: IEngineTask.cs
// 
// Current Data:
// 2020-01-16 12:02 AM
// 
// Creation Date:
// -- 
#endregion
namespace CoreGameEngine.EngineOperator
{
  public interface IEngineTask<T>
  {
    void Execute(object param = null);
  }
}