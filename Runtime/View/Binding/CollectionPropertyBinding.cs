using JinToliq.Umvvm.ViewModel;

namespace JinToliq.Umvvm.View.Binding
{
  public abstract class CollectionPropertyBinding : SinglePropertyBinding
  {
    protected override void OnBeforeChange(Property property)
    { }

    protected override void OnChanged(Property property)
    { }
  }

  public class CollectionPropertyBinding<TValue> : SinglePropertyBinding<TValue>
  {
    protected override void OnBeforeChange(Property<TValue> property)
    { }

    protected override void OnChanged(Property<TValue> property)
    { }
  }
}