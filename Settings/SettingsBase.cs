using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using LiteDB;
using LiteDB.Engine;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Linq;
using Microsoft.Extensions.Options;
using UAM.VerifyEmployee.Factory;


namespace UAM.VerifyEmployee.Settings
{
    /// <summary>
    /// Base class for load settings
    /// </summary>
    public abstract class SettingsBase<T>
    {
        public static string FileName = "settings.db";
        private SettingsStore _settingsStore = SettingsStore.Instance;
        public static string LottoHome => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "LottoSendPrinter");
        private static string DbFileName => Path.Combine(LottoHome, FileName);

        protected T _currentSettings { get; set; }

        private IOptions<CommonSettings> _options;

        protected SettingsBase()
        {
            var l = new List<int>();
            //_options = options;
        }

        protected void Load(string settingsSection)
        {
            var isInDB = SettingsStore.Instance.HasSettings(settingsSection);
            _currentSettings = _settingsStore.HasSettings(settingsSection)
                ? _settingsStore.GetSettingsFromDB<T>(settingsSection)
                : LoadJson<T>(settingsSection);
        }

        protected void LoadDefault()
        {
            throw new NotImplementedException();
        }

        protected void Save()
        {
            throw new NotImplementedException();
        }

        protected T LoadJson<T>(string fileName)
        {
            T res;
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                res = JsonConvert.DeserializeObject<T>(json);
            }

            return res;
        }

        protected void SaveJson<T>(string fileName, T sourse)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, sourse);
            }
        }

        private static ILiteDatabase CreateDbContext()
        {
            ConnectionString connectionString = new ConnectionString
            {
                Filename = DbFileName,
                Password = "settings_pass",
                Connection = ConnectionType.Shared
            };
            ILiteDatabase db = new LiteDatabase(connectionString);
            db.CheckpointSize = 100;
            db.Rebuild(new RebuildOptions() { Password = connectionString.Password });
            db.Commit();

            return db;
        }


        private class SettingsStore
        {

            private ILiteDatabase _db;

            private SettingsStore()
            {
                _db = CreateDbContext();
            }

            public static SettingsStore Instance => PughSingletone.instance;

            private class PughSingletone
            {
                static PughSingletone()
                {
                }

                internal static readonly SettingsStore instance = new SettingsStore();
            }

            public bool HasSettings(string collectionName)
            {
                _db.BeginTrans();
                var collection = _db.GetCollection<SettingsItem>(collectionName);
                _db.Commit();
                return collection.Count() > 0;
            }

            public TSettings GetSettingsFromDB<TSettings>(string collectionName)
            {
                _db.BeginTrans();
                var collection = _db.GetCollection<SettingsItem>(collectionName);
                var item = collection.FindAll().FirstOrDefault();
                _db.Commit();
                return null == item ? default(TSettings) : JsonConvert.DeserializeObject<TSettings>(item.Json);
            }
            public void SaveSettingsToDB<TSettings>(TSettings settings, string collectionName)
            {
                _db.BeginTrans();
                var collection = _db.GetCollection<SettingsItem>(collectionName);
                var item = collection.FindAll().FirstOrDefault() ?? new SettingsItem { Name = collectionName };
                item.Json = JsonConvert.SerializeObject(settings);
                collection.Upsert(item);
                _db.Commit();
            }

            internal class SettingsItem
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Json { get; set; }
            }

        }
        
    }
}
