using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JinToliq.Umvvm.ViewModel;
using UnityEngine;

namespace JinToliq.Umvvm.View
{
  public abstract class SelfContainedViewManager : BaseViewManager
  {
    protected virtual void Awake()
    {
      SelfContainedViewModel.Instance.StateOpened += OpenUi;
      SelfContainedViewModel.Instance.StateClosed += CloseUi;
    }

    protected virtual void OnDestroy()
    {
      SelfContainedViewModel.Instance.StateOpened -= OpenUi;
      SelfContainedViewModel.Instance.StateClosed -= CloseUi;
    }
  }
}