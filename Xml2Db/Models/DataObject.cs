using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xml2Db.Models
{
    public class DataObject
    {
        [Key]
        public Guid Ref { get; set; }
    }
}
