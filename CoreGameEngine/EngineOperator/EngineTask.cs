using System;
using CoreGameEngine.Resources;

namespace CoreGameEngine.EngineOperator
{
  public class EngineTask<T> : IEngineTask<T>
  {
    private readonly Action _action;
    private readonly Action<T> _paramAction;
    private readonly bool _requiresParam;

    public void Execute(object param = null)
    {
      if (_requiresParam)
      {
        if (!(param is T obj))
        {
          throw new ArgumentException(Exceptions.Parameter_IncorrectType, nameof(param));
        }

        _paramAction.Invoke(obj);
      }
      else
      {
        _action.Invoke();
      }
    }

    public EngineTask(Action action)
    {
      _action = action;
      _requiresParam = false;
    }

    public EngineTask(Action<T> action)
    {
      _paramAction = action;
      _requiresParam = true;
    }
  }
}