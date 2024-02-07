using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiometriaAPI.Models
{
    public class Fingerprint
    {
        public string NUMINT { get; set; }
        public string Dedo { get; set; }
        public string IsoTemplate { get; set; }
        public string ImagenDedo { get; set; }
        public DateTime? Creado { get; set; }
        public DateTime? Modificado { get; set; }
        public Int16 Mano { get; set; }
    }
}