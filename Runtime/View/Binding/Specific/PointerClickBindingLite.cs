using UnityEngine.EventSystems;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class PointerClickBindingLite : CommandBinding, IPointerClickHandler
  {
    public void OnPointerClick(PointerEventData eventData) => Invoke();
  }
}