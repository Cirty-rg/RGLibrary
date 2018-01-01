using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SafetyInvokeExtension {


    public static void SafetyInvoke(this Action @delegate)
    {
#if NET_4_6
        @delegate?.Invoke();
#else
        if (@delegate == null) return;
        @delegate.Invoke();
#endif
    }

    public static void SafetyInvoke<TArg>(this Action<TArg> @delegate, TArg arg)
    {
#if NET_4_6
        @delegate?.Invoke(arg);
#else
        if (@delegate == null) return;
        @delegate.Invoke(arg);
#endif
    }
}
