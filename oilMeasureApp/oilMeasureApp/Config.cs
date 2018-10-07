using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace oilMeasure
{
    class Config
    {
        /* *
         * config:App.config
         * */
        public Config(){
        }

        //read parameter from config.
        public string getValue(string key){
            string myvalue = ConfigurationManager.AppSettings[key];
            return myvalue;
        }
        
        //write key-value to config.
        public Boolean writeKeyValue(string key, string value) {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings[key].Value = value;
            cfg.Save();
            ConfigurationManager.RefreshSection("appSettings");

            return true;
        }


    }
}
