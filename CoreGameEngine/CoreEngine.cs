using System;
using System.Diagnostics.CodeAnalysis;
using CoreGameEngine.EngineOperator;
using CoreGameEngine.KeyboardController;

namespace CoreGameEngine
{
  public class CoreEngine : IDisposable
  {
    private readonly IController _controller;
    private Action _onFinish;
    private Action _onStart;
    private Func<bool> _onUpdate;

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public CoreEngine()
    {
    }

    public CoreEngine([NotNull] IController controller)
    {
      _controller = controller;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _controller?.Dispose();
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
      _controller?.RunController();

      _onStart.Invoke();

      _onUpdate.Invoke();

      _onFinish.Invoke();

      _controller?.StopController();
    }
  }
}