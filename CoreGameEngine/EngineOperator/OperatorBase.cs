using System.Diagnostics.CodeAnalysis;

namespace CoreGameEngine.EngineOperator
{
  public abstract class OperatorBase<T> : IOperator<T>
  {
    private IEngineTask<T> _operation;
    public abstract bool CanExecute();

    public void SetOperation([NotNull] IEngineTask<T> operation)
    {
      _operation = operation;
    }

    public abstract void ExecuteOperation(object param);
  }
}