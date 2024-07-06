using System;

namespace JinToliq.Umvvm.ViewModel.Exceptions
{
  public class ContextHasNoParent : Exception
  {
    public readonly string Name;

    public ContextHasNoParent(string name) => Name = name;
  }
}