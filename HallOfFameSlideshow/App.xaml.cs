using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Xml;

namespace HallOfFameSlideshow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string localSettingsPath = "appsettings.local.config";
            if (File.Exists(localSettingsPath))
            {
                ExternallyLoadAppSettings(localSettingsPath);
            }
        }

        private void ExternallyLoadAppSettings(string filePath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            foreach (XmlNode node in xmlDoc.SelectNodes("configuration/appSettings/add"))
            {
                string key = node.Attributes["key"]?.Value;
                string value = node.Attributes["value"]?.Value;

                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings.Set(key, value);
                }
            }
        }

    }

}
