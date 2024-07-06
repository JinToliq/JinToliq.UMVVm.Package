using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding
{
  public abstract class CommandBinding : BaseBinding
  {
    [SerializeField] private string _path;
    private Command _command;

    public void Invoke() => (_command ??= GetCommand(_path))?.Invoke();

    protected override void Bind() { }

    protected override void Unbind() => _command = null;
  }

  public abstract class CommandBinding<TArg> : BaseBinding
  {
    [SerializeField] private string _path;
    private Command<TArg> _command;

    public void Invoke(TArg arg) => (_command ??= GetCommand<Command<TArg>>(_path))?.Invoke(arg);

    protected override void Bind() { }

    protected override void Unbind() => _command = null;
  }
}