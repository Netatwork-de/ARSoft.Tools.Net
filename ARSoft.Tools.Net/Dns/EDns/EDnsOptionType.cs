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
	///   ENDS Option types
	/// </summary>
	public enum EDnsOptionType : ushort
	{
		/// <summary>
		///   <para>Update Lease</para>
		///   <para>
		///     Defined in
		///     <a href="http://files.dns-sd.org/draft-sekar-dns-llq.txt">draft-sekar-dns-llq</a>.
		///   </para>
		/// </summary>
		LongLivedQuery = 1,

		/// <summary>
		///   <para>Update Lease</para>
		///   <para>
		///     Defined in
		///     <a href="http://files.dns-sd.org/draft-sekar-dns-ul.txt">draft-sekar-dns-ul</a>.
		///   </para>
		/// </summary>
		UpdateLease = 2,

		/// <summary>
		///   <para>Name server ID</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc5001.html">RFC 5001</a>.
		///   </para>
		/// </summary>
		NsId = 3,

		/// <summary>
		///   <para>Owner</para>
		///   <para>
		///     Defined in
		///     <a href="http://tools.ietf.org/html/draft-cheshire-edns0-owner-option">draft-cheshire-edns0-owner-option</a>.
		///   </para>
		/// </summary>
		Owner = 4,

		/// <summary>
		///   <para>DNSSEC Algorithm Understood</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc6975.html">RFC 6975</a>.
		///   </para>
		/// </summary>
		DnssecAlgorithmUnderstood = 5,

		/// <summary>
		///   <para>DS Hash Understood</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc6975.html">RFC 6975</a>.
		///   </para>
		/// </summary>
		DsHashUnderstood = 6,

		/// <summary>
		///   <para>NSEC3 Hash Understood</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc6975.html">RFC 6975</a>.
		///   </para>
		/// </summary>
		Nsec3HashUnderstood = 7,

		/// <summary>
		///   <para>Client Subnet</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc7871.html">RFC 7871</a>.
		///   </para>
		/// </summary>
		ClientSubnet = 8,

		/// <summary>
		///   <para>Expire EDNS Option</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc7314.html">RFC 7314</a>.
		///   </para>
		/// </summary>
		Expire = 9,

		/// <summary>
		///   <para>Cookie Option</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc7873.html">RFC 7873</a>.
		///   </para>
		/// </summary>
		Cookie = 10,

		/// <summary>
		///   <para>edns-tcp-keepalive EDNS0 Option</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc7828.html">RFC 7828</a>.
		///   </para>
		/// </summary>
		TcpKeepAlive = 11,

		/// <summary>
		///   <para>The EDNS(0) Padding Option</para>
		///   <para>
		///     Defined in
		///     <a href="https://www.rfc-editor.org/rfc/rfc7830.html">RFC 7830</a>.
		///   </para>
		/// </summary>
		Padding = 12,
	}
}