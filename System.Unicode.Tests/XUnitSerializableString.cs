using System.Text;
using Xunit.Abstractions;

namespace System.Unicode.Tests
{
	// This class is needed because apparently, somewhere in the process of unit testing, strings with invalid UTF-16 sequences are "fixed", which totally messes up the tests here.
	// This is just a wrapper over regular strings… Data is serialized as an array of chars instead of a string. This seems to do the trick.
	public class XUnitSerializableString : IEquatable<XUnitSerializableString>, IXunitSerializable
	{
		private string _value;

		public XUnitSerializableString() : this(null) { }

		public XUnitSerializableString(string value)
		{
			_value = value;
		}

		void IXunitSerializable.Deserialize(IXunitSerializationInfo info)
		{
			var chars = info.GetValue<char[]>("Chars");

			_value = chars != null ?
				new string(chars) :
				null;
		}

		void IXunitSerializable.Serialize(IXunitSerializationInfo info)
			=> info.AddValue("Chars", _value?.ToCharArray(), typeof(char[]));

		public override string ToString()
		{
			if (string.IsNullOrEmpty(_value)) return _value;

			var sb = new StringBuilder(_value.Length * 6);

			foreach (char c in _value)
			{
				sb.Append(@"\u")
					.Append(((ushort)c).ToString("X4"));
			}

			return sb.ToString();
		}

		public bool Equals(XUnitSerializableString other) => _value == other._value;
		public override bool Equals(object obj) => obj is XUnitSerializableString && Equals((XUnitSerializableString)obj);
		public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(_value);

		public static implicit operator string(XUnitSerializableString text) => text._value;
		public static implicit operator XUnitSerializableString(string text) => new XUnitSerializableString(text);
	}
}
