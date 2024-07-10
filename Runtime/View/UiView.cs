using System;
using System.Collections;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View
{
  public interface IUiView
  {
    UiType UiType { get; }
    Enum BaseType => UiType;

    GameObject GetGameObject();
    Transform GetTransform();

    IEnumerator OnOpen();
    IEnumerator OnClose();
    IEnumerator OnShow();
    IEnumerator OnHide();
  }

  public interface IUiView<out TType> : IUiView where TType : Enum
  {
    TType Type { get; }
  }

  public abstract class UiView<TType> : DataView, IUiView<TType> where TType : Enum
  {
    [SerializeField] private TType _type;

    public abstract UiType UiType { get; }
    public TType Type => _type;

    public override IDataView GetInitialized() => this;

    public GameObject GetGameObject() => gameObject;

    public Transform GetTransform() => transform;

    public IEnumerator OnOpen()
    {
      yield break;
    }

    public IEnumerator OnClose()
    {
      yield break;
    }

    public IEnumerator OnShow()
    {
      yield break;
    }

    public IEnumerator OnHide()
    {
      yield break;
    }
  }

  public abstract class UiView<TType, TContext> : DataView<TContext>, IUiView<TType>
    where TType : Enum
    where TContext : Context, new()
  {
    [SerializeField] private TType _type;

    public abstract UiType UiType { get; }
    public TType Type => _type;

    public GameObject GetGameObject() => gameObject;

    public Transform GetTransform() => transform;

    public IEnumerator OnOpen()
    {
      yield break;
    }

    public IEnumerator OnClose()
    {
      yield break;
    }

    public IEnumerator OnShow()
    {
      yield break;
    }

    public IEnumerator OnHide()
    {
      yield break;
    }
  }
}