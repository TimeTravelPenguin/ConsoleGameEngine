using System;

namespace CoreGameEngine.EngineActions
{
  public interface IEngineAction
  {
    bool CanExecute { get; set; }
    void SetAction(Action action);
    void Execute();
  }

  public interface IEngineAction<T>
  {
    bool CanExecute { get; set; }
    void SetAction(Func<T> action);
    T Execute();
  }
}