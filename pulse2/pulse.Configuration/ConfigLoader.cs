﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace pulse.Configuration
{
    public class ConfigLoader<T> where T : new()
    {
        private static ConfigLoader<T> _instance;
        private static object _sync = new object();

        public static ConfigLoader<T> Resolve(string path)
        {
            lock (_sync)
            {
                return _instance ?? (_instance = new ConfigLoader<T>(path));
            }
        }

        public static T Instance
        {
            get { return _instance.Load(); }
        } 

        private readonly Dictionary<string, PropertyInfo> _configMaps;
        private readonly string _path;
        private bool _loaded;
        private readonly T _config;

        private ConfigLoader(string path)
        {
            _configMaps = new Dictionary<string, PropertyInfo>();
            _config = new T();
            _path = path;
        }

        public T Load()
        {
            if (_loaded || string.IsNullOrEmpty(_path) || !File.Exists(_path))
                return _config;

            foreach (var property in typeof (T).GetProperties())
            {
                _configMaps.Add(property.Name.ToLower(), property);
            }

            try
            {
                using (var reader = new StreamReader(_path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!line.Contains("="))
                            continue;

                        var split = line.Split('=');

                        var cmd = split[0].ToLower();
                        var val = split[1];

                        var prop = _configMaps[cmd];

                        prop.SetValue(_config, Convert.ChangeType(val, prop.PropertyType));
                    }
                }
            }
            catch (Exception)
            {
                //TODO: logging
            }

            _loaded = true;
            return _config;
        }
    }
}
