using TMPro;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class TextMeshProBinding : TextBindingBase
  {
    [SerializeField] private TextMeshProUGUI _text;

    protected override void SetText(string value) => _text.text = value;

    private void Reset()
    {
      _text = GetComponent<TextMeshProUGUI>();
    }
  }
}