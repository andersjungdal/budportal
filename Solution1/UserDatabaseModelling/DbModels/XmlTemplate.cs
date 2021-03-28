using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ModelsInterfaces;

namespace DatabaseModelling.DbModels
{
    public class XmlTemplate : IXmlTemplate
    {
        public int Id { get; set; }
        public Guid PublicIdentifire { get; set; }
        [Column(TypeName = "xml")]
        public string XMLTemplate { get; set; }
    }
}
