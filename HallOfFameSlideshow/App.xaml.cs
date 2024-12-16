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

            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.config");
            string localConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.local.config");

            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);

            // Load appsettings.local.config if it exists, otherwise use appsettings.config
            
            
            if (File.Exists(localConfigPath))
            {
                OverrideAppSettings(localConfigPath);
            }
            else if (File.Exists(configPath))
            {
                OverrideAppSettings(configPath);
            }
            else
            {
                MessageBox.Show("No configuration file found!");
                Shutdown();
            }
        }

        private void OverrideAppSettings(string filePath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            foreach (XmlNode node in xmlDoc.SelectNodes("configuration/appSettings/add"))
            {
                string key = node.Attributes["key"]?.Value;
                string value = node.Attributes["value"]?.Value;

                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings[key] = value;
                }
            }
        }

    }

}
