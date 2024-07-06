using System;

namespace JinToliq.Umvvm.ViewModel.Exceptions
{
  public class InvalidPropertyName : Exception
  {
    public readonly string Name;
    public readonly Type ContextType;

    public InvalidPropertyName(string name, Type contextType) : base($"Path: {name} does not exist in the context {contextType.Name}")
    {
      Name = name;
      ContextType = contextType;
    }
  }
}