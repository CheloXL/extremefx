using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Efx.Core.Caching;

namespace Efx.Core.Reflection
{
	public static class MethodInfoExtensions
	{
		private static readonly MemoryLockedCache<MethodInfo, Action<object, object[]>> _delegatesNoReturn = new MemoryLockedCache<MethodInfo, Action<object, object[]>>();
		private static readonly MemoryLockedCache<MethodInfo, Func<object, object[], object>> _delegatesReturn = new MemoryLockedCache<MethodInfo, Func<object, object[], object>>();
		private static readonly Type _typeOfVoid = typeof(void);

		public static void Execute(this MethodInfo methodInfo, object instance, IEnumerable<object> arguments = null)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException(nameof(methodInfo));
			}

			if (instance == null)
			{
				throw new ArgumentNullException(nameof(instance));
			}

			if (methodInfo.ReturnType == _typeOfVoid)
			{
				_delegatesNoReturn.GetOrAdd(methodInfo, CreateWithoutReturn)(instance, arguments == null
					? new object[0]
					: arguments.ToArray());
			}
			else
			{
				var obj = _delegatesReturn.GetOrAdd(methodInfo, CreateWithReturn)(instance, arguments == null
					? new object[0]
					: arguments.ToArray());
			}
		}

		public static T Execute<T>(this MethodInfo methodInfo, object instance, IEnumerable<object> arguments = null)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException(nameof(methodInfo));
			}

			if (instance == null)
			{
				throw new ArgumentNullException(nameof(instance));
			}

			if (!(methodInfo.ReturnType == _typeOfVoid))
			{
				return (T) _delegatesReturn.GetOrAdd(methodInfo, CreateWithReturn)(instance, arguments == null
					? new object[0]
					: arguments.ToArray());
			}

			_delegatesNoReturn.GetOrAdd(methodInfo, CreateWithoutReturn)(instance, arguments == null
				? new object[0]
				: arguments.ToArray());

			return default(T);
		}

		public static bool TryExecute(this MethodInfo methodInfo, object instance, IEnumerable<object> arguments = null)
		{
			if (methodInfo == null)
			{
				return false;
			}

			if (instance == null)
			{
				return false;
			}

			try
			{
				if (methodInfo.ReturnType == _typeOfVoid)
				{
					_delegatesNoReturn.GetOrAdd(methodInfo, CreateWithoutReturn)(instance, arguments == null
						? new object[0]
						: arguments.ToArray());
				}
				else
				{
					_delegatesReturn.GetOrAdd(methodInfo, CreateWithReturn)(instance, arguments == null
						? new object[0]
						: arguments.ToArray());
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static bool TryExecute<T>(this MethodInfo methodInfo, object instance, IEnumerable<object> arguments, out T returnValue)
		{
			returnValue = default(T);

			if (methodInfo == null)
			{
				return false;
			}

			if (instance == null)
			{
				return false;
			}

			try
			{
				if (methodInfo.ReturnType == _typeOfVoid)
				{
					_delegatesNoReturn.GetOrAdd(methodInfo, CreateWithoutReturn)(instance, arguments == null
						? new object[0]
						: arguments.ToArray());

					return true;
				}

				returnValue = (T) _delegatesReturn.GetOrAdd(methodInfo, CreateWithReturn)(instance, arguments == null
					? new object[0]
					: arguments.ToArray());

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal static Action<object, object[]> CreateWithoutReturn(this MethodInfo pMethod)
		{
			var parameterExpression1 = Expression.Parameter(typeof(object));
			var parameterExpression2 = Expression.Parameter(typeof(object[]));

			return Expression.Lambda<Action<object, object[]>>(
				Expression.Convert(
					Expression.Call(
						Expression.Convert(
							parameterExpression1,
							pMethod.DeclaringType
						),
						pMethod,
						CreateParameterExpressions(pMethod, parameterExpression2)
					),
					typeof(void)
				),
				parameterExpression1, parameterExpression2)
				.Compile();
		}

		internal static Func<object, object[], object> CreateWithReturn(this MethodInfo pMethod)
		{
			var parameterExpression1 = Expression.Parameter(typeof(object));
			var parameterExpression2 = Expression.Parameter(typeof(object[]));

			return Expression.Lambda<Func<object, object[], object>>(
				Expression.Convert(
					Expression.Call(
						Expression.Convert(parameterExpression1, pMethod.DeclaringType),
						pMethod,
						CreateParameterExpressions(pMethod, parameterExpression2)),
					typeof(object)),
				parameterExpression1, parameterExpression2)
				.Compile();
		}

		private static Expression[] CreateParameterExpressions(MethodInfo pMethod, Expression pArgumentsParameter)
		{
			return pMethod
				.GetParameters()
				.Select((pParameter, pIndex) => Expression.Convert(
					Expression.ArrayIndex(pArgumentsParameter, Expression.Constant(pIndex)
				), pParameter.ParameterType))
				.ToArray();
		}
	}
}
