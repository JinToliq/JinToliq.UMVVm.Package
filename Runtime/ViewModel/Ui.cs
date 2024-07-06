using System;
using System.Collections.Generic;
using System.Linq;

namespace JinToliq.Umvvm.ViewModel
{
  public class Ui
  {
    public event Action<UiState> StateOpened;
    public event Action<UiState> StateClosed;
    public event Action<UiState> StateShown;
    public event Action<UiState> StateHidden;

    public static Ui Instance { get; } = new();

    private List<UiState> _states = new();

    private Ui() { }

    public void ToggleIfLastState(UiType uiType, Enum type)
    {
      if (_states.Count == 0)
      {
        OpenState(uiType, type);
        return;
      }

      var last = _states[^1];
      if (last.UiType != uiType || !last.Type.Equals(type))
      {
        OpenState(uiType, type);
        return;
      }

      CloseState(uiType, type);
    }

    public void OpenState(UiType uiType, Enum type)
    {
      var index = 0;
      if (_states.Count > 0 && uiType is UiType.Window)
      {
        foreach (var uiState in _states.Where(uiState => uiState.IsActive))
        {
          uiState.IsActive = false;
          StateHidden?.Invoke(uiState);
        }

        index = _states[^1].Index + 1;
      }

      var state = new UiState(uiType, type, index)
      {
        IsActive = true
      };
      _states.Add(state);
      StateOpened?.Invoke(state);
    }

    public void CloseState(UiType uiType, Enum type)
    {
      if (_states.Count == 0)
        return;

      var last = _states[^1];
      if (last.UiType == uiType && last.Type.Equals(type))
      {
        Back();
        return;
      }

      if (_states.Count == 1)
        return;

      for (var i = _states.Count - 2; i >= 0; i--)
      {
        var state = _states[i];
        if (state.UiType != uiType || !state.Type.Equals(type))
          continue;

        _states.RemoveAt(i);
        StateClosed?.Invoke(state);
      }
    }

    public void Back()
    {
      if (_states.Count == 0)
        return;

      var last = _states[^1];
      _states.RemoveAt(_states.Count - 1);
      StateClosed?.Invoke(last);

      if (_states.Count == 0)
        return;

      for (var i = _states.Count - 1; i >= 0; i--)
      {
        var state = _states[i];
        if (!state.IsActive)
        {
          state.IsActive = true;
          StateShown?.Invoke(state);
        }

        if (state.UiType is UiType.Window)
          break;
      }
    }
  }
}