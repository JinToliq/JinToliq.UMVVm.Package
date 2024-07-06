namespace JinToliq.Umvvm.ViewModel.Properties
{
  public class FloatProperty : Property<float>
  {
    public FloatProperty(string name) : base(name) { }
    public FloatProperty(string name, float value) : base(name, value) { }
    public FloatProperty(string name, float value, bool ignoreDifference) : base(name, value, ignoreDifference) { }
  }
}