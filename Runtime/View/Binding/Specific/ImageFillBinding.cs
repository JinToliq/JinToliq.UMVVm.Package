using JinToliq.Umvvm.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class ImageFillBinding : SinglePropertyBinding<float>
  {
    [SerializeField] private Image _image;

    protected override void OnBeforeChange(Property<float> property) { }

    protected override void OnChanged(Property<float> property) => _image.fillAmount = property.Value;

    private void Reset() => _image = GetComponent<Image>();
  }
}