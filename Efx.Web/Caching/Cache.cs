using Efx.Core;

namespace Efx.Web.Caching
{
	public static class Cache
	{
		private static readonly Lazy<ContextCache> _contextCache = new Lazy<ContextCache>(() => new ContextCache(), false);
		private static readonly Lazy<RuntimeCache> _runtimeCache = new Lazy<RuntimeCache>(() => new RuntimeCache(), false);
		private static readonly Lazy<SessionCache> _sessionCache = new Lazy<SessionCache>(() => new SessionCache(), false);

		public static ContextCache Context
		{
			get { return _contextCache.Value; }
		}

		public static RuntimeCache Runtime
		{
			get { return _runtimeCache.Value; }
		}

		public static SessionCache Session
		{
			get { return _sessionCache.Value; }
		}
	}
}
