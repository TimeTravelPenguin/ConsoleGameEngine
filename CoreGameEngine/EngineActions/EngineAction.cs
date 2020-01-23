using System;
using System.Globalization;
using CoreGameEngine.Resources;

namespace CoreGameEngine.EngineActions
{
  public abstract class EngineAction : IEngineAction
  {
    private Action _action;
    public bool CanExecute { get; set; }

    public void SetAction(Action action)
    {
      _action = action;
      CanExecute = true;
    }

    public void Execute()
    {
      if (_action is null)
      {
        throw new NullReferenceException(string.Format(CultureInfo.InvariantCulture, Exceptions.ParameterIsNull,
          nameof(_action)));
      }

      _action.Invoke();
    }
  }

  public abstract class EngineAction<T> : IEngineAction<T>
  {
    private Func<T> _action;
    public bool CanExecute { get; set; }

    public void SetAction(Func<T> action)
    {
      _action = action;
      CanExecute = true;
    }

    public T Execute()
    {
      if (_action is null)
      {
        throw new NullReferenceException(string.Format(CultureInfo.InvariantCulture, Exceptions.ParameterIsNull,
          nameof(_action)));
      }

      return _action.Invoke();
    }
  }
}