using System;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class SpriteByIndexBinding : SinglePropertyBinding
  {
    [SerializeField] private Image _target;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Sprite _fallback;

    protected override void OnBeforeChange(Property property) { }

    protected override void OnChanged(Property property)
    {
      var value = property.GetValue();
      var index = value switch
      {
        int intVal => intVal,
        Enum enumVal => Convert.ToInt32(enumVal),
        _ => -1,
      };

      if (index < 0 || index >= _sprites.Length)
        _target.sprite = _fallback;
      else
        _target.sprite = _sprites[index];
    }

    private void Reset()
    {
      _target = GetComponent<Image>();
    }
  }
}