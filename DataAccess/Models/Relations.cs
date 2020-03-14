using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataAccess.Models
{

    public enum Relation {
        [Description("Amigos")]
        AM,
        [Description("Familiar")]
        FA,
        [Description("Colegas")]
        CO,
        [Description("Otros")]
        OT
    }
    public class Relations
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int User2Id { get; set; }
        public User User2 { get; set; }

        public Relation Relation { get; set; }
    }


}
