using System.Runtime.InteropServices;
using System.Text.Json;

namespace PNMT.WebApp.Authentification
{
    public class WebAppUserManager
    {

        private string usersDbPath;

        public List<WebAppUser>? Users { get; private set; }

        public int Count()
        {
            if (Users == null) return 0;
            return Users!.Count;
        }

        public int NumberOfAdmins()
        {
            if (Users == null) return 0;
            return Users.Count(u => u.IsAdmin);
        }

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
                
                // Migration Logic
                if (Users.Count == 1
                    && string.IsNullOrEmpty(Users[0].Name)
                    && Users[0].Id == Guid.Empty)
                {
                    var user = Users[0];
                    user.IsAdmin = true;
                    user.Id = Guid.NewGuid();
                    user.Name = user.Username;
                    Save();
                }
            }
            else
            {
                Users = new List<WebAppUser>()
                {
                    WebAppUser.New("default user", "pnmt", "pnmt", true)
                };
                Save();
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

        public WebAppUser GetUserById(Guid id)
        {
            return Users.Single(u => u.Id == id);
        }

        public bool Delete(Guid id)
        {
            return Users.Remove(GetUserById(id));
        }

        public WebAppUser? GetByUsername(string username)
        {
            return Users.SingleOrDefault(u => u.Username == username);
        }
    }
}
