using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Script.Exception;
using UnityEngine;

namespace Script.Config
{
    public static class JsonConfig
    {
        public static T Load<T>(string fileName)
        {
            var jsonFile = Application.streamingAssetsPath + "/json/" + fileName + ".json";
            string json = File.ReadAllText(jsonFile, Encoding.UTF8);

            if (string.IsNullOrEmpty(json))
            {
                throw new ConfigException($"Load Json file [{jsonFile}] failed : content empty");
            }

            var obj = JsonUtility.FromJson<T>(json);
            if (obj == null)
            {
                throw new ConfigException($"Load Json file [{jsonFile}] failed : parse error");
            }

            Debug.Log($"Load Config : [{jsonFile}] {json}");
            return obj;
        }
    }
}