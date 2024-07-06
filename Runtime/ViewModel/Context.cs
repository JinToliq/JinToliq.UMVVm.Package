using System;
using System.Collections;
using System.Collections.Generic;
using JinToliq.Umvvm.ViewModel.Exceptions;

namespace JinToliq.Umvvm.ViewModel
{
  public interface IContext
  {
    void SetParent(IContext parent);
    Property GetProperty(string name);
    TProperty GetProperty<TProperty>(string name) where TProperty : Property;
    Command GetCommand(string name);
    TCommand GetCommand<TCommand>(string name) where TCommand : ICommand;
    void Enable();
    void Disable();
  }

  public interface IContextWithState : IContext
  {
    void SetStateObject(object state);
    object GetStateObject();
  }

  public abstract class Context<TState> : Context, IContextWithState
  {
    private TState _currentState;

    public TState CurrentState
    {
      get => _currentState;
      set
      {
        _currentState = value;
        OnStateChanged();
      }
    }

    public void SetStateObject(object state)
    {
      if (state is null)
        CurrentState = default;

      if (state is TState typedState)
        CurrentState = typedState;
      else
        throw new ArgumentException($"State object is not of type {typeof(TState)}");
    }

    public object GetStateObject() => _currentState;

    protected virtual void OnStateChanged() {}
  }

  public abstract class Context : IContext
  {
    public const char ParentContextMarker = '#';
    public const char ChildContextMarker = '.';

    private IContext _parent;
    private Dictionary<string, Property> _properties;
    private Dictionary<string, ICommand> _commands;
    private Dictionary<string, IContext> _contexts;

    public void SetParent(IContext parent) => _parent = parent;

    public Property GetProperty(string name)
    {
      AssertNullOrEmpty(name);

      if (name[0] == ParentContextMarker)
      {
        if (_parent is null)
          throw new ContextHasNoParent(name);

        return _parent.GetProperty(name[1..]);
      }

      var childMarkerIndex = name.IndexOf(ChildContextMarker);
      if (childMarkerIndex == 0)
        throw new InvalidPropertyName(name, GetType());

      if (childMarkerIndex > 0)
      {
        var contextName = name[..childMarkerIndex];
        AssertRegistered(_contexts, contextName);
        return _contexts[contextName].GetProperty(name[(childMarkerIndex + 1)..]);
      }

      AssertRegistered(_properties, name);
      return _properties[name];
    }

    public TProperty GetProperty<TProperty>(string name) where TProperty : Property
    {
      return GetProperty(name).As<TProperty>();
    }

    public Command GetCommand(string name) => GetCommandInternal(name).As<Command>();

    public TCommand GetCommand<TCommand>(string name) where TCommand : ICommand => GetCommandInternal(name).As<TCommand>();

    public IContext GetChildContext(string name)
    {
      AssertRegistered(_contexts, name);
      return _contexts[name];
    }

    public void Enable()
    {
      OnEnabled();

      if (_contexts is null)
        return;

      foreach (var context in _contexts)
        context.Value.Enable();
    }

    public void Disable()
    {
      OnDisabled();

      if (_contexts is null)
        return;

      foreach (var context in _contexts)
        context.Value.Disable();
    }

    protected virtual void OnEnabled() {}

    protected virtual void OnDisabled() {}

    public virtual void Update() {}

    protected void RegisterProperty(Property property)
    {
      _properties ??= new Dictionary<string, Property>();
      _properties.Add(property.Name, property);
    }

    protected void RegisterProperties(params Property[] properties)
    {
      foreach (var item in properties)
        RegisterProperty(item);
    }

    protected void RegisterCommand(string name, Action action)
    {
      _commands ??= new Dictionary<string, ICommand>();
      _commands.Add(name, new Command(action));
    }

    protected void RegisterCommand<TArg>(string name, Action<TArg> action)
    {
      _commands ??= new Dictionary<string, ICommand>();
      _commands.Add(name, new Command<TArg>(action));
    }

    protected void RegisterContext(string name, IContext context)
    {
      _contexts ??= new Dictionary<string, IContext>();
      _contexts.Add(name, context);
      context.SetParent(this);
    }

    private ICommand GetCommandInternal(string name)
    {
      AssertNullOrEmpty(name);

      if (name[0] == ParentContextMarker)
      {
        if (_parent is null)
          throw new ContextHasNoParent(name);

        return _parent.GetCommand(name[1..]);
      }

      var childMarkerIndex = name.IndexOf(ChildContextMarker);
      if (childMarkerIndex == 0)
        throw new InvalidPropertyName(name, GetType());

      if (childMarkerIndex > 0)
      {
        var contextName = name[..childMarkerIndex];
        AssertRegistered(_contexts, contextName);
        return _contexts[contextName].GetCommand(name[(childMarkerIndex + 1)..]);
      }

      AssertRegistered(_commands, name);
      return _commands[name];
    }

    private void AssertNullOrEmpty(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new InvalidPropertyName(name, GetType());
    }

    private void AssertRegistered(IDictionary dictionary, string name)
    {
      if (dictionary is null)
        throw new InvalidPropertyName(name, GetType());
      if (!dictionary.Contains(name))
        throw new InvalidPropertyName(name, GetType());
    }
  }
}