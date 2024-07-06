namespace JinToliq.Umvvm.ViewModel.Properties
{
  public class IntProperty : Property<int>
  {
    public IntProperty(string name) : base(name) { }
    public IntProperty(string name, int value) : base(name, value) { }
    public IntProperty(string name, int value, bool ignoreDifference) : base(name, value, ignoreDifference) { }
  }
}