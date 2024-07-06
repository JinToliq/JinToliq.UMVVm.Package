using System;
using System.Collections.Generic;
using UnityEngine;

namespace JinToliq.Umvvm.View
{
  public class UiPool : MonoBehaviour
  {
    private class Bucket
    {
      public readonly GameObject Original;
      public readonly Stack<GameObject> Pooled;
      public readonly HashSet<GameObject> Used;

      public Bucket(GameObject original)
      {
        Original = original;
        Pooled = new();
        Used = new();
      }
    }

    public static UiPool Instance => LazyInstance.Value;
    private static readonly Lazy<UiPool> LazyInstance = new(Factory);
    private readonly Dictionary<string, Bucket> _registry = new();

    private static UiPool Factory()
    {
      var result = new GameObject("UiPool").AddComponent<UiPool>();
      result.gameObject.SetActive(false);
      return result;
    }

    public void EnsureRegistered(GameObject original, int count = 1)
    {
      if (!IsRegistered(original.name))
        Register(original.name, original, count);
    }

    public bool IsRegistered(GameObject original) => IsRegistered(original.name);

    public bool IsRegistered(string key) => _registry.ContainsKey(key);

    public void Register(GameObject original, int count = 1, bool additive = false)
    {
      Register(original.name, original, count, additive);
    }

    public void Register(string key, GameObject original, int count = 1, bool additive = false)
    {
      RegisterKeyIfNeeded(key, original);

      if (!additive && _registry[key].Pooled.Count >= count)
        return;

      var pool = _registry[key].Pooled;
      for (var i = 0; i < count; i++)
      {
        var instance = Instantiate(original, transform);
        instance.name = $"{key} ({pool.Count + _registry[key].Used.Count})";
        instance.SetActive(false);
        pool.Push(instance);
      }
    }

    public T Get<T>(GameObject original) where T : Component
    {
      EnsureRegistered(original);
      return Get<T>(original.name);
    }

    public T Get<T>(GameObject original, Transform parent) where T : Component
    {
      EnsureRegistered(original);
      return Get<T>(original.name, parent);
    }

    public GameObject Get(GameObject original, Transform parent)
    {
      EnsureRegistered(original);
      return Get(original.name, parent);
    }

    public GameObject Get(GameObject original)
    {
      EnsureRegistered(original);
      return Get(original.name, original);
    }

    public T Get<T>(string key) where T : Component
    {
      var go = Get(key);
      return go.GetComponent<T>();
    }

    public T Get<T>(string key, Transform parent) where T : Component
    {
      var go = Get(key, parent);
      return go.GetComponent<T>();
    }

    public GameObject Get(string key, Transform parent)
    {
      var go = Get(key);
      var trans = go.transform;
      var originalScale = trans.localScale;
      trans.SetParent(parent);
      trans.localScale = originalScale;
      return go;
    }

    public GameObject Get(string key, GameObject original = null)
    {
      RegisterKeyIfNeeded(key, original);

      var bucket = _registry[key];
      if (!bucket.Pooled.TryPop(out var instance))
      {
        instance = Instantiate(bucket.Original, transform);
        instance.name = $"{key} ({bucket.Pooled.Count + bucket.Used.Count})";
        instance.SetActive(false);
      }

      bucket.Used.Add(instance);
      return instance;
    }

    public void Recycle(Component instance) => Recycle(instance.gameObject);

    public void Recycle(Transform instance) => Recycle(instance.gameObject);

    public void Recycle(GameObject instance)
    {
      if (instance == null)
        return;

      if (gameObject == null)
      {
        Destroy(instance);
        return;
      }

      foreach (var bucket in _registry.Values)
      {
        if (!bucket.Used.Remove(instance))
          continue;

        instance.SetActive(false);
        bucket.Pooled.Push(instance);
        instance.transform.SetParent(transform);
        break;
      }
    }

    private void RegisterKeyIfNeeded(string key, GameObject original)
    {
      if (!_registry.ContainsKey(key))
        _registry.Add(key, new(original));
    }
  }
}