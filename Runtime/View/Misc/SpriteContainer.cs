using System;
using System.Linq;
using UnityEngine;

namespace JinToliq.Umvvm.View.Misc
{
  [CreateAssetMenu(menuName = "Object Container/Sprite Container")]
  public class SpriteContainer : ScriptableObject
  {
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Sprite _fallback;

    public Sprite Evaluate(string value)
    {
      if (string.IsNullOrEmpty(value))
        return _fallback;

      var item = _sprites.FirstOrDefault(i => value.Equals(i.name, StringComparison.InvariantCultureIgnoreCase));
      return item ?? _fallback;
    }

    public Sprite Evaluate(int index)
    {
      if (index < 0 || index >= _sprites.Length)
        return _fallback;

      return _sprites[index];
    }
  }
}