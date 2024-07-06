using System;
using JinToliq.Umvvm.ViewModel.Exceptions;

namespace JinToliq.Umvvm.ViewModel
{
  public interface ICommand
  {
    TCommand As<TCommand>() where TCommand : ICommand;
  }

  public class Command : ICommand
  {
    private readonly Action _action;

    public Command(Action action) => _action = action ?? throw new ArgumentNullException(nameof(action));

    public TCommand As<TCommand>() where TCommand : ICommand
    {
      if (this is TCommand typed)
        return typed;

      throw new InvalidCommandTypeException();
    }

    public void Invoke() => _action();
  }

  public class Command<TArg> : ICommand
  {
    private readonly Action<TArg> _action;

    public Command(Action<TArg> action) => _action = action ?? throw new ArgumentNullException(nameof(action));

    public TCommand As<TCommand>() where TCommand : ICommand
    {
      if (this is TCommand typed)
        return typed;

      throw new InvalidCommandTypeException();
    }

    public void Invoke(TArg arg)
    {
      _action(arg);
    }
  }
}