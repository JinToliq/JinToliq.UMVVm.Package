using JinToliq.Umvvm.View.Binding.Condition;
using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class ButtonActivityBinding : ConditionBinding
  {
    [SerializeField] private Button _button;

    protected override void OnEvaluated(bool result)
    {
      _button.interactable = result;
    }
  }
}