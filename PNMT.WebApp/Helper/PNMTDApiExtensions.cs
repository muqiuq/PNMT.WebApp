using PNMT.ApiClient.Data;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace PNMT.WebApp.Helper
{
    public static class PNMTDApiExtensions
    {

        public static void Update(this object obj)
        {
            var scope = Global.WebApp.Services.CreateScope();
            try
            {
                var api = scope.ServiceProvider.GetService<PNMTDApi>();

                var fields = typeof(PNMTDApi).GetFields();
                foreach (var field in fields)
                {
                    if (field.FieldType.BaseType.Namespace == "PNMT.ApiClient.Data" &&
                        field.FieldType.BaseType.Name.StartsWith("ApiCrud") &&
                        field.FieldType.BaseType.GenericTypeArguments.First() == obj.GetType())
                    {
                        var updateMethod = field.FieldType.BaseType.GetMethod("Update");
                        var apiCrud = field.GetValue(api);
                        var task = (Task) updateMethod.Invoke(apiCrud, new object[] { obj });
                        task.Wait();
                        break;
                    }
                }
            }finally {
                scope.Dispose();
            }
        }
    }
}
