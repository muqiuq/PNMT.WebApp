using PNMT.WebApp.Authentification;
using System.CommandLine;
using System.Security;

namespace PNMT.WebAppCli
{
    internal class Program
    {
        static int Main(string[] args)
        {
            WebAppUserManager userManager = new WebAppUserManager();

            var rootCommand = new RootCommand("PNMTD Tool");

            var usernameOption = new Option<string>(
               name: "--username",
               description: "Username");

            var adduserCommand = new Command("adduser", "Add users to users.json")
            {
                usernameOption,
            };

            var checkPasswordCommand = new Command("checkpassword", "check credentials for user")
            {
                usernameOption,
            };

            rootCommand.AddCommand(adduserCommand);
            rootCommand.AddCommand(checkPasswordCommand);

            adduserCommand.SetHandler((username) =>
            {
                if (username == null)
                {
                    Console.WriteLine("Missing options");
                    return;
                }
                var webAppUser = new WebAppUser();
                webAppUser.Username = username;
                Console.Write("Passwort: ");
                var pw1 = GetPassword();
                Console.WriteLine();
                Console.Write("Passwort repeat: ");
                var pw2 = GetPassword();
                Console.WriteLine();
                if(pw1 != pw2)
                {
                    Console.WriteLine("Passwords do not match!");
                    return;
                }
                webAppUser.SetPassword(pw1);
                userManager.AddOrUpdateUser(webAppUser);
                userManager.Save();
            },
            usernameOption);

            checkPasswordCommand.SetHandler((username) =>
            {
                if (username == null)
                {
                    Console.WriteLine("Missing options");
                    return;
                }

                Console.Write("Passwort: ");
                var pw1 = GetPassword();
                Console.WriteLine();

                if(userManager.CheckCredentials(username, pw1))
                {
                    Console.WriteLine("Username & Passwort OK");
                }
                else
                {
                    Console.WriteLine("Username or passwort mismatch");
                }
            },
            usernameOption);

            return rootCommand.InvokeAsync(args).Result;
        }

        public static string GetPassword()
        {
            string pwd = "";
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd = pwd.Substring(0, pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (i.KeyChar != '\u0000') // KeyChar == '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
                {
                    pwd = pwd + i.KeyChar;
                    Console.Write("*");
                }
            }
            return pwd;
        }
    }
}