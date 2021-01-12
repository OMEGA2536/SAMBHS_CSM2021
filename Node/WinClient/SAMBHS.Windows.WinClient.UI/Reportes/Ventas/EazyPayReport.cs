using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Font = iTextSharp.text.Font;
using SAMBHS.Common.BE;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using SAMBHS.Common.BE.Custom;
using SAMBHS.Windows.WinClient.UI.Procesos;

namespace NetPdf
{
    public class EazyPayReport
    {
        private static void RunFile(string filePDF)
        {
            Process proceso = Process.Start(filePDF);
            proceso.WaitForExit();
            proceso.Close();
        }
        public static void CreateReportEazyPayReport(string filePDF, string inicio, string fin, OrganizationDto1 infoEmpresaPropietaria, int systemUserId, List<VentasEazyPay> VentasEazyPayDetalle)
        {
            #region Declaration Tables
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            filePDF = filePDF + "report.pdf";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            document.Open();
            var subTitleBackGroundColor = new BaseColor(System.Drawing.Color.Gray);
            string include = string.Empty;
            List<PdfPCell> cells = null;
            float[] columnWidths = null;
            PdfPTable header2 = new PdfPTable(6);
            header2.HorizontalAlignment = Element.ALIGN_CENTER;
            header2.WidthPercentage = 100;
            float[] widths1 = new float[] { 16.6f, 18.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            header2.SetWidths(widths1);
            PdfPTable companyData = new PdfPTable(6);
            companyData.HorizontalAlignment = Element.ALIGN_CENTER;
            companyData.WidthPercentage = 100;
            float[] widthscolumnsCompanyData = new float[] { 16.6f, 16.6f, 16.6f, 16.6f, 16.6f, 16.6f };
            companyData.SetWidths(widthscolumnsCompanyData);
            PdfPTable filiationWorker = new PdfPTable(4);
            PdfPTable table = null;
            document.Add(new Paragraph("\r\n"));
            var tamaño_celda = 14f;
            var tamaño_celda2 = 70f;


            #endregion

            #region Fonts

            Font fontTitle1 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitle2 = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTable = FontFactory.GetFont("Calibri", 7, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontSubTitle = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
            Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

            Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValue1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
            Font fontColumnValueBold_1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));


            #endregion

            #region Title

            PdfPCell logoEmpresa = null;
            if (infoEmpresaPropietaria.b_Image != null)
                logoEmpresa = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, null, null, 120, 50));
            else
                logoEmpresa = new PdfPCell(new Phrase(" ", fontColumnValue));

            logoEmpresa.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            logoEmpresa.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            
            cells = new List<PdfPCell>()
                 {
                     new PdfPCell(logoEmpresa){Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda2,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                     new PdfPCell(new Phrase("REPORTE DE VENTAS EAZY PAY, DEL "+inicio+" AL "+fin, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                     new PdfPCell(new Phrase("usuario: "+"\r\nFecha y hora de Impresión: ", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                 };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            #endregion

            #region ReportBody

            cells = new List<PdfPCell>()
             {
                 new PdfPCell(new Phrase("Fecha", fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("Cliente", fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("Boucher", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("Monto", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("Comprobante", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

             };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            decimal acumuladorTotal = 0;

            int detalle = 0;
            foreach (var egreso in VentasEazyPayDetalle)
            {

                if (detalle != egreso.IdEazyPaY)
                {

                    var grupo = VentasEazyPayDetalle.FindAll(p => p.IdEazyPaY == egreso.IdEazyPaY).ToList();

                    if (grupo.Count != 0)
                    {

                        cells = new List<PdfPCell>()
                             {
                                 new PdfPCell(new Phrase(egreso.EazyPay + " - " + grupo.Count(), fontColumnValueBold)){Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, BackgroundColor = BaseColor.LIGHT_GRAY, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},  
                             };
                        columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                        document.Add(filiationWorker);
                        decimal acumuladorSubTotal = 0;
                        foreach (var item1 in grupo)
                        {
                            decimal monto = Convert.ToDecimal(item1.Pago);
                            monto = Decimal.Round(monto, 2);
                            if (VentasEazyPayDetalle[0] != null)
                                cells = new List<PdfPCell>()
                             {
                                 new PdfPCell(new Phrase(item1.FechaIngreso.ToString().Split(' ')[0], fontColumnValue)){Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                                 new PdfPCell(new Phrase(item1.NombreCliente, fontColumnValue)){Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                                 new PdfPCell(new Phrase(item1.Comprobante, fontColumnValue)){Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                                 new PdfPCell(new Phrase(monto.ToString(), fontColumnValue)){Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                                 new PdfPCell(new Phrase(item1.Documento, fontColumnValue)){Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                         
                             };
                            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                            document.Add(filiationWorker);
                            acumuladorTotal = acumuladorTotal + monto;
                            acumuladorSubTotal += monto;
                        }

                        cells = new List<PdfPCell>()
                             {
                                 new PdfPCell(new Phrase("SUB TOTAL", fontTitleTable)) {BackgroundColor=BaseColor.LIGHT_GRAY, Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , UseVariableBorders=true, BorderColorRight = BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(acumuladorSubTotal.ToString(), fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                                 new PdfPCell(new Phrase("", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,UseVariableBorders=true, BorderColorLeft = BaseColor.WHITE},    

                             };
                        columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                        filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                        document.Add(filiationWorker);

                    }
                }

                detalle = egreso.IdEazyPaY;

            }


            cells = new List<PdfPCell>()
             {
                 new PdfPCell(new Phrase("TOTAL", fontTitleTable)) {BackgroundColor=BaseColor.LIGHT_GRAY,Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE , UseVariableBorders=true, BorderColorRight = BaseColor.WHITE},    
                 new PdfPCell(new Phrase(acumuladorTotal.ToString(), fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,UseVariableBorders=true, BorderColorLeft = BaseColor.WHITE},    

             };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);

            #endregion

            #region RUN

            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);

            #endregion

        }
    }
}
