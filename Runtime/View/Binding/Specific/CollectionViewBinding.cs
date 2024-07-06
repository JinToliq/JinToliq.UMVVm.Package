using System.Collections;
using System.Collections.Generic;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class CollectionViewBinding : CollectionPropertyBinding
  {
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private bool _clearChildrenOnAwake = true;
    private readonly List<DataView> _instances = new();

    protected override void OnAwakened()
    {
      base.OnAwakened();
      if (!_clearChildrenOnAwake)
        return;

      var childCount = _parent.childCount;
      for (var i = childCount - 1; i >= 0; i--)
      {
        var child = _parent.GetChild(i);
        Destroy(child.gameObject);
      }
    }

    protected override void OnChanged(Property property)
    {
      var value = property.GetValue();
      if (value is not IList collection || collection.Count == 0)
      {
        foreach (var instance in _instances)
          UiPool.Instance.Recycle(instance);

        _instances.Clear();
        return;
      }

      var newCount = collection.Count;
      var oldCount = _instances.Count;

      if (newCount < oldCount)
      {
        for (var i = oldCount - 1; i >= newCount; i--)
        {
          var instance = _instances[i];

          UiPool.Instance.Recycle(instance);
          _instances.RemoveAt(i);
        }
      }
      else if (newCount > oldCount)
      {
        for (var i = oldCount; i < newCount; i++)
        {
          var instance = UiPool.Instance.Get<DataView>(_prefab, _parent);
          instance.gameObject.SetActive(true);
          _instances.Add(instance);
        }
      }

      for (var i = 0; i < newCount; i++)
      {
        var state = collection[i];
        var instance = _instances[i];
        instance.SetState(state);
      }
    }

    private void Reset()
    {
      _parent = transform;
    }
  }
}