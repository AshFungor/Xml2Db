using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Xml2Db.Models
{
    [XmlRoot(Namespace = "http://v8.1c.ru/data")]
    [XmlInclude(typeof(OriginReference))]
    [XmlInclude(typeof(ColorReference))]
    [XmlInclude(typeof(WareReference))]
    [XmlInclude(typeof(ArrivalReference))]
    [XmlInclude(typeof(SellReference))]
    public class Reference
    {
        [Key]
        [XmlText]
        public Guid Value { get; set; }
    }
}
