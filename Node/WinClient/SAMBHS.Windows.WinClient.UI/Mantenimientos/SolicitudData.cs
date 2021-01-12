using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Portable.Components;

namespace SAMBHS.Windows.WinClient.UI.Mantenimientos
{
    public class SolicitudData
    {
        public string dniSolicitante { get; set; }
        public string NombreSolicitante { get; set; }
        public string parentesco { get; set; }
        public string phoneSolicitante { get; set; }
        public string emailSolicitante { get; set; }
        public bool historia { get; set; }
        public bool examenes { get; set; }
        public bool certificado { get; set; }
        public bool informe { get; set; }
        public bool otros { get; set; }
        public string otrosDescripcion { get; set; }
        public string proposito { get; set; }
        public string nroHistoria { get; set; }
    }
}
