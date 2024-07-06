using System;
using System.Collections;
using System.Collections.Generic;

namespace JinToliq.Umvvm.ViewModel.Properties
{
  public class CollectionProperty<TElement> : Property<IList<TElement>>, IList<TElement>
  {
    public int Count
    {
      get
      {
        if (Value is null)
          throw new NullReferenceException("Collections is null");

        return Value.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        if (Value is null)
          throw new NullReferenceException("Collections is null");

        return Value.IsReadOnly;
      }
    }

    public CollectionProperty(string name) : base(name)
    { }

    public CollectionProperty(string name, IList<TElement> value) : base(name, value)
    { }

    public CollectionProperty(string name, IList<TElement> value, bool ignoreDifference) : base(name, value, ignoreDifference)
    { }

    public IEnumerator<TElement> GetEnumerator() => Value.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(TElement item)
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      InvokeBeforeChange();
      Value.Add(item);
      InvokeChanged();
    }

    public void Clear()
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      InvokeBeforeChange();
      Value.Clear();
      InvokeChanged();
    }

    public bool Contains(TElement item)
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      return Value.Contains(item);
    }

    public void CopyTo(TElement[] array, int arrayIndex)
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      Value.CopyTo(array, arrayIndex);
    }

    public bool Remove(TElement item)
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      InvokeBeforeChange();
      var changed = Value.Remove(item);
      InvokeChanged();
      return changed;
    }

    public int IndexOf(TElement item)
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      return Value.IndexOf(item);
    }

    public void Insert(int index, TElement item)
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      if (Value.Count <= index)
        throw new IndexOutOfRangeException();

      InvokeBeforeChange();
      Value.Insert(index, item);
      InvokeChanged();
    }

    public void RemoveAt(int index)
    {
      if (Value is null)
        throw new NullReferenceException("Collections is null");

      if (Value.Count <= index)
        throw new IndexOutOfRangeException();

      InvokeBeforeChange();
      Value.RemoveAt(index);
      InvokeChanged();
    }

    public TElement this[int index]
    {
      get
      {
        if (Value is null)
          throw new NullReferenceException("Collections is null");

        return Value[index];
      }
      set
      {
        if (Value is null)
          throw new NullReferenceException("Collections is null");

        if (Value.Count <= index)
          throw new IndexOutOfRangeException();

        if (IsEqualElements(Value[index], value))
        {
          if (!IgnoreDifference)
            return;

          InvokeBeforeChange();
          InvokeChanged();
          return;
        }

        InvokeBeforeChange();
        Value[index] = value;
        InvokeChanged();
      }
    }

    private bool IsEqualElements(TElement a, TElement b)
    {
      if (ReferenceEquals(a, b))
        return true;

      return a is not null && b is not null && a.GetHashCode() == b.GetHashCode();
    }
  }
}