﻿// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleUninitializer
// Assembly: MCenters, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45CFC87E-86C0-4035-8A46-F8737ED6CA8B
// Assembly location: C:\Users\Misi\Downloads\akshnav_3.exe

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

#nullable disable
namespace \u003CCrtImplementationDetails\u003E
{
  internal class ModuleUninitializer : Stack
  {
    private static object @lock = new object();
    internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();

    [SecuritySafeCritical]
    internal void AddHandler(EventHandler handler)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        Monitor.Enter(ModuleUninitializer.@lock, ref lockTaken);
        RuntimeHelpers.PrepareDelegate((Delegate) handler);
        this.Push((object) handler);
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ModuleUninitializer.@lock);
      }
    }

    [SecurityCritical]
    static ModuleUninitializer()
    {
    }

    [SecuritySafeCritical]
    private ModuleUninitializer()
    {
      EventHandler eventHandler = new EventHandler(this.SingletonDomainUnload);
      AppDomain.CurrentDomain.DomainUnload += eventHandler;
      AppDomain.CurrentDomain.ProcessExit += eventHandler;
    }

    [SecurityCritical]
    [PrePrepareMethod]
    private void SingletonDomainUnload(object source, EventArgs arguments)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        RuntimeHelpers.PrepareConstrainedRegions();
        Monitor.Enter(ModuleUninitializer.@lock, ref lockTaken);
        foreach (EventHandler eventHandler in (Stack) this)
          eventHandler(source, arguments);
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ModuleUninitializer.@lock);
      }
    }
  }
}
