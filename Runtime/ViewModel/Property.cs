using System;
using JinToliq.Umvvm.ViewModel.Exceptions;

namespace JinToliq.Umvvm.ViewModel
{
  public abstract class Property
  {
    public event Action<Property> BeforeChange;
    public event Action<Property> Changed;

    public readonly string Name;
    public readonly bool IgnoreDifference;

    protected Property(string name, bool ignoreDifference)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentNullException(nameof(name));

      Name = name;
      IgnoreDifference = ignoreDifference;
    }

    public TProperty As<TProperty>() where TProperty : Property
    {
      if (this is TProperty typed)
        return typed;

      throw new InvalidPropertyTypeException();
    }

    public TValue GetValue<TValue>() => (TValue)GetValue();

    public abstract object GetValue();

    public abstract Type GetDataType();

    protected virtual void InvokeBeforeChange() => BeforeChange?.Invoke(this);

    protected virtual void InvokeChanged() => Changed?.Invoke(this);
  }

  public class Property<TData> : Property
  {
    public new event Action<Property<TData>> BeforeChange;
    public new event Action<Property<TData>> Changed;

    private TData _value;

    public TData Value
    {
      get => _value;
      set
      {
        if (ReferenceEquals(_value, value) && !IgnoreDifference)
          return;

        if (_value is not null && _value.Equals(value) && !IgnoreDifference)
          return;

        InvokeBeforeChange();
        _value = value;
        InvokeChanged();
      }
    }

    public Property(string name) : base(name, false) { }

    public Property(string name, TData value) : base(name, false) => _value = value;

    public Property(string name, TData value, bool ignoreDifference) : base(name, ignoreDifference) => _value = value;

    public override object GetValue() => Value;

    public override Type GetDataType() => typeof(TData);

    protected override void InvokeBeforeChange()
    {
      base.InvokeBeforeChange();
      BeforeChange?.Invoke(this);
    }

    protected override void InvokeChanged()
    {
      base.InvokeChanged();
      Changed?.Invoke(this);
    }

    public override string ToString()
    {
      if (_value is null)
        return null;

      return Value as string ?? Value.ToString();
    }
  }
}