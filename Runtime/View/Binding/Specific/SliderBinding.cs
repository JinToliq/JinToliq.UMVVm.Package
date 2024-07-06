using JinToliq.Umvvm.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class SliderBinding : SinglePropertyBinding<float>
  {
    [SerializeField] private Slider _slider;

    protected override void OnBeforeChange(Property<float> property)
    { }

    protected override void OnChanged(Property<float> property)
    {
      _slider.value = property.Value;
    }

#if UNITY_EDITOR
    private void Reset()
    {
      _slider = GetComponent<Slider>();
    }
#endif
  }
}