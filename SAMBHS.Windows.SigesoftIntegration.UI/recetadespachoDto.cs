using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMBHS.Windows.SigesoftIntegration.UI
{
    public class recetadespachoDto
    {
        public int RecetaId { get; set; }
        public int lleva { get; set; }
        public string NombrePaciente { get; set; }
        public string Medicamento { get; set; }
        public string Presentacion { get; set; }
        public string UnidadMedida { get; set; }
        public string Ubicacion { get; set; }
        public string Duracion { get; set; }
        public DateTime FechaFin { get; set; }
        public string Dosis { get; set; }
        public decimal CantidadRecetada { get; set; }
        public string NombreMedico { get; set; }
        public string MedicoNroCmp { get; set; }
        public byte[] RubricaMedico { get; set; }
        public string NombreClinica { get; set; }
        public string DireccionClinica { get; set; }
        public byte[] LogoClinica { get; set; }
        public bool Despacho { get; set; }
        public string MedicinaId { get; set; }
    }
}
