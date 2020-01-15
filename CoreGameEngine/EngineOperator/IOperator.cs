using System.Diagnostics.CodeAnalysis;

namespace CoreGameEngine.EngineOperator
{
  public interface IOperator<T>
  {
    public bool CanExecute();
    public void SetOperation([NotNull] IEngineTask<T> operation);
    public void ExecuteOperation(object param);
  }
}