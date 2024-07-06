using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View
{
  public abstract class BaseUiViewManager : MonoBehaviour
  {
    [SerializeField] private string _resourcesBasePath = "Prefabs/UI";

    private readonly List<IUiView> _pool = new();
    private readonly List<(IUiView View, int Index)> _activeUi = new();

    public bool HasOpenedUi => _activeUi.Count > 0;

    protected abstract IUiView GetNewView(UiType uiType, Enum type);
    protected virtual void OnUiOpened(IUiView view) {}
    protected virtual void OnUiClosed(IUiView view) {}
    protected virtual void OnUiShown(IUiView view) {}
    protected virtual void OnUiHidden(IUiView view) {}

    protected IUiView GetFromResources(UiType uiType, Enum type)
    {
      var path = GetResourcesUiPath(uiType, type);
      var template = Resources.Load<GameObject>(path);
      if (template == null)
        throw new Exception($"No UI prefab found by path: {Path.Combine("Resources", path)}");

      var instance = Instantiate(template);
      var view = instance.GetComponent<IUiView>();
      if (view is null)
        throw new Exception($"UI prefab by path {Path.Combine("Resources", path)} does not contain component implementing {nameof(IUiView)} interface");

      return view;
    }

    private void Awake()
    {
      Ui.Instance.StateOpened += OpenUi;
      Ui.Instance.StateClosed += CloseUi;
      Ui.Instance.StateShown += ShowUi;
      Ui.Instance.StateHidden += HideUi;
    }

    private void OnDestroy()
    {
      Ui.Instance.StateOpened -= OpenUi;
      Ui.Instance.StateClosed -= CloseUi;
      Ui.Instance.StateShown -= ShowUi;
      Ui.Instance.StateHidden -= HideUi;
    }

    private void OpenUi(UiState state)
    {
      IUiView view;
      var pooledIndex = _pool.FindIndex(p => p.UiType == state.UiType && p.BaseType.Equals(state.Type));
      if (pooledIndex < 0)
      {
        view = GetNewView(state.UiType, state.Type);
        view.GetTransform().SetParent(transform);
      }
      else
      {
        view = _pool[pooledIndex];
        _pool.RemoveAt(pooledIndex);
      }

      view.GetGameObject().SetActive(true);
      _activeUi.Add(new(view, state.Index));
      StartCoroutine(DoViewRoutine(view, view.OnOpen(), OnUiOpened));
    }

    private void CloseUi(UiState state)
    {
      var index = _activeUi.FindIndex(p => p.Index == state.Index && p.View.UiType == state.UiType && p.View.BaseType.Equals(state.Type));
      if (index < 0)
        return;

      var view = _activeUi[index].View;
      _activeUi.RemoveAt(index);
      _pool.Add(view);
      StartCoroutine(DoViewRoutine(view, view.OnClose(), Complete));
      return;

      void Complete(IUiView item)
      {
        item.GetGameObject().SetActive(false);
        OnUiClosed(view);
      }
    }

    private void ShowUi(UiState state)
    {
      var index = _activeUi.FindIndex(p => p.Index == state.Index && p.View.UiType == state.UiType && p.View.BaseType.Equals(state.Type));
      if (index < 0)
        return;

      var view = _activeUi[index].View;
      view.GetGameObject().SetActive(true);
      StartCoroutine(DoViewRoutine(view, view.OnShow(), OnUiShown));
    }

    private void HideUi(UiState state)
    {
      var index = _activeUi.FindIndex(p => p.Index == state.Index && p.View.UiType == state.UiType && p.View.BaseType.Equals(state.Type));
      if (index < 0)
        return;

      var view = _activeUi[index].View;
      StartCoroutine(DoViewRoutine(view, view.OnHide(), Complete));
      return;

      void Complete(IUiView item)
      {
        item.GetGameObject().SetActive(false);
        OnUiHidden(view);
      }
    }

    private IEnumerator DoViewRoutine(IUiView view, IEnumerator routine, Action<IUiView> onEnd)
    {
      while (routine.MoveNext())
        yield return routine.Current;

      onEnd(view);
    }

    private string GetResourcesUiPath(UiType uiType, Enum type)
    {
      if (string.IsNullOrEmpty(_resourcesBasePath))
        throw new Exception("PopupResourcesBasePath should be set");

      return Path.Combine(_resourcesBasePath, uiType.ToString(), type.ToString());
    }
  }
}