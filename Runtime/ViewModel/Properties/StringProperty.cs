namespace JinToliq.Umvvm.ViewModel.Properties
{
  public class StringProperty : Property<string>
  {
    public StringProperty(string name) : base(name) { }
    public StringProperty(string name, string value) : base(name, value) { }
    public StringProperty(string name, string value, bool ignoreDifference) : base(name, value, ignoreDifference) { }
  }
}