using System;
using System.Collections.Generic;
using System.Text;

namespace Xml2Db.Models
{
    public class CatalogObject : DataObject
    {
        public bool DeletionMark { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
