#region Title Header

// Name: Phillip Smith
// 
// Solution: DemoConsoleGame.Maze
// Project: CoreGameEngine
// File Name: IFactory.cs
// 
// Current Data:
// 2020-01-16 1:08 AM
// 
// Creation Date:
// -- 

#endregion

using System;

namespace CoreGameEngine.Factory
{
  public interface IFactory<in TKey, TValue>
  {
    void Register(TKey key, Func<TValue> value);
    TValue Create(TKey key);
    bool ContainsKey(TKey key);
  }
}