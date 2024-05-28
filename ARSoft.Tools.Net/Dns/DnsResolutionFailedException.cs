namespace ARSoft.Tools.Net.Dns
{
	public class DnsResolutionFailedException : Exception
	{
		public DnsFailureReason Reason { get; }
		public ReturnCode? ReturnCode { get; }
		public DomainName DnsName { get; }

		public DnsResolutionFailedException(DnsFailureReason reason, DomainName dnsName): base(GetDescription(reason))
		{
			Reason = reason;
			DnsName = dnsName;
		}

		public DnsResolutionFailedException(ReturnCode returnCode, DomainName dnsName): base($"The DNS resolution failed with error {returnCode}.")
		{
			ReturnCode = returnCode;
			Reason = DnsFailureReason.DnsServerError;
			DnsName = dnsName;
		}

		private static string GetDescription(DnsFailureReason reason) =>
			reason switch
			{
				DnsFailureReason.NoAuthoritativeResult => "Response of best known server is not authoritative and has no referrals.",
				DnsFailureReason.TooFewLabels => "Encoding of records with less labels than RrSigRecord is not allowed.",
				DnsFailureReason.QueryLimitReached => "Query limit reached without authoritive answer.",
				DnsFailureReason.InternalResolutionFailure => "The resolution failed due to an internal error.",
				DnsFailureReason.ResolveLoop => "A resolve loop was detected.",
				_ => "DNS resolution failed for an unknown reason."
			};
	}
}
