using System;
using CoreGameEngine;
using CoreGameEngine.EngineActions;
using CoreGameEngine.Factory;
using CoreGameEngine.Helpers;
using CoreGameEngine.KeyboardController;
using CoreGameEngine.Shapes;
using CoreGameEngine.Structs;

namespace TetrisDemo
{
  internal class Program
  {
    public class KeysFac : FactoryBase<ConsoleKey, Action>
    {
    }

    public class MyController : Controller
    {
      public MyController(IFactory<ConsoleKey, Action> controllerFactory) : base(controllerFactory)
      {
      }
    }

    public class UpdateAction : IEngineAction<bool>
    {
      private Func<bool> Action;
      public bool CanExecute { get; set; }

      public void SetAction(Func<bool> action)
      {
        Action = action;
        CanExecute = true;
      }

      public bool Execute()
      {
        return Action.Invoke();
      }
    }

    public class StartEndAction : IEngineAction
    {
      private Action Action;
      public bool CanExecute { get; set; }

      public void SetAction(Action action)
      {
        Action = action;
        CanExecute = true;
      }

      public void Execute()
      {
        Action.Invoke();
      }
    }

    private static void Main(string[] args)
    {
      //var myShape = Shape.New("c red r1 c white r1 c red r1 c white r1 c red r1 c blue r4 c white r1 c blue r5" +
      //                        "n 0 1 c red r5 c blue r7 c white r1 c blue r2" +
      //                        "n 0 2 c red r1 c white r1 c red r1 c white r1 c red r1 c blue r2 c white r1 c blue r3 c white r1 c blue r3" +
      //                        "n 0 3 c blue r2 c white r1 c blue r12" +
      //                        "n 0 4 r9 c white r1 c blue r5",
      //  'X',
      //  new Point3D(10, 5, 0));

      var myShape = Shape.New("c red r4 n 4 0 d1", 'X', new Point3D(10, 5, 0));

      var factory = new KeysFac();
      var engine = EngineCore.NewEngine(new MyController(factory));
      engine.ShapeManager.Add<Shape>(myShape);

      #region Start, update, finish

      var onStartAction = new StartEndAction();
      onStartAction.SetAction(() =>
      {
        Console.WriteLine();
        Console.WriteLine("Press any key to start demo showing async timer & controller function");
        Console.WriteLine("Press up to rotate clockwise, down to rotate counter-clockwise, and E to exit");
        Console.ReadKey(true);
        Console.Clear();
      });

      engine.SetOnStart(onStartAction);

      var onUpdateAction = new UpdateAction();
      var count = 0;
      onUpdateAction.SetAction(() =>
      {
        Console.SetCursorPosition(0, 0);
        Console.Write(count++);

        engine.ShapeManager.UpdateScreen();
        return onUpdateAction.CanExecute;
      });

      engine.SetOnUpdate(onUpdateAction);

      var onEndAction = new StartEndAction();
      onEndAction.SetAction(Console.Clear);

      engine.SetOnFinish(onEndAction);

      #endregion

      #region Register Controller

      Action Rotate(Rotation rotation)
      {
        return () => myShape.Rotate(rotation);
      }

      factory.Register(ConsoleKey.UpArrow, () => Rotate(Rotation.Clockwise));
      factory.Register(ConsoleKey.DownArrow, () => Rotate(Rotation.CounterClockwise));
      factory.Register(ConsoleKey.E, () => { return () => onUpdateAction.CanExecute = false; });

      #endregion

      engine.Start();
    }
  }
}