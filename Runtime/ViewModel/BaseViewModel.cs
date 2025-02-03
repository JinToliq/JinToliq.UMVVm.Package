using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;

namespace JinToliq.Umvvm.ViewModel
{
  [Preserve]
  public abstract class BaseViewModel
  {
    private List<UiState> _states = new();

    [Preserve]
    public bool IsLastOpenedState(Enum type) => _states.Count > 0 && _states[^1].Type.Equals(type);

    [Preserve]
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

    [Preserve]
    public void OpenState(Enum type, object openWithState = null)
    {
      var state = new UiState(type, _states.Count > 0 ? _states.Max(s => s.Index + 1) : 0, openWithState);
      _states.Add(state);
      OnStateOpened(state);
    }

    [Preserve]
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
        OnStateClosed(state);
      }
    }

    [Preserve]
    public void Back()
    {
      if (_states.Count == 0)
        return;

      var last = _states[^1];
      _states.RemoveAt(_states.Count - 1);
      OnStateClosed(last);
    }

    protected abstract void OnStateOpened(UiState state);

    protected abstract void OnStateClosed(UiState state);
  }
}