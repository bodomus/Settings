using System;
using System.IO;
using Newtonsoft.Json;
using LiteDB;
using LiteDB.Engine;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;


namespace LottoSheli.SendPrinter.Settings.Settings
{
    /// <summary>
    /// Base class for load settings
    /// </summary>
    public abstract class SettingsBase<T>
    {
        protected abstract string SectionName { get; }
        protected static string DbName = "settings.db";
        private readonly SettingsStore _settingsStore = SettingsStore.Instance;
        public static string LottoHome => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "LottoSendPrinter");
        private static string DbFileName => Path.Combine(LottoHome, DbName);
        private string DefaultSettingsFile => Path.Combine(_settingsDir, $"{SectionName}.json");
        protected virtual string _settingsDir => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Settings");
        //protected virtual string _settingsFile => Path.Combine(_settingsDir, $"{JsonFileName}.json");
        private string _settingsSection;
        public T CurrentSettings { get; set; }
        private IConfiguration _configuration;

        public SettingsBase(IConfiguration configuration)
        {
            _configuration = configuration;
            Load();
        }

        protected void Load()
        {
            CurrentSettings = SettingsStore.Instance.HasSettings(SectionName)
                ? SettingsStore.Instance.GetSettingsFromDB<T>(SectionName)
                : LoadJson<T>(DefaultSettingsFile);
        }

        protected void SaveData(T sett)
        {
            _settingsStore.SaveSettingsToDB(sett, SectionName);
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
                return null == item ? default : JsonConvert.DeserializeObject<TSettings>(item.Json);
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

    public class SettingsHelper
    {

    }
}
