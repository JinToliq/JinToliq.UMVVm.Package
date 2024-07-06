using System.Runtime.CompilerServices;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding
{
  public abstract class BaseBinding : MonoBehaviour
  {
    private IDataView _view;

    protected virtual bool AlwaysActiveForChange => false;
    protected bool IsBound { get; private set; }

    public Property GetProperty(string property) => _view.GetProperty(property);

    public TProperty GetProperty<TProperty>(string property) where TProperty : Property => _view.GetProperty<TProperty>(property);

    public Command GetCommand(string property) => _view.GetCommand(property);

    public TCommand GetCommand<TCommand>(string property) where TCommand : ICommand => _view.GetCommand<TCommand>(property);

    protected virtual void Bind() {}

    protected virtual void OnBound() {}

    protected virtual void Unbind() {}

    protected virtual void OnAwakened() {}

    protected virtual void OnEnabled() {}

    protected virtual void OnDisabled() {}

    private void Awake()
    {
      _view = GetView();
      OnAwakened();
    }

    private void OnEnable()
    {
      if (!IsBound)
      {
        Bind();
        OnBound();
        IsBound = true;
      }

      OnEnabled();
    }

    private void OnDisable()
    {
      if (!AlwaysActiveForChange)
      {
        Unbind();
        IsBound = false;
      }

      OnDisabled();
    }

    private void OnDestroy()
    {
      if (!IsBound)
        return;

      Unbind();
      IsBound = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IDataView GetView() => (GetComponent<IDataView>() ?? GetComponentInParent<IDataView>()).GetInitialized();
  }
}