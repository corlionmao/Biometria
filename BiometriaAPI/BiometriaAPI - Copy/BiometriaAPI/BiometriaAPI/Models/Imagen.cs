using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiometriaAPI.Models
{
    public class Imagen
    {
        public string numint { get; set; }
        public int tipo { get; set; }
        public string imagenAsBase64 { get; set; }
        public string imagenDataUrl { get; set; }
        public string imagenData { get; set; }
    }
}