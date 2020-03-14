using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataAccess.Models
{

    public enum DocumentType {
        [Description("Adulto sin identidad")]
        AS,
        [Description("Cédula de ciudadanía")]
        CC,
        [Description("Cédula de extranjería")]
        CE,
        [Description("Menor sin identificación")]
        MS,
        [Description("Pasaporte")]
        PA,
        [Description("Registro Civil")]
        RC,
        [Description("Tarjeta de identidad.")]
        TI
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public ICollection<Relations> Relations { get; set; }
    }

}
