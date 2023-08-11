namespace PNMTD.Helper
{
    public static class LogManager
    {

        internal static WebApplication App;

        public static ILogger<T> _CreateLogger<T>()
        {
            if (App == null) { return null; }
            return App.Services.GetRequiredService<ILogger<T>>();
        }

        public static ILogger<T> CreateLogger<T>()
        {
            var l = _CreateLogger<T>();
            if (l != null) return l;

            var tempFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            return tempFactory.CreateLogger<T>();
        }

    }
}
