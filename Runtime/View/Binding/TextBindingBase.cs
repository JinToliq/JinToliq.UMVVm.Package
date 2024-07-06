using System;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding
{
  public enum TextBindingOption
  {
    Default = 0,
    ToUpper = 1,
    ToLower = 2,
    ToSentence = 3,
  }

  public abstract class TextBindingBase : SinglePropertyBinding
  {
    [SerializeField] private TextBindingOption _option;
    [SerializeField] private string _format;

    protected override void OnBeforeChange(Property property) { }

    protected override void OnChanged(Property property)
    {
      var value = property.ToString();
      if (string.IsNullOrEmpty(value))
      {
        SetText(string.Empty);
        return;
      }

      value = _option switch
      {
        TextBindingOption.Default => value,
        TextBindingOption.ToUpper => value.ToUpper(),
        TextBindingOption.ToLower => value.ToLower(),
        TextBindingOption.ToSentence => string.Concat(value[0].ToString().ToUpper(), value[1..].ToLower()),
        _ => throw new ArgumentOutOfRangeException()
      };

      if (!string.IsNullOrEmpty(_format))
        value = string.Format(_format, value);

      SetText(value);
    }

    protected abstract void SetText(string value);
  }
}