using System;
using ModelsInterfaces;

namespace Models.DbModels
{
    public class Company : ICompany
    {
        //Change PublicIdentifier in next opdate
        public int Id { get; set; }
        public Guid PublicIdentifier { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Road { get; set; }
        public string StreetNumber { get; set; }
    }
}