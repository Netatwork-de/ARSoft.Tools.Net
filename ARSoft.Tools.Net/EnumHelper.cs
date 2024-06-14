﻿#region Copyright and License
// Copyright 2010..2024 Alexander Reinert
// 
// This file is part of the ARSoft.Tools.Net - C# DNS client/server and SPF Library (https://github.com/alexreinert/ARSoft.Tools.Net)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARSoft.Tools.Net
{
	internal static class EnumHelper<T>
		where T : struct, Enum
	{
		private static readonly Dictionary<T, string> _names;
		private static readonly Dictionary<string, T> _values;

		static EnumHelper()
		{
			string[] names = Enum.GetNames<T>();
			T[] values = Enum.GetValues<T>();

			_names = new Dictionary<T, string>(names.Length);
			_values = new Dictionary<string, T>(names.Length * 2);

			for (int i = 0; i < names.Length; i++)
			{
				_names[values[i]] = names[i];
				_values[names[i]] = values[i];
				_values[names[i].ToLowerInvariant()] = values[i];
			}
		}

		public static bool TryParse(string s, bool ignoreCase, out T value)
		{
			if (String.IsNullOrEmpty(s))
			{
				value = default;
				return false;
			}

			return _values.TryGetValue((ignoreCase ? s.ToLowerInvariant() : s), out value);
		}

		public static string ToString(T value)
		{
			return _names.TryGetValue(value, out var res) ? res : Convert.ToInt64(value).ToString();
		}

		public static Dictionary<T, string> Names => _names;

		internal static T Parse(string s, bool ignoreCase, T defaultValue)
		{
			return TryParse(s, ignoreCase, out var res) ? res : defaultValue;
		}

		internal static T Parse(string s, bool ignoreCase)
		{
			if (TryParse(s, ignoreCase, out var res))
				return res;

			throw new ArgumentOutOfRangeException(nameof(s));
		}
	}

	internal static class EnumHelper
	{
		public static bool IsAnyOf<T>(this T value, params T[] valuesToCheck)
			where T : struct, Enum
		{
			return valuesToCheck.Any(x => value.Equals(x));
		}
	}
}