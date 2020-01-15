using System;
using System.Threading;
using System.Threading.Tasks;
using CoreGameEngine.Resources;

namespace CoreGameEngine
{
  public sealed class BackgroundAction : IDisposable
  {
    private Task _backgroundTask;
    private int _counter;
    private CancellationTokenSource _cts;

    public int Counter => _counter;

    public void Dispose()
    {
      if (_backgroundTask != null)
      {
        Stop();
      }

      _cts.Dispose();
    }

    public void UpdateCounter()
    {
      Interlocked.Increment(ref _counter);
    }

    public void UpdateCounter(int increment)
    {
      Interlocked.Add(ref _counter, increment);
    }

    public void ResetCounter()
    {
      Interlocked.Exchange(ref _counter, 0);
    }

    public void Start(int actionExecutionInterval, Action action)
    {
      if (actionExecutionInterval < 1)
      {
        throw new ArgumentException(Exceptions.BackgroundAction_IntervalLessThanOne, nameof(actionExecutionInterval));
      }

      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      if (_backgroundTask != null)
      {
        throw new InvalidOperationException(Exceptions.BackgroundAction_AlreadyRunning);
      }

      _cts = new CancellationTokenSource();
      _backgroundTask = CreateBackgroundTask(actionExecutionInterval, action);
    }

    public void Stop()
    {
      if (_backgroundTask == null)
      {
        throw new InvalidOperationException(Exceptions.BackgroundAction_NotRunning);
      }

      CancelTracking();

      // allows the garbage collector to know the task can be disposed of
      _backgroundTask = null;
    }

    private void CancelTracking()
    {
      try
      {
        _cts.Cancel();
      }
      catch (TaskCanceledException)
      {
        // task is already canceled, ignore the exception
      }
    }

    private Task CreateBackgroundTask(int actionExecutionInterval, Action action)
    {
      return Task.Factory.StartNew(async () =>
      {
        while (!_cts.IsCancellationRequested)
        {
          await Task.Delay(TimeSpan.FromMilliseconds(actionExecutionInterval), _cts.Token).ConfigureAwait(false);

          if (!_cts.IsCancellationRequested)
          {
            action.Invoke();
            UpdateCounter(_counter);
          }
        }
      }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();
    }
  }
}