using System;
using ModelsInterfaces;

namespace Models.DbModels
{
    public class Aktion : IAktion
    {
        //for nlste opdatering
        public int Id { get; set; }
        public Guid PublicIdentifier { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}