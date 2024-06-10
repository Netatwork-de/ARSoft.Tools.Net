namespace ARSoft.Tools.Net.Dns
{
	public enum DnsFailureReason
	{
		None,
		NoAuthoritativeResult,
		TooFewLabels,
		QueryLimitReached,
		InternalResolutionFailure,
		ResolveLoop,
		DnsServerError,
		QueryTimeout,
		InvalidQuery,
		HttpsResponseInvalid
	}
}
