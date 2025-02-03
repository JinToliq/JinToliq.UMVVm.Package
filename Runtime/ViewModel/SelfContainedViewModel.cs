using System;

namespace JinToliq.Umvvm.ViewModel
{
  public class SelfContainedViewModel : BaseViewModel
  {
    public event Action<UiState> StateOpened;
    public event Action<UiState> StateClosed;

    public static SelfContainedViewModel Instance { get; } = new();

    protected SelfContainedViewModel() {}

    protected override void OnStateOpened(UiState state) =>
      StateOpened?.Invoke(state);

    protected override void OnStateClosed(UiState state) =>
      StateClosed?.Invoke(state);
  }
}