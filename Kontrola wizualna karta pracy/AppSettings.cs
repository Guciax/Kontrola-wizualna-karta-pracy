using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class AppSettings
    {
        public static void AddOrUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings =  configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public static string GetSettings(string key)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            return configFile.AppSettings.Settings[key].Value;
            //return ConfigurationManager.open.AppSettings[key];
        }

        public static void CheckAppSettingsKeys()
        {
            try
            {
                AppSettings.GetSettings("SprawdzajSerial");
            }
            catch
            {
                AppSettings.AddOrUpdateAppSettings("SprawdzajSerial", "OFF");
            }

            try
            {
                AppSettings.GetSettings("Camera_ON_OFF");
            }
            catch
            {
                AppSettings.AddOrUpdateAppSettings("Camera_ON_OFF", "OFF");
            }

            try
            {
                AppSettings.GetSettings("camera180Rotate");
            }
            catch
            {
                AppSettings.AddOrUpdateAppSettings("camera180Rotate", "OFF");
            }

            try
            {
                AppSettings.GetSettings("AppPath");
            }
            catch
            {
                AppSettings.AddOrUpdateAppSettings("AppPath", @"C:\Kontrola Wzrokowa Karta Pracy 2.0\");
            }

            try
            {
                AppSettings.GetSettings("ImgPath");
            }
            catch
            {
                AppSettings.AddOrUpdateAppSettings("ImgPath", @"P:\Kontrola_Wzrokowa");
            }

            try
            {
                AppSettings.GetSettings("camera180Rotate");
            }
            catch
            {
                AppSettings.AddOrUpdateAppSettings("deviceMonikerString", "");
            }
        }
    }
}
