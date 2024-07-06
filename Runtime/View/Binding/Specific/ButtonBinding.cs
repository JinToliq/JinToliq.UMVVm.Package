using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class ButtonBinding : CommandBinding
  {
    [SerializeField] private Button _button;

    protected override void Bind()
    {
      base.Bind();
      _button.onClick.AddListener(Invoke);
    }

    protected override void Unbind()
    {
      base.Unbind();
      _button.onClick.RemoveListener(Invoke);
    }

#if UNITY_EDITOR
    private void Reset()
    {
      _button = GetComponent<Button>();
    }
#endif
  }
}