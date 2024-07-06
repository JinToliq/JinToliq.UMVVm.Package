using JinToliq.Umvvm.View.Binding.Condition;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class ActivityBinding : ConditionBinding
  {
    [SerializeField] private GameObject _target;

    protected override bool AlwaysActiveForChange => true;

    protected override void OnEvaluated(bool result) => _target.SetActive(result);

    private void Reset()
    {
      _target = gameObject;
    }
  }
}