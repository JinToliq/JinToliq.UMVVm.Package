using System;

namespace JinToliq.Umvvm.ViewModel
{
  public enum UiType
  {
    PopAbove,
    OpenNewWindow,
  }

  public class UiState
  {
    public readonly Enum Type;
    public readonly int Index;
    public bool IsActive;

    public UiState(Enum type, int index)
    {
      Type = type;
      Index = index;
    }
  }
}