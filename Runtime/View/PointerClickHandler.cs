using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JinToliq.Umvvm.View
{
  public class PointerClickHandler : Graphic, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
  {
    public event Action<PointerEventData> OnClick;
    public event Action<PointerEventData> OnEnter;
    public event Action<PointerEventData> OnExit;
    public event Action<PointerEventData> OnDown;
    public event Action<PointerEventData, GameObject> OnUp;

    private static GameObject _currentUnderlyingGameObject;

    public void OnPointerClick(PointerEventData eventData) => OnClick?.Invoke(eventData);

    public void OnPointerEnter(PointerEventData eventData)
    {
      _currentUnderlyingGameObject = gameObject;
      OnEnter?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      _currentUnderlyingGameObject = null;
      OnExit?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      _currentUnderlyingGameObject = gameObject;
      OnDown?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      OnUp?.Invoke(eventData, _currentUnderlyingGameObject);
    }

    public override bool raycastTarget
    {
      get => true;
      set { }
    }

    protected override void Awake()
    {
      base.Awake();
      if (!gameObject.GetComponent<CanvasRenderer>())
        gameObject.AddComponent<CanvasRenderer>();
    }

#if UNITY_EDITOR
    public override void OnRebuildRequested() { }
#endif

    protected override void OnPopulateMesh(VertexHelper vh) =>
      vh.Clear();
  }

#if UNITY_EDITOR
  [CustomEditor(typeof(PointerClickHandler))]
  public class PointerClickHandlerEditor : Editor
  {
    public override void OnInspectorGUI()
    { }
  }
#endif
}