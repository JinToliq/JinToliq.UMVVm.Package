using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class ProgressBarBinding : SinglePropertyBinding<float>
  {
    [SerializeField] private RectTransform _fill;

    private float _maxWidth;

    protected override void OnAwakened()
    {
      _maxWidth = _fill.parent.GetComponent<RectTransform>().rect.width;
    }

    protected override void OnBeforeChange(Property<float> property)
    { }

    protected override void OnChanged(Property<float> property)
    {
      _fill.sizeDelta = new Vector2(_maxWidth * property.Value, _fill.sizeDelta.y);
    }
  }
}