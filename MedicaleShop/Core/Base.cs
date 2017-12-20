using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace MedicaleShop.Core
{
    [DataContract]
    public class Base<T> where T : Base<T>, IFileManager
    {
        public static Dictionary<Guid, T> Items = new Dictionary<Guid, T>();

        public Base() : this("")
        {
        }

        public Base(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Items.Add(Id, (T) this);
        }

        public Base(string name, Guid key)
        {
            Id = key;
            Name = name;
            Items.Add(Id, (T) this);
        }

        public static List<T> Indexer => Items.Values.ToList();

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        public bool Load()
        {
            if (!Directory.Exists("Saves"))
                return false;
            var file = typeof (T).Name + ".xml";
            var path = Directory.GetCurrentDirectory();
            if (!File.Exists(path + @"\Saves\" + file))
                return false;
            using (var xmlReader = XmlReader.Create(path + @"\Saves\" + file))
            {
                var dataContractSerializer = new DataContractSerializer(typeof (Dictionary<Guid, T>));
                Items = dataContractSerializer.ReadObject(xmlReader) as Dictionary<Guid, T>;
            }
            return true;
        }

        public void Save()
        {
            var file = typeof (T).Name + ".xml";
            var settings = new XmlWriterSettings {Indent = true};
            var path = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path + @"\" + "Saves"))
                Directory.CreateDirectory(path + @"\" + "Saves");
            path += @"\" + "Saves";
            using (var xmlWriter = XmlWriter.Create(path + @"\" + file, settings))
            {
                var dataContractSerializer = new DataContractSerializer(typeof (Dictionary<Guid, T>));
                dataContractSerializer.WriteObject(xmlWriter, Items);
                xmlWriter.Close();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}