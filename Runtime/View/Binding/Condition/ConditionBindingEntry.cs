using System;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding.Condition
{
  public enum ConditionType
  {
    Bool = 0,
    Equals = 1,
    Greater = 2,
    Empty = 3,
  }

  [Serializable]
  public class ConditionBindingEntry
  {
    public event Action Changed;

    [SerializeField] private string _path;
    [SerializeField] private ConditionType _condition;
    [SerializeField] private string _value;
    [SerializeField] private bool _invert;

    private Property _property;

    public void Bind(BaseBinding binding)
    {
      _property = binding.GetProperty(_path);
      _property.Changed += OnChanged;
    }

    public void Unbind()
    {
      _property.Changed -= OnChanged;
      _property = null;
    }

    public bool Evaluate()
    {
      var input = _property.GetValue();
      var direct = EvaluateDirect(input);
      return _invert
        ? !direct
        : direct;
    }

    public string ToInspectString() => _property is null ? "" : $"{_path}: {_property.GetValue()?.ToString() ?? "null"}";

    protected void OnChanged(Property property)
    {
      Changed!.Invoke();
    }

    private bool EvaluateDirect(object input)
    {
      if (input == null)
        return EvaluateNull();

      switch (input)
      {
        case bool boolValue:
          return boolValue;
        case string stringValue:
          return EvaluateString(stringValue);
        case byte byteValue:
          return EvaluateNumber(byteValue);
        case short shortValue:
          return EvaluateNumber(shortValue);
        case int intValue:
          return EvaluateNumber(intValue);
        case long longValue:
          return EvaluateNumber(longValue);
        case float floatValue:
          return EvaluateNumber(floatValue);
        case double doubleValue:
          return EvaluateNumber(doubleValue);
        case Enum enumValue:
        {
          if (long.TryParse(_value, out var longValue))
            return longValue == Convert.ToInt64(enumValue);

          return string.Equals(_value, enumValue.ToString(), StringComparison.OrdinalIgnoreCase);
        }
        default:
          throw new ArgumentOutOfRangeException(nameof(input), _property.GetDataType().Name, "Unhandled property value type");
      }
    }

    private bool EvaluateNull()
    {
      switch (_condition)
      {
        case ConditionType.Bool:
        case ConditionType.Equals:
        case ConditionType.Greater:
          return false;
        case ConditionType.Empty:
          return true;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool EvaluateString(string value)
    {
      switch (_condition)
      {
        case ConditionType.Bool:
          return false;
        case ConditionType.Equals:
          return string.Equals(value, _value, StringComparison.OrdinalIgnoreCase);
        case ConditionType.Greater:
          return false;
        case ConditionType.Empty:
          return string.IsNullOrEmpty(value);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool EvaluateNumber(long value)
    {
      switch (_condition)
      {
        case ConditionType.Bool:
          return value > 0;
        case ConditionType.Equals:
          return value == long.Parse(_value);
        case ConditionType.Greater:
          return value > long.Parse(_value);
        case ConditionType.Empty:
          return false;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool EvaluateNumber(double value)
    {
      switch (_condition)
      {
        case ConditionType.Bool:
          return value > 0;
        case ConditionType.Equals:
          return Math.Abs(value - double.Parse(_value)) < double.Epsilon;
        case ConditionType.Greater:
          return value > double.Parse(_value);
        case ConditionType.Empty:
          return false;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}