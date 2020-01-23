using System;
using System.Diagnostics.CodeAnalysis;
using CoreGameEngine.EngineActions;
using CoreGameEngine.KeyboardController;
using CoreGameEngine.Shapes;

namespace CoreGameEngine
{
  public class EngineCore : IDisposable
  {
    private readonly IController _controller;
    private Action _onFinish;
    private Action _onStart;
    private Func<bool> _onUpdate;

    public ShapeManager ShapeManager { get; }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private EngineCore(IController controller)
    {
      _controller = controller;
      ShapeManager = ShapeManager.NewManager();
    }

    public static EngineCore NewEngine([NotNull] IController controller)
    {
      return new EngineCore(controller);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _controller?.Dispose();
      }
    }

    public void SetOnStart(IEngineAction action)
    {
      _onStart = () =>
      {
        if (action.CanExecute)
        {
          action.Execute();
        }
      };
    }

    public void SetOnUpdate(IEngineAction<bool> action)
    {
      _onUpdate = () => action.CanExecute && action.Execute();
    }

    public void SetOnFinish(IEngineAction action)
    {
      _onFinish = () =>
      {
        if (action.CanExecute)
        {
          action.Execute();
        }
      };
    }

    public void Start()
    {
      _controller?.RunController();

      _onStart.Invoke();

      bool update;
      do
      {
        update = _onUpdate.Invoke();
      } while (update);

      _onFinish.Invoke();

      _controller?.StopController();
    }
  }
}