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
    public readonly object OpenWithState;

    public UiState(Enum type, int index, object openWithState = null)
    {
      Type = type;
      Index = index;
      OpenWithState = openWithState;
    }
  }
}