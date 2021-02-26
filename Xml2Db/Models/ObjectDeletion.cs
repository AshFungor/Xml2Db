using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xml2Db.Models
{
    [XmlRoot(Namespace = "http://v8.1c.ru/data")]
    public class ObjectDeletion
    {
        public Reference Ref { get; set; }
    }
}
