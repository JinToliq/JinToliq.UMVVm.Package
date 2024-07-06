using System;
using JinToliq.Umvvm.ViewModel;

namespace JinToliq.Umvvm.View
{
  public interface IDialogView<TDialogType> where TDialogType : Enum
  {
    TDialogType Type { get; }
  }

  public abstract class DialogView<TDialogType> : DataView, IDialogView<TDialogType> where TDialogType : Enum
  {
    public abstract TDialogType Type { get; }
  }

  public abstract class DialogView<TDialogType, TContext> : DataView<TContext>, IDialogView<TDialogType>
    where TDialogType : Enum
    where TContext : Context, new()
  {
    public abstract TDialogType Type { get; }
  }
}