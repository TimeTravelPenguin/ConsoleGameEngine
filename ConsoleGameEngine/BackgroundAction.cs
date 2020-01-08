﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGameEngine
{
  public sealed class BackgroundAction : IDisposable
  {
    private Task _backgroundTask;
    private int _counter;
    private CancellationTokenSource _cts;

    public int Counter => _counter;

    public void Dispose()
    {
      // we cannot cancel any running task AND wait for the thread to complete (cannot await within Dispose)
      // so clean up the best we can.

      if (_backgroundTask != null)
      {
        Stop();
      }
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
        throw new ArgumentException("The interval must be a positive value");
      }

      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      if (_backgroundTask != null)
      {
        throw new InvalidOperationException("Progress tracking is already started");
      }

      _cts = new CancellationTokenSource();
      _backgroundTask = CreateBackgroundTask(actionExecutionInterval, action);
    }

    public void Stop()
    {
      if (_backgroundTask == null)
      {
        throw new InvalidOperationException("Progress tracking is not started");
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
          await Task.Delay(TimeSpan.FromMilliseconds(actionExecutionInterval), _cts.Token);

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