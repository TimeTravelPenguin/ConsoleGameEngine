using System;
using CoreGameEngine.EngineOperator;

namespace CoreGameEngine
{
  public class CoreEngine : IDisposable
  {
    private Action _onFinish;
    private Action _onStart;
    private Func<bool> _onUpdate;
    private BackgroundAction _trackKeyboard;

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _trackKeyboard?.Dispose();
      }
    }

    public void SetOnStart<T>(IOperator<T> operation, object param = null)
    {
      _onStart = () =>
      {
        if (operation.CanExecute())
        {
          operation.Execute(param);
        }
      };
    }

    public void SetOnUpdate<T>(IOperator<T> operation, object param = null)
    {
      _onUpdate = () =>
      {
        if (operation.CanExecute())
        {
          operation.Execute(param);
        }

        return operation.CanExecute();
      };
    }

    public void SetOnFinish<T>(IOperator<T> operation, object param = null)
    {
      _onFinish = () =>
      {
        if (operation.CanExecute())
        {
          operation.Execute(param);
        }
      };
    }

    public void Start()
    {
      _trackKeyboard = new BackgroundAction();

      _onStart.Invoke();

      _onUpdate.Invoke();

      _onFinish.Invoke();

      _trackKeyboard.Dispose();
    }
  }
}