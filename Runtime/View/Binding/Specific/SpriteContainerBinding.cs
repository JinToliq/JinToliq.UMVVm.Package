using JinToliq.Umvvm.View.Misc;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class SpriteContainerBinding : SinglePropertyBinding
  {
    [SerializeField] private Image _target;
    [SerializeField] private SpriteContainer _container;

    protected override void OnBeforeChange(Property property)
    { }

    protected override void OnChanged(Property property)
    {
      _target.sprite = _container.Evaluate(property.GetValue().ToString());
    }

    private void Reset()
    {
      _target = GetComponent<Image>();
    }
  }
}