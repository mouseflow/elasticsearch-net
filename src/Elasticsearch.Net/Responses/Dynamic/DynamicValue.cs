﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

// ReSharper disable ArrangeConstructorOrDestructorBody
// ReSharper disable ArrangeAccessorOwnerBody
// ReSharper disable RemoveRedundantBraces
// ReSharper disable ArrangeMethodOrOperatorBody

namespace Elasticsearch.Net
{
	public class DynamicValue : DynamicObject, IEquatable<DynamicValue>, IConvertible
	{
		private readonly object _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="DynamicValue" /> class.
		/// </summary>
		/// <param name="value">The value to store in the instance</param>
		public DynamicValue(object value) => _value = value;

		/// <summary>
		/// Gets a value indicating whether this instance has value.
		/// </summary>
		/// <value><c>true</c> if this instance has value; otherwise, <c>false</c>.</value>
		/// <remarks><see langword="null" /> is considered as not being a value.</remarks>
		public bool HasValue
		{
			get { return _value != null; }
		}

		public DynamicValue this[string name]
		{
			get
			{
				object r;
				Dispatch(out r, name);
				return (DynamicValue)r;
			}
		}

		public DynamicValue this[int i]
		{
			get
			{
				if (!HasValue)
					return new DynamicValue(null);

				var l = Value as IList;
				if (l != null && l.Count - 1 >= i)
				{
					return new DynamicValue(l[i]);
				}
				return new DynamicValue(null);
			}
		}

		/// <summary>
		/// Gets the inner value
		/// </summary>
		public object Value
		{
			get { return _value; }
		}

