﻿using System;
using CoreGameEngine.Factory;

namespace CoreGameEngine.KeyboardController
{
  public abstract class Controller : IController
  {
    private readonly BackgroundAction _backgroundControl = new BackgroundAction();
    private readonly IFactory<ConsoleKey, Action> _controllerFactory;

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public void AddControl(ConsoleKey consoleKey, Func<Action> action)
    {
      _controllerFactory.Register(consoleKey, action);
    }

    public void RunController()
    {
      _backgroundControl.Start(1, () =>
      {
        var key = Console.ReadKey(true).Key;

        if (_controllerFactory.ContainsKey(key))
        {
          _controllerFactory.Create(key).Invoke();
        }
      });
    }

    public void StopController()
    {
      _backgroundControl.Stop();
    }

    protected Controller(IFactory<ConsoleKey, Action> controllerFactory)
    {
      _controllerFactory = controllerFactory;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _backgroundControl?.Dispose();
      }
    }
  }
}