using System;

namespace RGSoft.Linq
{

	public static class SafetyInvokeExtension
	{

		/// <summary>
		/// Call the method represented by the current delegate.
		/// <remarks>If the delegate is null it does not do anything.</remarks>
		/// </summary>
		/// <param name="delegate">delegate</param>
		public static void SafetyInvoke(this Action @delegate)
		{
#if NET_4_6
			@delegate?.Invoke();
#else
			if (@delegate == null) return;
			@delegate.Invoke();
#endif
		}

		/// <summary>
		/// Call the method represented by the current delegate.
		/// <para>If the delegate is null it does not do anything.</para>
		/// </summary>
		/// <typeparam name="TArg">delegate argument type</typeparam>
		/// <param name="delegate">delegate</param>
		/// <param name="arg">delegate argument</param>
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

}

