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

namespace ARSoft.Tools.Net.Dns
{
	/// <summary>
	///   <para>ISDN address</para>
	///   <para>
	///     Defined in
	///     <a href="https://www.rfc-editor.org/rfc/rfc1183.html">RFC 1183</a>.
	///   </para>
	/// </summary>
	public class IsdnRecord : DnsRecordBase
	{
		/// <summary>
		///   ISDN number
		/// </summary>
		public string IsdnAddress { get; private set; }

		/// <summary>
		///   Sub address
		/// </summary>
		public string SubAddress { get; private set; }

		internal IsdnRecord(DomainName name, RecordType recordType, RecordClass recordClass, int timeToLive, IList<byte> resultData, int currentPosition, int length)
			: base(name, recordType, recordClass, timeToLive)
		{
			int endPosition = currentPosition + length;

			IsdnAddress = DnsMessageBase.ParseText(resultData, ref currentPosition);
			SubAddress = (currentPosition < endPosition) ? DnsMessageBase.ParseText(resultData, ref currentPosition) : String.Empty;
		}

		internal IsdnRecord(DomainName name, RecordType recordType, RecordClass recordClass, int timeToLive, DomainName origin, string[] stringRepresentation)
			: base(name, recordType, recordClass, timeToLive)
		{
			if (stringRepresentation.Length > 2)
				throw new FormatException();

			IsdnAddress = stringRepresentation[0];
			SubAddress = stringRepresentation.Length > 1 ? stringRepresentation[1] : String.Empty;
		}

		/// <summary>
		///   Creates a new instance of the IsdnRecord class
		/// </summary>
		/// <param name="name"> Name of the record </param>
		/// <param name="timeToLive"> Seconds the record should be cached at most </param>
		/// <param name="isdnAddress"> ISDN number </param>
		public IsdnRecord(DomainName name, int timeToLive, string isdnAddress)
			: this(name, timeToLive, isdnAddress, String.Empty) { }

		/// <summary>
		///   Creates a new instance of the IsdnRecord class
		/// </summary>
		/// <param name="name"> Name of the record </param>
		/// <param name="timeToLive"> Seconds the record should be cached at most </param>
		/// <param name="isdnAddress"> ISDN number </param>
		/// <param name="subAddress"> Sub address </param>
		public IsdnRecord(DomainName name, int timeToLive, string isdnAddress, string subAddress)
			: base(name, RecordType.Isdn, RecordClass.INet, timeToLive)
		{
			IsdnAddress = isdnAddress ?? String.Empty;
			SubAddress = subAddress ?? String.Empty;
		}

		internal override string RecordDataToString()
		{
			return IsdnAddress.ToMasterfileLabelRepresentation()
			       + (String.IsNullOrEmpty(SubAddress) ? String.Empty : " " + SubAddress.ToMasterfileLabelRepresentation());
		}

		protected internal override int MaximumRecordDataLength => 2 + IsdnAddress.Length + SubAddress.Length;

		protected internal override void EncodeRecordData(IList<byte> messageData, ref int currentPosition, Dictionary<DomainName, ushort>? domainNames, bool useCanonical)
		{
			DnsMessageBase.EncodeTextBlock(messageData, ref currentPosition, IsdnAddress);
			DnsMessageBase.EncodeTextBlock(messageData, ref currentPosition, SubAddress);
		}
	}
}