using System;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding
{
#if UNITY_EDITOR
  [UnityEditor.CustomEditor(typeof(SinglePropertyBinding), true)]
  public class SinglePropertyBindingEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      var binding = (SinglePropertyBinding)target;

      if (!Application.isPlaying)
        return;

      UnityEditor.EditorGUILayout.Space();
      UnityEditor.EditorGUILayout.Space();
      UnityEditor.EditorGUILayout.LabelField("Value:", binding.Property?.GetValue()?.ToString() ?? "null");
    }
  }
#endif

  public abstract class SinglePropertyBinding : BaseBinding
  {
    public Action<object> BeforeChange;
    public Action<object> Changed;

    [SerializeField] private string _path;

    public Property Property { get; private set; }

    protected override void Bind()
    {
      Property = GetProperty(_path);
      Property.BeforeChange += InvokeBeforeChange;
      Property.Changed += InvokeChanged;
    }

    protected override void OnEnabled() => OnChanged(Property);

    protected override void Unbind()
    {
      Property.BeforeChange -= InvokeBeforeChange;
      Property.Changed -= InvokeChanged;
      Property = null;
    }

    protected abstract void OnBeforeChange(Property property);

    protected abstract void OnChanged(Property property);

    private void InvokeBeforeChange(Property property)
    {
      BeforeChange?.Invoke(property.GetValue());
      OnBeforeChange(property);
    }

    private void InvokeChanged(Property property)
    {
      Changed?.Invoke(property.GetValue());
      OnChanged(property);
    }
  }

  public abstract class SinglePropertyBinding<TValue> : BaseBinding
  {
    public Action<TValue> BeforeChange;
    public Action<TValue> Changed;

    [SerializeField] private string _path;

    protected Property<TValue> Property { get; private set; }

    protected override void Bind()
    {
      Property = GetProperty<Property<TValue>>(_path);
      Property.BeforeChange += InvokeBeforeChange;
      Property.Changed += InvokeChanged;
    }

    protected override void OnBound() => OnChanged(Property);

    protected override void Unbind()
    {
      Property.BeforeChange -= InvokeBeforeChange;
      Property.Changed -= InvokeChanged;
      Property = null;
    }

    protected abstract void OnBeforeChange(Property<TValue> property);

    protected abstract void OnChanged(Property<TValue> property);

    private void InvokeBeforeChange(Property<TValue> property)
    {
      BeforeChange?.Invoke(property.Value);
      OnBeforeChange(property);
    }

    private void InvokeChanged(Property<TValue> property)
    {
      Changed?.Invoke(property.Value);
      OnChanged(property);
    }
  }
}