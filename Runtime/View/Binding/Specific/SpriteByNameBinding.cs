using System;
using System.Linq;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  [Serializable]
  public class SpriteByNameBindingItem
  {
    public string Name;
    public Sprite Sprite;
  }

  public class SpriteByNameBinding : SinglePropertyBinding
  {
    [SerializeField] private Image _target;
    [SerializeField] private SpriteByNameBindingItem[] _sprites;
    [SerializeField] private Sprite _fallback;

    protected override void OnBeforeChange(Property property) { }

    protected override void OnChanged(Property property)
    {
      var sprite = property.ToString();
      if (string.IsNullOrEmpty(sprite))
      {
        _target.sprite = _fallback;
        return;
      }

      var item = _sprites.FirstOrDefault(s => sprite.Equals(s.Name, StringComparison.InvariantCultureIgnoreCase));
      _target.sprite = item is null ? _fallback : item.Sprite;
    }
  }
}