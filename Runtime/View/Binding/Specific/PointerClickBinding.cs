using UnityEngine;
using UnityEngine.EventSystems;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  [RequireComponent(typeof(PointerClickHandler))]
  public class PointerClickBinding : CommandBinding
  {
    [SerializeField] private PointerClickHandler _handler;

    protected override void Bind()
    {
      base.Bind();
      _handler.OnClick += OnPointerClick;
    }

    protected override void Unbind()
    {
      base.Unbind();
      _handler.OnClick -= OnPointerClick;
    }

    public void OnPointerClick(PointerEventData eventData) => Invoke();

#if UNITY_EDITOR
    private void Reset()
    {
      _handler = GetComponent<PointerClickHandler>();
    }
#endif
  }
}