using System.Reflection;

namespace PNMT.WebApp
{
    public class FrontendInfo
    {

        public string Version
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        public string Name
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Name ?? "PNMT.WebApp";
            }
        }

    }
}
