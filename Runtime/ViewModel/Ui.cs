using System;
using System.Collections.Generic;
using System.Linq;

namespace JinToliq.Umvvm.ViewModel
{
  public class Ui
  {
    public event Action<UiState> StateOpened;
    public event Action<UiState> StateClosed;

    public static Ui Instance { get; } = new();

    private List<UiState> _states = new();

    private Ui() { }

    public bool IsLastOpenedState(Enum type) => _states.Count > 0 && _states[^1].Type.Equals(type);

    public void ToggleIfLastState(Enum type)
    {
      if (_states.Count == 0)
      {
        OpenState(type);
        return;
      }

      var last = _states[^1];
      if (!last.Type.Equals(type))
      {
        OpenState(type);
        return;
      }

      CloseState(type);
    }

    public void OpenState(Enum type, object openWithState = null)
    {
      var state = new UiState(type, _states.Count > 0 ? _states.Max(s => s.Index + 1) : 0, openWithState);
      _states.Add(state);
      StateOpened?.Invoke(state);
    }

    public void CloseState(Enum type)
    {
      if (_states.Count == 0)
        return;

      var last = _states[^1];
      if (last.Type.Equals(type))
      {
        Back();
        return;
      }

      if (_states.Count == 1)
        return;

      for (var i = _states.Count - 2; i >= 0; i--)
      {
        var state = _states[i];
        if (!state.Type.Equals(type))
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
    }
  }
}