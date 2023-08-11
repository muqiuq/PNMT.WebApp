using System.Runtime.InteropServices;
using System.Text.Json;

namespace PNMT.WebApp.Authentification
{
    public class WebAppUserManager
    {

        private string usersDbPath;

        public List<WebAppUser>? Users { get; private set; }

        public WebAppUserManager()
        {
            var path = "";
            if (!Global.IsDevelopment)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    path = Path.Join(Path.Combine(GlobalConfiguration.LinuxBasePath));
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "PNMT");
                }
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            }
            usersDbPath = System.IO.Path.Join(path, "users.json");
            if(File.Exists(usersDbPath))
            {
                Users = JsonSerializer.Deserialize<List<WebAppUser>>(File.ReadAllText(usersDbPath));
            }
            else
            {
                Users = new List<WebAppUser>();
            }
        }

        public void AddOrUpdateUser(WebAppUser user)
        {
            if(Users.Where(u => u.Username == user.Username).Any())
            {
                var existingUser = Users.Where(u => u.Username == user.Username).Single();
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.Salt = user.Salt;
            }
            else
            {
                Users.Add(user);
            }
        }

        public bool CheckCredentials(string username, string password)
        {
            var user = Users.Where(u => u.Username == username).SingleOrDefault();
            if (user == null) return false;
            return user.CheckPassword(password);
        }

        public void Save()
        {
            File.WriteAllText(usersDbPath, JsonSerializer.Serialize(Users));
        }
    }
}
