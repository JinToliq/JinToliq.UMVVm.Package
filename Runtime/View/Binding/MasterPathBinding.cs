using System;
using UnityEngine;

namespace JinToliq.Umvvm.View.Binding
{
  public class MasterPathBinding : MonoBehaviour
  {
    [SerializeField] private string _path;

    public string Path => _path;

    public void ValidatePath()
    {
      if (string.IsNullOrEmpty(_path))
        throw new Exception("Path is not set");
    }
  }
}