		/// <summary>
		/// Returns the <see cref="T:System.TypeCode" /> for this instance.
		/// </summary>
		/// <returns>
		/// The enumerated constant that is the <see cref="T:System.TypeCode" /> of the class or value type that implements this interface.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public TypeCode GetTypeCode()
		{
			if (_value == null) return TypeCode.Empty;

			return Type.GetTypeCode(_value.GetType());
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// A Boolean value equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public bool ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 8-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public byte ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// A Unicode character equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public char ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public DateTime ToDateTime(IFormatProvider provider)
		{
			return Convert.ToDateTime(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public decimal ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A double-precision floating-point number equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public double ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 16-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public short ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 32-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public int ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 64-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public long ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 8-bit signed integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public sbyte ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A single-precision floating-point number equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public float ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting
		/// information.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> instance equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public string ToString(IFormatProvider provider)
		{
			return Convert.ToString(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an
		/// equivalent value, using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this
		/// instance.
		/// </returns>
		/// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		public object ToType(Type conversionType, IFormatProvider provider)
		{
			return Convert.ChangeType(_value, conversionType, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 16-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public ushort ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 32-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public uint ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(_value, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An 64-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		/// <param name="provider">
		/// An <see cref="T:System.IFormatProvider" /> interface implementation that supplies culture-specific formatting
		/// information.
		/// </param>
		/// <filterpriority>2</filterpriority>
		[CLSCompliant(false)]
		public ulong ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(_value, provider);
		}

		/// <summary>
		/// Returns the value as a dictionary if the current value represents an object.
		/// Otherwise returns null.
		/// </summary>
		public IDictionary<string, DynamicValue> ToDictionary()
		{
			if (!(_value is IDictionary<string, object> dict)) return null;
			return DynamicDictionary.Create(dict);
		}


		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="compareValue" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		/// <param name="compareValue">An <see cref="DynamicValue" /> to compare with this instance.</param>
		public bool Equals(DynamicValue compareValue)
		{
			if (ReferenceEquals(null, compareValue))
			{
				return false;
			}

			return ReferenceEquals(this, compareValue) || Equals(compareValue._value, _value);
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			var name = binder.Name;

			return Dispatch(out result, name);
		}


		private bool Dispatch(out object result, string name)
		{
			if (!HasValue)
			{
				result = new DynamicValue(null);
				return true;
			}

			var d = Value as IDictionary<string, object>;
			object r;
			if (d != null && d.TryGetValue(name, out r))
			{
				result = new DynamicValue(r);
				return true;
			}
			var x = Value as IDynamicMetaObjectProvider;
			if (x != null)
			{
				var dm = GetDynamicMember(Value, name);
				result = new DynamicValue(dm);
				return true;
			}
			var ds = Value as IDictionary;
			if (ds != null && ds.Contains(name))
			{
				result = new DynamicValue(ds[name]);
				return true;
			}

			result = new DynamicValue(Value);
			return true;
		}

		private static object GetDynamicMember(object obj, string memberName)
		{
			var binder = Binder.GetMember(CSharpBinderFlags.None, memberName, null,
				new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) });
			var callsite = CallSite<Func<CallSite, object, object>>.Create(binder);
			return callsite.Target(callsite, obj);
		}

		/// <summary>
		/// Returns a default value if Value is null
		/// </summary>
		/// <typeparam name="T">When no default value is supplied, required to supply the default type</typeparam>
		/// <param name="defaultValue">Optional parameter for default value, if not given it returns default of type T</param>
		/// <returns>If value is not null, value is returned, else default value is returned</returns>
		public T Default<T>(T defaultValue = default(T))
		{
			if (HasValue)
			{
				try
				{
					return (T)_value;
				}
				catch
				{
					var typeName = _value.GetType().Name;
					var message = string.Format("Cannot convert value of type '{0}' to type '{1}'",
						typeName, typeof(T).Name);

					throw new InvalidCastException(message);
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// Attempts to convert the value to type of T, failing to do so will return the defaultValue.
		/// </summary>
		/// <typeparam name="T">When no default value is supplied, required to supply the default type</typeparam>
		/// <param name="defaultValue">Optional parameter for default value, if not given it returns default of type T</param>
		/// <returns>If value is not null, value is returned, else default value is returned</returns>
		public T TryParse<T>(T defaultValue = default(T))
		{
			if (HasValue)
			{
				try
				{
					if (_value.GetType().IsAssignableFrom(typeof(T)))
					{
						return (T)_value;
					}

					var type = typeof(T);

					var stringValue = _value as string;
					if (type == typeof(DateTime))
					{
						DateTime result;

						if (DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
						{
							return (T)(object)result;
						}
					}
					else if (stringValue != null)
					{
						var converter = TypeDescriptor.GetConverter(type);
						if (converter.IsValid(stringValue)) return (T)converter.ConvertFromInvariantString(stringValue);
					}
					else if (type == typeof(string))
					{
						return (T)Convert.ChangeType(_value, TypeCode.String, CultureInfo.InvariantCulture);
					}
				}
				catch
				{
					return defaultValue;
				}
			}

			return defaultValue;
		}

		public static bool operator ==(DynamicValue dynamicValue, object compareValue)
		{
			if (dynamicValue._value == null && compareValue == null)
			{
				return true;
			}

			return dynamicValue._value != null && dynamicValue._value.Equals(compareValue);
		}

		public static bool operator !=(DynamicValue dynamicValue, object compareValue)
		{
			return !(dynamicValue == compareValue);
		}

		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to the current <see cref="object" />.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the specified <see cref="object" /> is equal to the current <see cref="DynamicValue" />; otherwise,
		/// <c>false</c>.
		/// </returns>
		/// <param name="compareValue">The <see cref="object" /> to compare with the current <see cref="DynamicValue" />.</param>
		public override bool Equals(object compareValue)
		{
			if (ReferenceEquals(null, compareValue))
			{
				return false;
			}

			if (ReferenceEquals(this, compareValue)
				|| ReferenceEquals(_value, compareValue)
				|| Equals(_value, compareValue)
			)
			{
				return true;
			}

			return compareValue.GetType() == typeof(DynamicValue) && Equals((DynamicValue)compareValue);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current instance.</returns>
		public override int GetHashCode()
		{
			return _value != null ? _value.GetHashCode() : 0;
		}

		/// <summary>
		/// Provides implementation for binary operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override
		/// this method to specify dynamic behavior for operations such as addition and multiplication.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns <c>false</c>, the run-time binder of
		/// the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		/// <param name="binder">
		/// Provides information about the binary operation. The binder.Operation property returns an
		/// <see cref="T:System.Linq.Expressions.ExpressionType" /> object. For example, for the sum = first + second statement, where first and second
		/// are derived from the DynamicObject class, binder.Operation returns ExpressionType.Add.
		/// </param>
		/// <param name="arg">
		/// The right operand for the binary operation. For example, for the sum = first + second statement, where first and second
		/// are derived from the DynamicObject class, <paramref name="arg" /> is equal to second.
		/// </param>
		/// <param name="result">The result of the binary operation.</param>
		public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
		{
			object resultOfCast;
			result = null;

			if (binder.Operation != ExpressionType.Equal)
			{
				return false;
			}

			var convert =
				Binder.Convert(CSharpBinderFlags.None, arg.GetType(), typeof(DynamicValue));

			if (!TryConvert((ConvertBinder)convert, out resultOfCast))
			{
				return false;
			}

			result = resultOfCast == null ? Equals(arg, resultOfCast) : resultOfCast.Equals(arg);

			return true;
		}

		/// <summary>
		/// Provides implementation for type conversion operations. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can
		/// override this method to specify dynamic behavior for operations that convert an object from one type to another.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the operation is successful; otherwise, <c>false</c>. If this method returns <c>false</c>, the run-time binder of
		/// the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
		/// </returns>
		/// <param name="binder">
		/// Provides information about the conversion operation. The binder.Type property provides the type to which the object
		/// must be converted. For example, for the statement (String)sampleObject in C# (CType(sampleObject, Type) in Visual Basic), where
		/// sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Type returns the
		/// <see cref="T:System.String" /> type. The binder.Explicit property provides information about the kind of conversion that occurs. It returns
		/// true for explicit conversion and false for implicit conversion.
		/// </param>
		/// <param name="result">The result of the type conversion operation.</param>
		public override bool TryConvert(ConvertBinder binder, out object result)
		{
			result = null;

			if (_value == null)
			{
				return true;
			}

			var binderType = binder.Type;
			if (binderType == typeof(string))
			{
				result = Convert.ToString(_value);
				return true;
			}

			if (binderType == typeof(Guid) || binderType == typeof(Guid?))
			{
				Guid guid;
				if (Guid.TryParse(Convert.ToString(_value), out guid))
				{
					result = guid;
					return true;
				}
			}
			else if (binderType == typeof(TimeSpan) || binderType == typeof(TimeSpan?))
			{
				TimeSpan timespan;
				if (TimeSpan.TryParse(Convert.ToString(_value), out timespan))
				{
					result = timespan;
					return true;
				}
			}
			else
			{
				if (binderType.IsGenericType && binderType.GetGenericTypeDefinition() == typeof(Nullable<>))
					binderType = binderType.GetGenericArguments()[0];

				var typeCode = Type.GetTypeCode(binderType);

				if (typeCode == TypeCode.Object)
				{
					if (binderType.IsAssignableFrom(_value.GetType()))
					{
						result = _value;
						return true;
					}
					else
						return false;
					}

				result = Convert.ChangeType(_value, typeCode);
				return true;
			}
			return base.TryConvert(binder, out result);
		}

		public override string ToString()
		{
			return _value == null ? base.ToString() : Convert.ToString(_value);
		}

		public static implicit operator bool(DynamicValue dynamicValue)
		{
			if (!dynamicValue.HasValue) return false;

			if (dynamicValue._value.GetType().IsValueType) return Convert.ToBoolean(dynamicValue._value);

			if (bool.TryParse(dynamicValue.ToString(CultureInfo.InvariantCulture), out var result)) return result;

			return true;
		}

		public static implicit operator string(DynamicValue dynamicValue)
		{
			return dynamicValue.HasValue
				? Convert.ToString(dynamicValue._value)
				: null;
		}

		public static implicit operator int(DynamicValue dynamicValue)
		{
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToInt32(dynamicValue._value);

			return int.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator Guid(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is Guid)
			{
				return (Guid)dynamicValue._value;
			}

			return Guid.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator DateTime(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is DateTime)
			{
				return (DateTime)dynamicValue._value;
			}

			return DateTime.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator TimeSpan(DynamicValue dynamicValue)
		{
			if (dynamicValue._value is TimeSpan)
			{
				return (TimeSpan)dynamicValue._value;
			}

			return TimeSpan.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator long(DynamicValue dynamicValue)
		{
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToInt64(dynamicValue._value);

			return long.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator float(DynamicValue dynamicValue)
		{
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToSingle(dynamicValue._value);

			return float.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator decimal(DynamicValue dynamicValue)
		{
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToDecimal(dynamicValue._value);

			return decimal.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}

		public static implicit operator double(DynamicValue dynamicValue)
		{
			if (dynamicValue._value.GetType().IsValueType) return Convert.ToDouble(dynamicValue._value);

			return double.Parse(dynamicValue.ToString(CultureInfo.InvariantCulture));
		}
	}
}
