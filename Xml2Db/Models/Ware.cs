using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xml2Db.Models
{
    public class Ware : CatalogObject
    {
        public Color Color { get; set; }
        public Origin Origin { get; set; }

        [XmlElement("ЦветТовара")]
        public Guid ColorId { get; set; }

        [XmlElement("СтранаТовара")]
        public Guid OriginId { get; set; }
    }
}
