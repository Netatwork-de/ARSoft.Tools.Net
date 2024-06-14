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
	///   <para>SSH key fingerprint record</para>
	///   <para>
	///     Defined in
	///     <a href="https://www.rfc-editor.org/rfc/rfc4255.html">RFC 4255</a>.
	///   </para>
	/// </summary>
	public class SshFpRecord : DnsRecordBase
	{
		/// <summary>
		///   Algorithm of the fingerprint
		/// </summary>
		public enum SshFpAlgorithm : byte
		{
			/// <summary>
			///   None
			/// </summary>
			None = 0,

			/// <summary>
			///   <para>RSA</para>
			///   <para>
			///     Defined in
			///     <a href="https://www.rfc-editor.org/rfc/rfc4255.html">RFC 4255</a>.
			///   </para>
			/// </summary>
			Rsa = 1,

			/// <summary>
			///   <para>DSA</para>
			///   <para>
			///     Defined in
			///     <a href="https://www.rfc-editor.org/rfc/rfc4255.html">RFC 4255</a>.
			///   </para>
			/// </summary>
			Dsa = 2,

			/// <summary>
			///   <para>ECDSA</para>
			///   <para>
			///     Defined in
			///     <a href="https://www.rfc-editor.org/rfc/rfc6594.html">RFC 6594</a>.
			///   </para>
			/// </summary>
			EcDsa = 3,

			/// <summary>
			///   <para>Ed25519</para>
			///   <para>
			///     Defined in
			///     <a href="https://www.rfc-editor.org/rfc/rfc7479.html">RFC 7479</a>.
			///   </para>
			/// </summary>
			Ed25519 = 4,
		}

		/// <summary>
		///   Type of the fingerprint
		/// </summary>
		public enum SshFpFingerPrintType : byte
		{
			/// <summary>
			///   None
			/// </summary>
			None = 0,

			/// <summary>
			///   <para>SHA-1</para>
			///   <para>
			///     Defined in
			///     <a href="https://www.rfc-editor.org/rfc/rfc4255.html">RFC 4255</a>.
			///   </para>
			/// </summary>
			Sha1 = 1,

			/// <summary>
			///   <para>SHA-1</para>
			///   <para>
			///     Defined in
			///     <a href="https://www.rfc-editor.org/rfc/rfc6594.html">RFC 6594</a>.
			///   </para>
			/// </summary>
			Sha256 = 2,
		}

		/// <summary>
		///   Algorithm of fingerprint
		/// </summary>
		public SshFpAlgorithm Algorithm { get; }

		/// <summary>
		///   Type of fingerprint
		/// </summary>
		public SshFpFingerPrintType FingerPrintType { get; }

		/// <summary>
		///   Binary data of the fingerprint
		/// </summary>
		public byte[] FingerPrint { get; private set; }

		internal SshFpRecord(DomainName name, RecordType recordType, RecordClass recordClass, int timeToLive, IList<byte> resultData, int currentPosition, int length)
			: base(name, recordType, recordClass, timeToLive)
		{
			Algorithm = (SshFpAlgorithm) resultData[currentPosition++];
			FingerPrintType = (SshFpFingerPrintType) resultData[currentPosition++];
			FingerPrint = DnsMessageBase.ParseByteData(resultData, ref currentPosition, length - 2);
		}

		internal SshFpRecord(DomainName name, RecordType recordType, RecordClass recordClass, int timeToLive, DomainName origin, string[] stringRepresentation)
			: base(name, recordType, recordClass, timeToLive)
		{
			if (stringRepresentation.Length < 3)
				throw new FormatException();

			Algorithm = (SshFpAlgorithm) Byte.Parse(stringRepresentation[0]);
			FingerPrintType = (SshFpFingerPrintType) Byte.Parse(stringRepresentation[1]);
			FingerPrint = String.Join("", stringRepresentation.Skip(2)).FromBase16String();
		}

		/// <summary>
		///   Creates a new instance of the SshFpRecord class
		/// </summary>
		/// <param name="name"> Name of the record </param>
		/// <param name="timeToLive"> Seconds the record should be cached at most </param>
		/// <param name="algorithm"> Algorithm of fingerprint </param>
		/// <param name="fingerPrintType"> Type of fingerprint </param>
		/// <param name="fingerPrint"> Binary data of the fingerprint </param>
		public SshFpRecord(DomainName name, int timeToLive, SshFpAlgorithm algorithm, SshFpFingerPrintType fingerPrintType, byte[] fingerPrint)
			: base(name, RecordType.SshFp, RecordClass.INet, timeToLive)
		{
			Algorithm = algorithm;
			FingerPrintType = fingerPrintType;
			FingerPrint = fingerPrint ?? new byte[] { };
		}

		internal override string RecordDataToString()
		{
			return (byte) Algorithm
			       + " " + (byte) FingerPrintType
			       + " " + FingerPrint.ToBase16String();
		}

		protected internal override int MaximumRecordDataLength => 2 + FingerPrint.Length;

		protected internal override void EncodeRecordData(IList<byte> messageData, ref int currentPosition, Dictionary<DomainName, ushort>? domainNames, bool useCanonical)
		{
			messageData[currentPosition++] = (byte) Algorithm;
			messageData[currentPosition++] = (byte) FingerPrintType;
			DnsMessageBase.EncodeByteArray(messageData, ref currentPosition, FingerPrint);
		}
	}
}