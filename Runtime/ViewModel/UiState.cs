using System;

namespace JinToliq.Umvvm.ViewModel
{
  public enum UiType
  {
    Popup,
    Window,
  }

  public class UiState
  {
    public readonly UiType UiType;
    public readonly Enum Type;
    public readonly int Index;
    public bool IsActive;

    public UiState(UiType uiType, Enum type, int index)
    {
      UiType = uiType;
      Type = type;
      Index = index;
    }
  }
}