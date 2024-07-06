using System;
using System.Linq;
using UnityEngine;

namespace JinToliq.Umvvm.View.Misc
{
  [Serializable]
  public class ObjectContainerItem<TValue>
  {
    public TValue Value;

    public virtual string GetName()
    {
      return Value is UnityEngine.Object uObject ? uObject.name : Value.ToString();
    }
  }

  [Serializable]
  public class ObjectContainerNamedItem<TValue> : ObjectContainerItem<TValue>
  {
    [SerializeField] private string Name;

    public override string GetName() => string.IsNullOrEmpty(Name) ? base.GetName() : Name;
  }

  [Serializable]
  [CreateAssetMenu(menuName = "Object Container/Object Container")]
  public class ObjectContainer<TValue> : ScriptableObject
  {
    [SerializeField] private ObjectContainerNamedItem<TValue>[] _items;
    [SerializeField] private TValue _fallback;

    public TValue Evaluate(string value)
    {
      if (string.IsNullOrEmpty(value))
        return _fallback;

      var item = _items.FirstOrDefault(i => value.Equals(i.GetName(), StringComparison.InvariantCultureIgnoreCase));
      return item is null ? _fallback : item.Value;
    }

    public TValue Evaluate(int index)
    {
      if (index < 0 || index >= _items.Length)
        return _fallback;

      return _items[index].Value;
    }
  }
}