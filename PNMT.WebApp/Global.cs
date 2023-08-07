namespace PNMT.WebApp
{
    public static class Global
    {
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
