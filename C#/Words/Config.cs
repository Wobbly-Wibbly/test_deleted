using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace Words
{
    abstract class Config
    {
        static private Configuration wordsConfig = null;
        static private Configuration userConfig = null;

        static public Configuration Current
        {
            get
            {
                if (wordsConfig == null)
                {
                    ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                    configMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "\\words.config";
                    wordsConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                }

                return wordsConfig;
            }
        }

        static public Configuration User
        {
            get
            {
                if (userConfig == null)
                {
                    ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                    configMap.ExeConfigFilename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\words_user.config";
                    userConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                    // check file for NULLs
                    try
                    {
                        userConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                    }
                    catch
                    {
                        File.Delete(configMap.ExeConfigFilename);
                        userConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                    }
                }

                return userConfig;
            }
        }
    }
}
