﻿using Newtonsoft.Json;

namespace Infrastructure.Security
{
    public class ApplicationPermission
    {
        public string Mnemonic { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        private static Dictionary<string, ApplicationPermission> Permissions { get; set; }

        static ApplicationPermission()
        {
            Permissions = new Dictionary<string, ApplicationPermission>();
        }

        internal ApplicationPermission(string mnemonic, string name, string description)
        {
            Mnemonic = mnemonic;
            Name = name;
            Description = description;
        }

        public static List<ApplicationPermission> GetAllPermissions()
        {
            return Permissions.Values.ToList<ApplicationPermission>();
        }

        public static ApplicationPermission GetPermission(string mnemonic)
        {
            return Permissions[mnemonic];
        }

        public static void ReadPermissions(string permissionsJson)
        {
            Permissions = new Dictionary<string, ApplicationPermission>();
            List<PermissionJsonConfig> permissionsJsonObject = JsonConvert.DeserializeObject<List<PermissionJsonConfig>>(permissionsJson);

            if (permissionsJsonObject == null)
                throw new ArgumentException("Could not read the permissions in the JSON configuration file");

            foreach (var permission in permissionsJsonObject)
            {
                ApplicationPermission objPermission = new ApplicationPermission(permission.Mnemonic, permission.Name,
                    permission.Description);

                Permissions.Add(objPermission.Mnemonic, objPermission);
            }
        }
    }
    internal class PermissionJsonConfig
    {
        public string Mnemonic { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public PermissionJsonConfig()
        {
            Mnemonic = "";
            Name = "";
            Description = "";
        }
    }
}
