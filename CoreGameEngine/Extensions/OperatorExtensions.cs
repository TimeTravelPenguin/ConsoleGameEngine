using CoreGameEngine.EngineOperator;

namespace CoreGameEngine.Extensions
{
  internal static class OperatorExtensions
  {
    public static void Execute<T>(this IOperator<T> operation, object param = null)
    {
      if (operation.CanExecute())
      {
        operation.ExecuteOperation(param);
      }
    }
  }
}