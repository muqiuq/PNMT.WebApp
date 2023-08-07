namespace PNMT.WebApp
{
    public static class Global
    {
        internal static WebApplication WebApp;

        public static string JwtToken { get; set; }

        public static bool IsDevelopment
        {
            get
            {
                return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            }
        }

    }
}
