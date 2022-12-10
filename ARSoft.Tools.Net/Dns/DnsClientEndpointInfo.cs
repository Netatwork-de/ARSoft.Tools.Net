#region Copyright and License
// Copyright 2010..2022 Alexander Reinert
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

using System.Net;

namespace ARSoft.Tools.Net.Dns
{
	internal class DnsClientEndpointInfo
	{
		public bool IsMulticast { get; }
		public IPAddress LocalAddress { get; }
		public IPAddress ServerAddress { get; }

		public DnsClientEndpointInfo(bool isMulticast, IPAddress serverAddress, IPAddress localAddress)
		{
			IsMulticast = isMulticast;
			ServerAddress = serverAddress;
			LocalAddress = localAddress;
		}
	}
}