using PNMTD.Lib;

namespace PNMT.WebApp.Helper
{
    public class JwtTokenProvider
    {
        public string JwtToken { get; }

        public JwtTokenProvider()
        {
            if(Global.IsDevelopment)
            {
                JwtToken = JwtTokenHelper.GenerateNewToken("dev", "PNMTD", "PNMTD", "Development123456789");
            }
        }
    }
}
