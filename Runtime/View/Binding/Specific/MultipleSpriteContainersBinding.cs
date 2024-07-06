using JinToliq.Umvvm.View.Misc;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class MultipleSpriteContainersBinding : SinglePropertyBinding
  {
    [SerializeField] private Image _target;
    [SerializeField] private SpriteContainer[] _containers;

    protected override void OnBeforeChange(Property property)
    { }

    protected override void OnChanged(Property property)
    {
      var value = property.GetValue();
      if (value is null)
      {
        _target.sprite = null;
        return;
      }

      var spriteName = value.ToString();
      foreach (var container in _containers)
      {
        var sprite = container.Evaluate(spriteName);
        if (sprite == null)
          continue;

        _target.sprite = sprite;
        return;
      }
    }

    private void Reset()
    {
      _target = GetComponent<Image>();
    }
  }
}