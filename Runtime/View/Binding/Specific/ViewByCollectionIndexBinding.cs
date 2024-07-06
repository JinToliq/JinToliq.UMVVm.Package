using System.Collections;
using UnityEngine;
using JinToliq.Umvvm.ViewModel;

namespace JinToliq.Umvvm.View.Binding.Specific
{
  public class ViewByCollectionIndexBinding : CollectionPropertyBinding
  {
    [SerializeField] private DataView _view;
    [SerializeField] private int _index;

    protected override void OnChanged(Property property)
    {
      var value = property.GetValue();
      if (value is not IList collection || collection.Count <= _index)
      {
        _view.ClearState();
        return;
      }

      var state = collection[_index];
      _view.SetState(state);
    }
  }
}