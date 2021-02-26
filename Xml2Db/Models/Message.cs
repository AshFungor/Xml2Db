using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xml2Db.Models
{
    [XmlRoot(Namespace = "http://v8.1c.ru/messages")]
    public class Message
    {
        [XmlArray()]
        [XmlArrayItem("ObjectDeletion", typeof(ObjectDeletion), Namespace = "http://v8.1c.ru/data")]
        [XmlArrayItem("CatalogObject.СтраныТоваров", typeof(Origin), Namespace = "")]
        [XmlArrayItem("CatalogObject.ЦветаТовара", typeof(Color), Namespace = "")]
        [XmlArrayItem("CatalogObject.Товары", typeof(Ware), Namespace = "")]
        [XmlArrayItem("AccumulationRegisterRecordSet.ОстаткиТоваров", typeof(RecordSet), Namespace = "")]
        public List<object> Body { get; set; }
    }
}
