namespace JinToliq.Umvvm.ViewModel.Properties
{
  public class BoolProperty : Property<bool>
  {
    public BoolProperty(string name) : base(name) { }
    public BoolProperty(string name, bool value) : base(name, value) { }
    public BoolProperty(string name, bool value, bool ignoreDifference) : base(name, value, ignoreDifference) { }
  }
}