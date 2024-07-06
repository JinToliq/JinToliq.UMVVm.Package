using UnityEngine;
using UnityEngine.UI;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class TextBinding : TextBindingBase
  {
    [SerializeField] private Text _text;

    protected override void SetText(string value) => _text.text = value;
  }
}