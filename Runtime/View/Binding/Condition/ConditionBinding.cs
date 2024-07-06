using System;
using System.Collections.Generic;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding.Condition
{
  public enum Relation
  {
    And = 0,
    Or = 1,
  }

#if UNITY_EDITOR
  [UnityEditor.CustomEditor(typeof(ConditionBinding), true)]
  public class ConditionBindingEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      var binding = (ConditionBinding)target;

      if (!Application.isPlaying)
        return;

      UnityEditor.EditorGUILayout.Space();
      UnityEditor.EditorGUILayout.Space();
      foreach (var condition in binding.Conditions)
        UnityEditor.EditorGUILayout.LabelField(condition.ToInspectString());
    }
  }
#endif

  public abstract class ConditionBinding : BaseBinding
  {
    [SerializeField] private Relation _relation;
    [SerializeField] private ConditionBindingEntry[] _condition;

    public IReadOnlyList<ConditionBindingEntry> Conditions => _condition;

    protected override void Bind()
    {
      foreach (var item in _condition)
      {
        item.Bind(this);
        item.Changed += Evaluate;
      }
    }

    protected override void OnEnabled() => Evaluate();

    protected override void Unbind()
    {
      foreach (var item in _condition)
      {
        item.Unbind();
        item.Changed -= Evaluate;
      }
    }

    protected abstract void OnEvaluated(bool result);

    private void Evaluate()
    {
      if (_condition is null || _condition.Length < 1)
        throw new Exception("Conditions not set");

      var result = _condition[0].Evaluate();
      if (_condition.Length > 1)
      {
        for (var i = 1; i < _condition.Length; i++)
        {
          result = _relation switch
          {
            Relation.And => result && _condition[i].Evaluate(),
            Relation.Or => result || _condition[i].Evaluate(),
            _ => throw new ArgumentOutOfRangeException()
          };
        }
      }

      OnEvaluated(result);
    }
  }
}