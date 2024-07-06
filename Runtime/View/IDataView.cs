using JinToliq.Umvvm.ViewModel;

namespace JinToliq.Umvvm.View
{
  public interface IDataView
  {

    IDataView GetInitialized();
    Property GetProperty(string property);
    TProperty GetProperty<TProperty>(string property) where TProperty : Property;
    Command GetCommand(string property);
    TCommand GetCommand<TCommand>(string property) where TCommand : ICommand;
  }
}