namespace CoreGameEngine.EngineOperator
{
  internal static class OperationExecutor
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