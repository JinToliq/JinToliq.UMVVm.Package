namespace JinToliq.Umvvm.ViewModel.Properties
{
  public class LongProperty : Property<long>
  {
    public LongProperty(string name) : base(name) { }
    public LongProperty(string name, long value) : base(name, value) { }
    public LongProperty(string name, long value, bool ignoreDifference) : base(name, value, ignoreDifference) { }
  }
}