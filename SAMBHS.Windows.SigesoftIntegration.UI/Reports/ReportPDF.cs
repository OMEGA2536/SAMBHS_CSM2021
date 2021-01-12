using iTextSharp.text;
using iTextSharp.text.pdf;
using SAMBHS.Windows.SigesoftIntegration.UI;
using SAMBHS.Windows.SigesoftIntegration.UI.Dtos;
using SAMBHS.Windows.SigesoftIntegration.UI.Reports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SAMBHS.Common.BE;
using SAMBHS.Common.DataModel;
using SAMBHS.Windows.WinClient.UI.Procesos;
using SAMBHS.Common.Resource;

namespace NetPdf
{
    public class ReportPDF
    {
        #region Test

        public static void CreateTest(string filePDF)
        {
            // step 1: creation of a document-object
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

            try
            {
                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

                //create an instance of your PDFpage class. This is the class we generated above.
                pdfPage page = new pdfPage();
               
                // step 3: we open the document
                document.Open();
                // step 4: we Add content to the document
                // we define some fonts

                #region Fonts

                Font fontTitle1 = FontFactory.GetFont("Calibri", 14, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitle2 = FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontTitleTable = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontSubTitle = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

                Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueBold = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                Font fontColumnValue1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));


                #endregion

                #region Title

                Paragraph cTitle = new Paragraph("Examen Clínico", fontTitle1);
                Paragraph cTitle2 = new Paragraph("Historia Clínica: ", fontTitle2);
                cTitle.Alignment = Element.ALIGN_CENTER;
                cTitle2.Alignment = Element.ALIGN_CENTER;

                document.Add(cTitle);
                document.Add(cTitle2);

                #endregion

                // step 5: we close the document
                document.Close();
                writer.Close();
                writer.Dispose();
                RunFile(filePDF);

            }
            catch (DocumentException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }

        }

         #endregion


        
        public static void CreateCuadreCaja(string filePDF, string inicio, string fin,
            OrganizationDto1 infoEmpresaPropietaria, int systemUserId)
        {
           
            
                #region Declaration Tables
                // step 1: creation of a document-object
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
                //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                document.SetPageSize(iTextSharp.text.PageSize.A4);
                //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

               
                    // step 2: we create a writer that listens to the document
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

                    //create an instance of your PDFpage class. This is the class we generated above.
                    pdfPage page = new pdfPage();
                    writer.PageEvent = page;
                    // step 3: we open the document
                    document.Open();
                    // step 4: we Add content to the document
                    // we define some fonts
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

                 #region Conexion SAM
                 ConexionSAM conectasam = new ConexionSAM();
                 conectasam.opensam();
                 
                 #endregion

                 #region Fecha
                 string[] fechaInicio = inicio.Split(' ');
                 string[] fecha1 = fechaInicio[0].Split('/');
                 string anioinicio_i = fecha1[2];
                 string mesinicio_i = fecha1[1];
                 string diainicio_i = fecha1[0];

                 string[] fechaFin = fin.Split(' ');
                 string[] fecha2 = fechaFin[0].Split('/');
                 string anioinicio_f = fecha2[2];
                 string mesinicio_f = fecha2[1];
                 string diainicio_f = fecha2[0];

                 DateTime localDate = DateTime.Now;
                 #endregion

                 #region Query Tablas
                 var cadena = "SELECT V.v_SerieDocumento as SERIE, V.v_CorrelativoDocumento as CORRELATIVO, VD.v_DescripcionProducto as CONCEPTO, " +
                              "CL.v_RazonSocial as CLIENTE,CL.v_PrimerNombre as Nombre1,	CL.v_SegundoNombre as Nombre2,	CL.v_ApePaterno as Paterno, " +
                              "CL.v_ApeMaterno as Materno,VD.d_Cantidad as CANTIDAD,VD.d_Precio as PRECIO_UNITARIO, VD.d_Valor as COSTO, " +
                              "VD.d_Igv as IGV, CD.d_ImporteSoles as TOTAL, DH.v_Value1 as CONDICION, DH2.v_Value1 as TIPO , SU.v_UserName as USUARIO, V.i_ClienteEsAgente as TIPO_VENTA " +
                              "From venta V " +
                              "Inner Join ventadetalle VD " +
                              "ON V.v_IdVenta=VD.v_IdVenta " +
                              "Inner Join cliente CL " +
                              "ON V.v_IdCliente = CL.v_IdCliente " +
                              "Left Join cobranzadetalle CD " +
                              "ON CD.v_IdVenta = V.v_IdVenta " +
                              "Left Join datahierarchy DH " +
                              "ON DH.i_GroupId=23 and DH.i_ItemId=V.i_IdCondicionPago " +
                              "Left Join datahierarchy DH2 " +
                              "ON DH2.i_GroupId=46 and DH2.i_ItemId=CD.i_IdFormaPago " +
                              "Inner Join systemuser SU ON SU.i_SystemUserId=V.i_InsertaIdUsuario " +
                              "WHERE ((Year(V.t_InsertaFecha)>=" + anioinicio_i + " and Month(V.t_InsertaFecha)>=" + mesinicio_i +
                              " and Day(V.t_InsertaFecha)>=" + diainicio_i + ") and (Year(V.t_InsertaFecha)<=" + anioinicio_f + " and Month(V.t_InsertaFecha)<=" + mesinicio_f +
                              " and Day(V.t_InsertaFecha)<=" + diainicio_f + ") ) and V.i_Eliminado= 0 and V.i_ClienteEsAgente is not null and V.i_InsertaIdUsuario=" + systemUserId + " order by V.i_ClienteEsAgente, V.v_SerieDocumento";
            
                var cadenaSA = "SELECT V.v_SerieDocumento as SERIE, V.v_CorrelativoDocumento as CORRELATIVO, VD.v_DescripcionProducto as CONCEPTO, " +
                                   "CL.v_RazonSocial as CLIENTE,CL.v_PrimerNombre as Nombre1,	CL.v_SegundoNombre as Nombre2,	CL.v_ApePaterno as Paterno, " +
                                   "CL.v_ApeMaterno as Materno,VD.d_Cantidad as CANTIDAD,VD.d_Precio as PRECIO_UNITARIO, VD.d_Valor as COSTO, " +
                                   "VD.d_Igv as IGV, CD.d_ImporteSoles as TOTAL, DH.v_Value1 as CONDICION, DH2.v_Value1 as TIPO , SU.v_UserName as USUARIO, V.i_ClienteEsAgente as TIPO_VENTA " +
                                   "From venta V " +
                                   "Inner Join ventadetalle VD " +
                                   "ON V.v_IdVenta=VD.v_IdVenta " +
                                   "Inner Join cliente CL " +
                                   "ON V.v_IdCliente = CL.v_IdCliente " +
                                   "Left Join cobranzadetalle CD " +
                                   "ON CD.v_IdVenta = V.v_IdVenta " +
                                   "Left Join datahierarchy DH " +
                                   "ON DH.i_GroupId=23 and DH.i_ItemId=V.i_IdCondicionPago " +
                                   "Left Join datahierarchy DH2 " +
                                   "ON DH2.i_GroupId=46 and DH2.i_ItemId=CD.i_IdFormaPago " +
                                   "Inner Join systemuser SU ON SU.i_SystemUserId=V.i_InsertaIdUsuario " +
                                   "WHERE ((Year(V.t_InsertaFecha)>=" + anioinicio_i + " and Month(V.t_InsertaFecha)>=" + mesinicio_i +
                                   " and Day(V.t_InsertaFecha)>=" + diainicio_i + ") and (Year(V.t_InsertaFecha)<=" + anioinicio_f + " and Month(V.t_InsertaFecha)<=" + mesinicio_f +
                                   " and Day(V.t_InsertaFecha)<=" + diainicio_f + ") ) and V.i_Eliminado= 0 and V.i_ClienteEsAgente is not null and V.i_ClienteEsAgente != 3 and V.i_InsertaIdUsuario != 2036 order by V.i_ClienteEsAgente, V.v_SerieDocumento";

                var cadenaFarmacia = "SELECT V.v_SerieDocumento as SERIE, V.v_CorrelativoDocumento as CORRELATIVO, VD.v_DescripcionProducto as CONCEPTO, " +
                               "CL.v_RazonSocial as CLIENTE,CL.v_PrimerNombre as Nombre1,	CL.v_SegundoNombre as Nombre2,	CL.v_ApePaterno as Paterno, " +
                               "CL.v_ApeMaterno as Materno,VD.d_Cantidad as CANTIDAD,VD.d_Precio as PRECIO_UNITARIO, VD.d_Valor as COSTO, " +
                               "VD.d_Igv as IGV, CD.d_ImporteSoles as TOTAL, DH.v_Value1 as CONDICION, DH2.v_Value1 as TIPO , SU.v_UserName as USUARIO, V.i_ClienteEsAgente as TIPO_VENTA " +
                               "From venta V " +
                               "Inner Join ventadetalle VD " +
                               "ON V.v_IdVenta=VD.v_IdVenta " +
                               "Inner Join cliente CL " +
                               "ON V.v_IdCliente = CL.v_IdCliente " +
                               "Left Join cobranzadetalle CD " +
                               "ON CD.v_IdVenta = V.v_IdVenta " +
                               "Left Join datahierarchy DH " +
                               "ON DH.i_GroupId=23 and DH.i_ItemId=V.i_IdCondicionPago " +
                               "Left Join datahierarchy DH2 " +
                               "ON DH2.i_GroupId=46 and DH2.i_ItemId=CD.i_IdFormaPago " +
                               "Inner Join systemuser SU ON SU.i_SystemUserId=V.i_InsertaIdUsuario " +
                               "WHERE ((Year(V.t_InsertaFecha)>=" + anioinicio_i + " and Month(V.t_InsertaFecha)>=" + mesinicio_i +
                               " and Day(V.t_InsertaFecha)>=" + diainicio_i + ") and (Year(V.t_InsertaFecha)<=" + anioinicio_f + " and Month(V.t_InsertaFecha)<=" + mesinicio_f +
                               " and Day(V.t_InsertaFecha)<=" + diainicio_f + ") ) and V.i_Eliminado= 0 and V.i_ClienteEsAgente is not null and V.i_ClienteEsAgente = 3 order by V.i_ClienteEsAgente, V.v_SerieDocumento";

                 #endregion

                 #region Title
                PdfPCell logoEmpresa = null;
                if (infoEmpresaPropietaria.b_Image != null)
                    logoEmpresa = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, null, null, 120, 50));
                else
                    logoEmpresa = new PdfPCell(new Phrase(" ", fontColumnValue));

                logoEmpresa.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                logoEmpresa.VerticalAlignment = PdfPCell.ALIGN_MIDDLE; 
                
            SqlCommand comandou = null;
                 if (systemUserId == 1)
                     comandou = new SqlCommand(cadenaSA, connection: conectasam.conectarsam);
                 else
                     comandou = new SqlCommand(cadena, connection: conectasam.conectarsam);

                 SqlDataReader lectoru = comandou.ExecuteReader();
                 var usuario = "";
                 while (lectoru.Read())
                 {
                     if (systemUserId == 1)
                         usuario = "ADMINISTRADOR DEL SISTEMA";
                     else if(systemUserId == 2036)
                         usuario = "BERTHA ISABELA YZARRA CASTAÑEDA";
                     else
                         usuario = lectoru.GetValue(15).ToString();
                 }
                 cells = new List<PdfPCell>()
                 {
                     new PdfPCell(logoEmpresa){Rowspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda2,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                     new PdfPCell(new Phrase("CUADRE DE CAJA ", fontTitleTable)) {Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                     new PdfPCell(new Phrase("usuario: "+usuario+"\r\n "+"\r\n  Cuadre de la Fecha: "+diainicio_i+" - "+mesinicio_i+" - "+anioinicio_i+"\r\n "+"\r\n AL: "+diainicio_f+" - "+mesinicio_f+" - "+anioinicio_f + "\r\n "+"\r\n Fecha y hora de Impresión: "+localDate, fontColumnValueBold)) {Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                 };
                 columnWidths = new float[] { 30f, 40f, 30f };
                 filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                 document.Add(filiationWorker);
                 lectoru.Close();
                 #endregion

                 #region Reporte
                  if (conectasam.conectarsam != null)
                     {
                        #region EGRESOS
                         SqlCommand comando = null;
                         if (systemUserId == 1)
                             comando = new SqlCommand(cadenaSA, connection: conectasam.conectarsam);
                         else if (systemUserId == 2036 )
                             comando = new SqlCommand(cadenaFarmacia, connection: conectasam.conectarsam);
                         else
                             comando = new SqlCommand(cadena, connection: conectasam.conectarsam);

                         SqlDataReader lector = comando.ExecuteReader();
                         
                         cells = new List<PdfPCell>()
                         {         
                             new PdfPCell(new Phrase("EGRESOS ", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase("Itm", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Documento", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Descripción", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cliente", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cantidad", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Precio Unitario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Total", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Venta", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             //new PdfPCell(new Phrase("IGV", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Condición", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Tipo", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Usuario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                         };
                         columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                         table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                         document.Add(table);
                         int count = 1; decimal total = 0;
                         while (lector.Read())
                         {
                             var val = lector.GetValue(12).ToString() == "" ? "0" : lector.GetValue(12).ToString();

                             if (lector.GetValue(0).ToString() == "ECO" || lector.GetValue(0).ToString() == "ECA" || lector.GetValue(0).ToString() == "ECF" )
                             {
                                 decimal eco_a_1_1 = decimal.Round(decimal.Parse(lector.GetValue(9).ToString()), 2);
                                 decimal eco_a_2_1 = decimal.Round(decimal.Parse(val), 2);
                                 cells = new List<PdfPCell>()
                                 { 
                                     new PdfPCell(new Phrase(count.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(lector.GetValue(0).ToString()+" - "+lector.GetValue(1).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(lector.GetValue(2).ToString().Split('-')[0], fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(lector.GetValue(3).ToString()+lector.GetValue(4).ToString()+" "+lector.GetValue(5).ToString()+" "+lector.GetValue(6).ToString()+" "+lector.GetValue(7).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(double.Parse(lector.GetValue(8).ToString()).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(eco_a_1_1.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(eco_a_2_1.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(lector.GetValue(16).ToString() == "1"?"OCUP":lector.GetValue(16).ToString()=="2"?"ASIS":lector.GetValue(16).ToString()=="3"?"FAR":lector.GetValue(16).ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     //new PdfPCell(new Phrase("- - -", fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                     new PdfPCell(new Phrase(lector.GetValue(13).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                                     new PdfPCell(new Phrase(lector.GetValue(14).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                                     new PdfPCell(new Phrase(lector.GetValue(15).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 };
                                 count++; total =decimal.Round(decimal.Parse(val) * -1 + total , 2);
                                 columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                                 table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                                 document.Add(table);
                             }


                         }

                         
                         cells = new List<PdfPCell>()
                         {         
                             new PdfPCell(new Phrase(" TOTAL EGRESOS:", fontColumnValueBold)){ Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase(total.ToString(), fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase(" ", fontColumnValueBold)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                         };
                         columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                         table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                         document.Add(table);
                         

                         #endregion

                        #region INGRESOS CONTADO EFECTIVO
                         cells = new List<PdfPCell>()
                         {         
                             new PdfPCell(new Phrase("INGRESOS CONTADO EFECTIVO", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase("Itm", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Documento", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Descripción", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cliente", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cantidad", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Precio Unitario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Total", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Venta", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             //new PdfPCell(new Phrase("IGV", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Condición", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Tipo", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Usuario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                         };
                         columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                         table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                         document.Add(table);
                         lector.Close();
                     }
            
                     SqlCommand comando1 = null;
                     if (systemUserId == 1)
                         comando1 = new SqlCommand(cadenaSA, connection: conectasam.conectarsam);
                     else if (systemUserId == 2036 )
                         comando1 = new SqlCommand(cadenaFarmacia, connection: conectasam.conectarsam);
                     else
                         comando1 = new SqlCommand(cadena, connection: conectasam.conectarsam);
                     SqlDataReader lector1 = comando1.ExecuteReader();
                     int count1 = 1; decimal total1 = 0;
                     while (lector1.Read())
                     {
                         var val = lector1.GetValue(12).ToString() == "" ? "0" : lector1.GetValue(12).ToString();
                         if (lector1.GetValue(0).ToString() != "ECO" && lector1.GetValue(0).ToString() != "ECA" && lector1.GetValue(0).ToString() != "ECF" && (lector1.GetValue(13).ToString() == "CONTADO" || lector1.GetValue(13).ToString() == "PAGO MIXTO") && lector1.GetValue(14).ToString() == "EFECTIVO SOLES" || lector1.GetValue(0).ToString() == "ICO" || lector1.GetValue(0).ToString() == "ICA" || lector1.GetValue(0).ToString() == "ICF" || lector1.GetValue(14).ToString() == "EFECTIVO SOLES")
                         {
                             decimal eco_a_1_2 = decimal.Round(decimal.Parse(lector1.GetValue(9).ToString()),2);
                             decimal eco_a_2_2 = decimal.Round(decimal.Parse(val), 2);

                             cells = new List<PdfPCell>()
                            { 
                             new PdfPCell(new Phrase(count1.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector1.GetValue(0).ToString()+" - "+lector1.GetValue(1).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector1.GetValue(2).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector1.GetValue(3).ToString()+lector1.GetValue(4).ToString()+" "+lector1.GetValue(5).ToString()+" "+lector1.GetValue(6).ToString()+" "+lector1.GetValue(7).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(double.Parse(lector1.GetValue(8).ToString()).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(eco_a_1_2.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(eco_a_2_2.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector1.GetValue(16).ToString() == "1"?"OCUP":lector1.GetValue(16).ToString()=="2"?"ASIS":lector1.GetValue(16).ToString()=="3"?"FAR":lector1.GetValue(16).ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             //new PdfPCell(new Phrase(double.Parse(lector1.GetValue(11).ToString()).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector1.GetValue(13).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                             new PdfPCell(new Phrase(lector1.GetValue(14).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                             new PdfPCell(new Phrase(lector1.GetValue(15).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                         };
                             count1++; total1 = decimal.Round(decimal.Parse(val) + total1, 2);
                             columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                             table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                             document.Add(table);
                         }

                     }
                     cells = new List<PdfPCell>()
                        {         
                        new PdfPCell(new Phrase(" TOTAL CONTADO EFECTIVO:", fontColumnValueBold)){ Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        new PdfPCell(new Phrase(total1.ToString(), fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        new PdfPCell(new Phrase(" ", fontColumnValueBold)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                        };
                     columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                     table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                     document.Add(table);


                        #endregion

                        #region TOTAL A ENTREGAR
                     lector1.Close();
                     SqlCommand comando4 = null;
                     if (systemUserId == 1)
                         comando4 = new SqlCommand(cadenaSA, connection: conectasam.conectarsam);
                     else if (systemUserId == 2036 )
                         comando4 = new SqlCommand(cadenaFarmacia, connection: conectasam.conectarsam);
                     else
                         comando4 = new SqlCommand(cadena, connection: conectasam.conectarsam);
                     SqlDataReader lector4 = comando4.ExecuteReader();
                     decimal egreso = 0;
                     decimal ingreso = 0;
                     decimal entregar;
                     while (lector4.Read())
                     {
                         var val = lector4.GetValue(12).ToString() == "" ? "0" : lector4.GetValue(12).ToString();
                         if (lector4.GetValue(0).ToString() == "ECO" || lector4.GetValue(0).ToString() == "ECA" || lector4.GetValue(0).ToString() == "ECF")
                         {
                             egreso = decimal.Round(decimal.Parse(val) + egreso , 2);
                         }
                         else if (lector4.GetValue(0).ToString() != "ECO" && lector4.GetValue(0).ToString() != "ECA" && lector4.GetValue(0).ToString() != "ECF" && ((lector4.GetValue(13).ToString() == "CONTADO" && lector4.GetValue(14).ToString() == "EFECTIVO SOLES") || (lector4.GetValue(13).ToString() == "PAGO MIXTO" && lector4.GetValue(14).ToString() == "EFECTIVO SOLES")))
                         {
                             ingreso = decimal.Round(decimal.Parse(val) + ingreso ,2);
                         }
                     }

                     entregar = decimal.Round(ingreso - egreso,2);
                     cells = new List<PdfPCell>()
                     {         
                         new PdfPCell(new Phrase("VENTA EN EFECTIVO ", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                         new PdfPCell(new Phrase(" TOTAL A ENTREGAR: "+ingreso+" - "+egreso+" = ", fontTitleTable)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                         new PdfPCell(new Phrase(entregar.ToString(), fontTitleTable)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                         new PdfPCell(new Phrase(" ", fontColumnValueBold)){ Colspan =8, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                         //new PdfPCell(new Phrase("  ", fontTitleTable)){ Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                     };
                     columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                     table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                     document.Add(table);
                    #endregion

                        #region INGRESOS CREDITO
                     cells = new List<PdfPCell>()
                        {         
                             new PdfPCell(new Phrase("INGRESOS CRÉDITO", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Itm", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Documento", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Descripción", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cliente", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cantidad", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Precio Unitario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Total", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Venta", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             //new PdfPCell(new Phrase("IGV", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Condición", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Tipo", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Usuario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                        };
                     columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                     table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                     document.Add(table);
                     lector4.Close();

                     SqlCommand comando2 = null;
                     if (systemUserId == 1)
                         comando2 = new SqlCommand(cadenaSA, connection: conectasam.conectarsam);
                     else if (systemUserId == 2036 )
                         comando2 = new SqlCommand(cadenaFarmacia, connection: conectasam.conectarsam);
                     else
                         comando2 = new SqlCommand(cadena, connection: conectasam.conectarsam);
                     SqlDataReader lector2 = comando1.ExecuteReader();
                     int count2 = 1; decimal total2 = 0;
                     while (lector2.Read())
                     {
                         var val = lector2.GetValue(12).ToString() == "" ? "0" : lector2.GetValue(12).ToString();
                         if (lector2.GetValue(0).ToString() != "ECO" && lector2.GetValue(0).ToString() != "ECA" && lector2.GetValue(0).ToString() != "ECF" && lector2.GetValue(13).ToString() == "CREDITO")
                         {
                             decimal eco_a_1_3 = decimal.Round(decimal.Parse(lector2.GetValue(9).ToString()), 2);
                             decimal eco_a_2_3 = decimal.Round(decimal.Parse(val), 2);

                             cells = new List<PdfPCell>()
                            { 
                            new PdfPCell(new Phrase(count1.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector2.GetValue(0).ToString()+" - "+lector2.GetValue(1).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector2.GetValue(2).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector2.GetValue(3).ToString()+lector2.GetValue(4).ToString()+" "+lector2.GetValue(5).ToString()+" "+lector2.GetValue(6).ToString()+" "+lector2.GetValue(7).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(double.Parse(lector2.GetValue(8).ToString()).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(eco_a_1_3.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(eco_a_2_3.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector2.GetValue(16).ToString() == "1"?"OCUP":lector2.GetValue(16).ToString()=="2"?"ASIS":lector2.GetValue(16).ToString()=="3"?"FAR":lector2.GetValue(16).ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             //new PdfPCell(new Phrase(double.Parse(lector2.GetValue(11).ToString()).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector2.GetValue(13).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                             new PdfPCell(new Phrase(lector2.GetValue(14).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                             new PdfPCell(new Phrase(lector2.GetValue(15).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                         };
                             count2++; total2 = decimal.Round(decimal.Parse(val) + total2 , 2);
                             columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                             table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                             document.Add(table);
                         }

                     }
                     cells = new List<PdfPCell>()
                        {         
                        new PdfPCell(new Phrase(" TOTAL CRÉDITO:", fontColumnValueBold)){ Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        new PdfPCell(new Phrase(total2.ToString(), fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        new PdfPCell(new Phrase(" ", fontColumnValueBold)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                        };
                     columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                     table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                     document.Add(table);
                    #endregion

                        #region INGRESOS NO EFECTIVO
                     cells = new List<PdfPCell>()
                        {         
                             new PdfPCell(new Phrase("INGRESOS NO EFECTIVO", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =12, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Itm", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Documento", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Descripción", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cliente", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Cantidad", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Precio Unitario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Total", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Venta", fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             //new PdfPCell(new Phrase("IGV", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Condición", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Tipo", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Usuario", fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                        };
                     columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                     table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                     document.Add(table);
                     lector2.Close();
                     SqlCommand comando3 = null;
                     if (systemUserId == 1)
                         comando3 = new SqlCommand(cadenaSA, connection: conectasam.conectarsam);
                     else if (systemUserId == 2036 )
                         comando3 = new SqlCommand(cadenaFarmacia, connection: conectasam.conectarsam);
                     else
                         comando3 = new SqlCommand(cadena, connection: conectasam.conectarsam);
                     SqlDataReader lector3 = comando3.ExecuteReader();
                     int count3 = 1; decimal total3 = 0;
                     while (lector3.Read())
                     {
                         var val = lector3.GetValue(12).ToString() == "" ? "0" : lector3.GetValue(12).ToString();
                         if ((lector3.GetValue(0).ToString() != "ECO" && lector3.GetValue(0).ToString() != "ECA" && lector3.GetValue(0).ToString() != "ECF" && (lector3.GetValue(13).ToString() == "CONTADO" || lector3.GetValue(13).ToString() == "PAGO MIXTO") && lector3.GetValue(14).ToString() != "EFECTIVO SOLES") || (lector3.GetValue(13).ToString() == "CHEQUE" || lector3.GetValue(13).ToString() == "DEPOSITO"))
                         {
                             decimal eco_a_1_4 = decimal.Round(decimal.Parse(lector3.GetValue(9).ToString()), 2);
                             decimal eco_a_2_4 = decimal.Round(decimal.Parse(val), 2);

                             cells = new List<PdfPCell>()
                            { 
                            new PdfPCell(new Phrase(count3.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector3.GetValue(0).ToString()+" - "+lector3.GetValue(1).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector3.GetValue(2).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector3.GetValue(3).ToString()+lector3.GetValue(4).ToString()+" "+lector3.GetValue(5).ToString()+" "+lector3.GetValue(6).ToString()+" "+lector3.GetValue(7).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(double.Parse(lector3.GetValue(8).ToString()).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(eco_a_1_4.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(eco_a_2_4.ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector3.GetValue(16).ToString() == "1"?"OCUP":lector3.GetValue(16).ToString()=="2"?"ASIS":lector3.GetValue(16).ToString()=="3"?"FAR":lector3.GetValue(16).ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             //new PdfPCell(new Phrase(double.Parse(lector3.GetValue(11).ToString()).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                             new PdfPCell(new Phrase(lector3.GetValue(13).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                             new PdfPCell(new Phrase(lector3.GetValue(14).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE}, 
                             new PdfPCell(new Phrase(lector3.GetValue(15).ToString(), fontColumnValue)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                         };
                             
                             count3++; total3 = decimal.Round(decimal.Parse(val) + total3 , 2);
                             columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                             table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                             document.Add(table);
                         }

                     }
                     cells = new List<PdfPCell>()
                        {         
                        new PdfPCell(new Phrase(" TOTAL NO EFECTIVO:", fontColumnValueBold)){ Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        new PdfPCell(new Phrase(total3.ToString(), fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        new PdfPCell(new Phrase(" ", fontColumnValueBold)){ Colspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        
                        };
                     columnWidths = new float[] { 3f, 10f, 20f, 20f, 5f, 5f, 5f, 5f, 3f, 7f, 10f, 7f };
                     table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                     document.Add(table);
                     lector3.Close();
                    #endregion

                        #region RESUMEN
                     
                     
                     SqlCommand comando5 = null;
                     if (systemUserId != 2036 )
                     {
                         if (systemUserId == 1)
                             comando5 = new SqlCommand(cadenaSA, connection: conectasam.conectarsam);
                         else
                             comando5 = new SqlCommand(cadena, connection: conectasam.conectarsam);
                         SqlDataReader lector5 = comando5.ExecuteReader();
                         decimal totalEgresos = 0,
                             totalIngresoEfectivo = 0,
                             totalCredito = 0,
                             totalIngresoNoEfectivo = 0,
                             totalOcupacional = 0,
                             totalAsistencial = 0,
                             totalEgresoOcupacional = 0,
                             totalEgresoAsistencial = 0,
                             totalIngresoEfectivoAsistencial = 0,
                             totalIngresoEfectivoOcupacional = 0,
                             totalIngresoNoEfectivoAsistencial_1 = 0,
                             totalIngresoNoEfectivoOcupacional_2 = 0,
                             totalIngresoNoEfectivoAsistencial_3 = 0,
                             totalIngresoNoEfectivoOcupacional_4 = 0,
                             totalCreditoOcupacional = 0,
                             totalCreditoAsistencial = 0;


                         while (lector5.Read())
                         {
                             var val = lector5.GetValue(12).ToString() == "" ? "0" : lector5.GetValue(12).ToString();
                             //if (lector5.GetValue(0).ToString() == "ECO" || lector5.GetValue(0).ToString() == "ECA" || lector5.GetValue(0).ToString() == "ECF")
                             if (lector5.GetValue(0).ToString() == "ECO" || lector5.GetValue(0).ToString() == "ECA" || lector5.GetValue(0).ToString() == "ECF")
                             {      
                                 totalEgresos = decimal.Round(decimal.Parse(val) + totalEgresos, 2);
                             }
                             //if (lector5.GetValue(0).ToString() == "ECO" || lector5.GetValue(0).ToString() == "ECA")
                             //{
                             //    totalEgresos = decimal.Round((decimal.Parse(lector5.GetValue(12).ToString()) * -1) + totalEgresos, 2);
                             //}
                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && (lector5.GetValue(13).ToString() == "CONTADO" || lector5.GetValue(13).ToString() == "PAGO MIXTO") && lector5.GetValue(14).ToString() == "EFECTIVO SOLES")
                             {
                                 totalIngresoEfectivo = decimal.Round(decimal.Parse(val) + totalIngresoEfectivo, 2);
                             }
                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(13).ToString() == "CREDITO")
                             {
                                 totalCredito = decimal.Round(decimal.Parse(val) + totalCredito, 2);
                             }
                             if ((lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && (lector5.GetValue(13).ToString() == "CONTADO" || lector5.GetValue(13).ToString() == "PAGO MIXTO") && lector5.GetValue(14).ToString() != "EFECTIVO SOLES") || (lector5.GetValue(13).ToString() == "CHEQUE" || lector5.GetValue(13).ToString() == "DEPOSITO"))
                             {
                                 totalIngresoNoEfectivo = decimal.Round(decimal.Parse(val) + totalIngresoNoEfectivo, 2);
                             }

                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(16).ToString() == "1")
                             {
                                 totalOcupacional = decimal.Round(decimal.Parse(val) + totalOcupacional, 2);
                             }
                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(16).ToString() == "2")
                             {
                                 totalAsistencial = decimal.Round(decimal.Parse(val) + totalAsistencial, 2);
                             }

                             if (lector5.GetValue(0).ToString() == "ECO")
                             {
                                 totalEgresoOcupacional = decimal.Round((decimal.Parse(val) * -1) + totalEgresoOcupacional, 2);
                             }
                             if (lector5.GetValue(0).ToString() == "ECA")
                             {
                                 totalEgresoAsistencial = decimal.Round((decimal.Parse(val) * -1) + totalEgresoAsistencial, 2);
                             }

                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && ((lector5.GetValue(13).ToString() == "CONTADO" && lector5.GetValue(14).ToString() == "EFECTIVO SOLES") || (lector5.GetValue(13).ToString() == "PAGO MIXTO" && lector5.GetValue(14).ToString() == "EFECTIVO SOLES")) && lector5.GetValue(16).ToString() == "1" || lector5.GetValue(0).ToString() == "ICO")
                             {
                                 totalIngresoEfectivoOcupacional = decimal.Round(decimal.Parse(val) + totalIngresoEfectivoOcupacional, 2);
                             }
                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && ((lector5.GetValue(13).ToString() == "CONTADO" && lector5.GetValue(14).ToString() == "EFECTIVO SOLES") || (lector5.GetValue(13).ToString() == "PAGO MIXTO" && lector5.GetValue(14).ToString() == "EFECTIVO SOLES")) && lector5.GetValue(16).ToString() == "2" || lector5.GetValue(0).ToString() == "ICA")
                             {
                                 totalIngresoEfectivoAsistencial = decimal.Round(decimal.Parse(val) + totalIngresoEfectivoAsistencial, 2);
                             }

                             if ((lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && (lector5.GetValue(13).ToString() == "CONTADO" || lector5.GetValue(13).ToString() == "PAGO MIXTO") && lector5.GetValue(14).ToString() != "EFECTIVO SOLES") && lector5.GetValue(16).ToString() == "2")
                             {
                                 totalIngresoNoEfectivoAsistencial_1 = decimal.Round(decimal.Parse(val) + totalIngresoNoEfectivoAsistencial_1, 2);
                             }
                             if ((lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && (lector5.GetValue(13).ToString() == "CONTADO" || lector5.GetValue(13).ToString() == "PAGO MIXTO") && lector5.GetValue(14).ToString() != "EFECTIVO SOLES") && lector5.GetValue(16).ToString() == "1")
                             {
                                 totalIngresoNoEfectivoOcupacional_2 = decimal.Round(decimal.Parse(val) + totalIngresoNoEfectivoOcupacional_2, 2);
                             }
                             if ((lector5.GetValue(13).ToString() == "CHEQUE" || lector5.GetValue(13).ToString() == "DEPOSITO") && lector5.GetValue(16).ToString() == "2")
                             {
                                 totalIngresoNoEfectivoAsistencial_3 = decimal.Round(decimal.Parse(val) + totalIngresoNoEfectivoAsistencial_3, 2);
                             }
                             if ((lector5.GetValue(13).ToString() == "CHEQUE" || lector5.GetValue(13).ToString() == "DEPOSITO") && lector5.GetValue(16).ToString() == "1")
                             {
                                 totalIngresoNoEfectivoOcupacional_4 = decimal.Round(decimal.Parse(val) + totalIngresoNoEfectivoOcupacional_4, 2);
                             }


                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(13).ToString() == "CREDITO" && lector5.GetValue(16).ToString() == "1")
                             {
                                 totalCreditoOcupacional = decimal.Round(decimal.Parse(val) + totalCreditoOcupacional, 2);
                             }
                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(13).ToString() == "CREDITO" && lector5.GetValue(16).ToString() == "2")
                             {
                                 totalCreditoAsistencial = decimal.Round(decimal.Parse(val) + totalCreditoAsistencial, 2);
                             }

                         }

                             cells = new List<PdfPCell>()
                            {         
                                 new PdfPCell(new Phrase("RESUMEN DE CAJA", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                                 new PdfPCell(new Phrase("OCUPACIONAL", fontColumnValueBold_1)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                                 new PdfPCell(new Phrase("ASISTENCIAL", fontColumnValueBold_1)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    

                                 new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                                 new PdfPCell(new Phrase("EGRESOS   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalEgresoOcupacional.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("EGRESOS   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalEgresoAsistencial.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("INGRESOS CONTADO EFECTIVO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoEfectivoOcupacional.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("INGRESOS CONTADO EFECTIVO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoEfectivoAsistencial.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("INGRESOS CRÉDITO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalCreditoOcupacional.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("INGRESOS CRÉDITO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalCreditoAsistencial.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("INGRESOS VISA   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoNoEfectivoOcupacional_2.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("INGRESOS VISA   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoNoEfectivoAsistencial_1.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("INGRESOS DEPOSITO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoNoEfectivoOcupacional_4.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("INGRESOS DEPOSITO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoNoEfectivoAsistencial_3.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("TOTAL ENTREGAR   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase((totalIngresoEfectivoOcupacional + totalEgresoOcupacional).ToString() , fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("TOTAL ENTREGAR   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase((totalIngresoEfectivoAsistencial + totalEgresoAsistencial).ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("---------------------------", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("---------------------------", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("---------------------------", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("---------------------------", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    


                                 new PdfPCell(new Phrase("VENTA TOTAL      S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase((totalIngresoEfectivoOcupacional + totalEgresoOcupacional + totalCreditoOcupacional + totalIngresoNoEfectivoOcupacional_2 + totalIngresoNoEfectivoOcupacional_4).ToString() , fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("VENTA TOTAL      S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase((totalIngresoEfectivoAsistencial + totalEgresoAsistencial + totalCreditoAsistencial + totalIngresoNoEfectivoAsistencial_1 + totalIngresoNoEfectivoAsistencial_3).ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    

     
                            };
                             columnWidths = new float[] { 25f, 15f, 10f, 25f, 15f, 10f };
                             table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                             document.Add(table);

                     }
                     else
                     {
                         comando5 = new SqlCommand(cadenaFarmacia, connection: conectasam.conectarsam);
                         SqlDataReader lector5 = comando5.ExecuteReader();
                         decimal totalEgresos = 0,
                             totalIngresoEfectivo = 0,
                             totalCredito = 0,
                             totalIngresoNoEfectivo = 0,
                             totalIngresoNoEfectivoVISA = 0;

                         while (lector5.Read())
                         {
                             var val = lector5.GetValue(12).ToString() == "" ? "0" : lector5.GetValue(12).ToString();

                             if (lector5.GetValue(0).ToString() == "ECO" || lector5.GetValue(0).ToString() == "ECA" || lector5.GetValue(0).ToString() == "ECF")
                             {
                                 totalEgresos = decimal.Round(decimal.Parse(val) + totalEgresos, 2);
                             }
                             //if (lector5.GetValue(0).ToString() == "ECO" || lector5.GetValue(0).ToString() == "ECA")
                             //{
                             //    totalEgresos = decimal.Round((decimal.Parse(lector5.GetValue(12).ToString()) * -1) + totalEgresos, 2);
                             //}
                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(0).ToString() != "ECF" && ((lector5.GetValue(13).ToString() == "CONTADO" && lector5.GetValue(14).ToString() == "EFECTIVO SOLES") || (lector5.GetValue(13).ToString() == "PAGO MIXTO" && lector5.GetValue(14).ToString() == "EFECTIVO SOLES")))
                             {
                                 totalIngresoEfectivo = decimal.Round(decimal.Parse(val) + totalIngresoEfectivo, 2);
                             }
                             if (lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(0).ToString() != "ECF" && lector5.GetValue(13).ToString() == "CREDITO")
                             {
                                 totalCredito = decimal.Round(decimal.Parse(val) + totalCredito, 2);
                             }
                             if ((lector5.GetValue(13).ToString() == "CHEQUE" || lector5.GetValue(13).ToString() == "DEPOSITO") && lector5.GetValue(16).ToString() == "3")
                             {
                                 totalIngresoNoEfectivo = decimal.Round(decimal.Parse(val) + totalIngresoNoEfectivo, 2);
                             }


                             if ((lector5.GetValue(0).ToString() != "ECO" && lector5.GetValue(0).ToString() != "ECA" && lector5.GetValue(0).ToString() != "ECF" && (lector5.GetValue(13).ToString() == "CONTADO" || lector5.GetValue(13).ToString() == "PAGO MIXTO") && lector5.GetValue(14).ToString() != "EFECTIVO SOLES") && lector5.GetValue(16).ToString() == "3")
                             {
                                 totalIngresoNoEfectivoVISA = decimal.Round(decimal.Parse(val) + totalIngresoNoEfectivoVISA, 2);
                             }

                         }
                         //entregar = decimal.Round(totalIngresoEfectivo - totalEgresos, 2);

                         //total = decimal.Round(totalIngresoEfectivo - totalEgresos + totalIngresoNoEfectivoVISA + totalIngresoNoEfectivo, 2);
                         decimal caja_chica = 203.5m;
                         cells = new List<PdfPCell>()
                            {         
                                 new PdfPCell(new Phrase("RESUMEN DE CAJA FARMACIA", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                                 
                                 new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    

                                 new PdfPCell(new Phrase("EGRESOS   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("- " + totalEgresos.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("INGRESOS CONTADO EFECTIVO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoEfectivo.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                
                                 //new PdfPCell(new Phrase("INGRESOS CRÉDITO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 //new PdfPCell(new Phrase(totalCredito.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 //new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 
                                 new PdfPCell(new Phrase("INGRESOS VISA   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoNoEfectivoVISA.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 
                                 new PdfPCell(new Phrase("INGRESOS DEPOSITO   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase(totalIngresoNoEfectivo.ToString(), fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 
                                 new PdfPCell(new Phrase("*** CAJA CHICHA MENSUAL   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase((caja_chica ).ToString() , fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("TOTAL EFECTIVO S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase((totalIngresoEfectivo - totalEgresos - caja_chica).ToString() , fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 
                                 new PdfPCell(new Phrase("TOTAL VENTAS   S/. ", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase((totalIngresoEfectivo - totalEgresos + totalIngresoNoEfectivoVISA + totalIngresoNoEfectivo - caja_chica).ToString() , fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                                 new PdfPCell(new Phrase("", fontColumnValueBold_1)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                                 new PdfPCell(new Phrase("", fontColumnValueBold)){ Colspan =3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = 7f, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    

     
                            };
                         columnWidths = new float[] { 45f, 25f, 30f };
                         table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                         document.Add(table);
                     }


                     #endregion

                        #region SE RECIBIÓ CONFORME
                     cells = new List<PdfPCell>()
                         {
                             new PdfPCell(new Phrase("FIRMA RECIBÍ CONFORME", fontColumnValueBold)){ Colspan =1, Rowspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("FIRMA USUARIO DE CAJA", fontColumnValueBold)){ Colspan =1,Rowspan =5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                         };
                         columnWidths = new float[] { 50f, 50f };
                         filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                         document.Add(filiationWorker);
                       #endregion
                 
                
                 // step 5: we close the document

                 document.Close();
                 writer.Close();
                 writer.Dispose();
                 RunFile(filePDF);

               

                 

                 #endregion
                
                
         }

        public static void CreateCuadreCajaNew(string filePDF, string inicio, string fin,
           OrganizationDto1 infoEmpresaPropietaria, int systemUserId, List<ventaDto> objData)
        {


            #region Declaration Tables
            // step 1: creation of a document-object
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
            //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //Document document = new Document(PageSize.A4, 0, 0, 20, 20);


            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

            //create an instance of your PDFpage class. This is the class we generated above.
            pdfPage page = new pdfPage();
            writer.PageEvent = page;
            // step 3: we open the document
            document.Open();
            // step 4: we Add content to the document
            // we define some fonts
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

            #region Conexion SAM
            ConexionSAM conectasam = new ConexionSAM();
            conectasam.opensam();
            #endregion

            #region Fecha
            string[] fechaInicio = inicio.Split(' ');
            string[] fecha1 = fechaInicio[0].Split('/');
            string anioinicio_i = fecha1[2];
            string mesinicio_i = fecha1[1];
            string diainicio_i = fecha1[0];

            string[] fechaFin = fin.Split(' ');
            string[] fecha2 = fechaFin[0].Split('/');
            string anioinicio_f = fecha2[2];
            string mesinicio_f = fecha2[1];
            string diainicio_f = fecha2[0];

            DateTime localDate = DateTime.Now;
            #endregion

         

            #region Title
            PdfPCell logoEmpresa = null;
            if (infoEmpresaPropietaria.b_Image != null)
                logoEmpresa = new PdfPCell(HandlingItextSharp.GetImage(infoEmpresaPropietaria.b_Image, null, null, 120, 50));
            else
                logoEmpresa = new PdfPCell(new Phrase(" ", fontColumnValue));

            logoEmpresa.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            logoEmpresa.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            string usuario = Globals.ClientSession.v_UserName;
            
            cells = new List<PdfPCell>()
                 {
                     new PdfPCell(logoEmpresa){Rowspan=1, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda2,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                     new PdfPCell(new Phrase("CUADRE DE CAJA ", fontTitleTable)) {Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                     new PdfPCell(new Phrase("usuario: "+usuario+"\r\n "+"\r\n  Cuadre de la Fecha: "+diainicio_i+" - "+mesinicio_i+" - "+anioinicio_i+"\r\n "+"\r\n AL: "+diainicio_f+" - "+mesinicio_f+" - "+anioinicio_f + "\r\n "+"\r\n Fecha y hora de Impresión: "+localDate, fontColumnValueBold)) {Rowspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                 };
            columnWidths = new float[] { 30f, 40f, 30f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            
            #endregion

            #region Reporte

            decimal total = 0;
            
                #region EFECTIVO
                cells = new List<PdfPCell>()
                         {         
                             new PdfPCell(new Phrase("EFECTIVO", fontTitleTable)){ BackgroundColor=BaseColor.LIGHT_GRAY, Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Itm", fontTitleTable)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Fecha", fontTitleTable)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Descripcion", fontTitleTable)){ Colspan =11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Ingreso", fontTitleTable)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Egreso", fontTitleTable)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase("Saldo", fontTitleTable)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                         };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);
                int count = 1;
                decimal ingreso = 0, egreso = 0, saldo = 0, ingresototal = 0, egresototal = 0;
                foreach (var obj in objData)
                {
                    
                    decimal subtotal = decimal.Round((decimal) obj.d_Total, 2);
                    string CondicionPago = GetCondicionPago(obj.v_IdVenta);
                    string FormaPago = GetFormaPago(obj.v_IdVenta);
                    if ((obj.TipoDocumento == "BOL" || obj.TipoDocumento == "FAC" || obj.TipoDocumento == "RSM") && (CondicionPago == "CONTADO" || CondicionPago == "PAGOMIXTO") && FormaPago == "EFECTIVO SOLES")
                    {
                        ingreso = subtotal;
                        egreso = 0;
                        saldo = saldo + subtotal;
                        ingresototal = ingresototal + ingreso;
                    }
                    if (obj.TipoDocumento == "ECA" || obj.TipoDocumento == "ECF" || obj.TipoDocumento == "ECO")
                    {
                        ingreso = 0;
                        egreso = subtotal;
                        saldo = saldo - subtotal;
                        egresototal = egresototal + egreso;
                    }
                    if (obj.TipoDocumento == "ICA" || obj.TipoDocumento == "ICO" || obj.TipoDocumento == "ICF")
                    {
                        ingreso = subtotal;
                        egreso = 0;
                        saldo = saldo + subtotal;
                        ingresototal = ingresototal + ingreso;
                    }
                        cells = new List<PdfPCell>()
                        {
                            new PdfPCell(new Phrase(count.ToString(), fontColumnValueBold)){ Colspan =1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                            new PdfPCell(new Phrase(obj.t_FechaRegistro.ToString().Split(' ')[0] , fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                            new PdfPCell(new Phrase(obj.v_Concepto +", "+obj.NombreCliente +", "+obj.Documento , fontColumnValueBold)){ Colspan =11, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                            new PdfPCell(new Phrase(ingreso.ToString(), fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                            new PdfPCell(new Phrase(egreso.ToString(), fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                            new PdfPCell(new Phrase(saldo.ToString(), fontColumnValueBold)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                        };
                        count++;
                        total = total + subtotal;
                        columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                        table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                        document.Add(table);
                    
                    
                }
                
                cells = new List<PdfPCell>()
                         {         
                             new PdfPCell(new Phrase(" TOTALES:", fontColumnValue)){ Colspan =14, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase(ingresototal.ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase(egresototal.ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},  
                             new PdfPCell(new Phrase(saldo.ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase(" ", fontColumnValue)){ Colspan =20, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase(" TOTAL:", fontColumnValue)){ Colspan =18, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                             new PdfPCell(new Phrase(saldo.ToString(), fontColumnValue)){ Colspan =2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE,  UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    

                         };
                columnWidths = new float[] { 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f };
                table = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable);
                document.Add(table);


                #endregion

            


            // step 5: we close the document

            document.Close();
            writer.Close();
            writer.Dispose();
            RunFile(filePDF);





            #endregion


        }

        private static string GetFormaPago(string idVenta)
        {
            ConexionSAM cnx = new ConexionSAM();
            cnx.opensam();
            string sql = "select dh.v_Value1 from venta vv " +
                         "Left Join cobranzadetalle CD " +
                         "ON CD.v_IdVenta = vv.v_IdVenta " +
                         "inner join datahierarchy dh on dh.i_GroupId=46 and dh.i_ItemId=CD.i_IdFormaPago " +
                         "where vv.v_IdVenta='" + idVenta + "'";
            SqlCommand command = new SqlCommand(sql, cnx.conectarsam);
            SqlDataReader lector = command.ExecuteReader();
            string forma = "EFECTIVO SOLES";
            while (lector.Read())
            {
                forma = lector.GetValue(0).ToString();
            }
            lector.Close();
            cnx.closesam();
            return forma;
        }

        private static string GetCondicionPago(string idVenta)
        {
            ConexionSAM cnx = new ConexionSAM();
            cnx.opensam();
            string sql = "select dh.v_Value1 from venta vv " +
                         "inner join datahierarchy dh on dh.i_GroupId=23 and dh.i_ItemId=vv.i_IdCondicionPago " +
                         "where v_IdVenta='" + idVenta + "'";
            SqlCommand command = new SqlCommand(sql, cnx.conectarsam);
            SqlDataReader lector = command.ExecuteReader();
            string condicion = "";
            while (lector.Read())
            {
                condicion = lector.GetValue(0).ToString();
            }
            lector.Close();
            cnx.closesam();
            return condicion;
        }

        private static object GetApplicationConfigValue(string nombre)
        {
            return Convert.ToString(ConfigurationManager.AppSettings[nombre]);
        }



         #region Utils

         private static void RunFile(string filePDF)
         {
             Process proceso = Process.Start(filePDF);
             proceso.WaitForExit();
             proceso.Close();

         }

         #endregion


         public static void CreateReportProduccion(string filePDF, List<ProduccionReprt> produccions, List<Productos> productos, string FechaInicio, string fechaFin, string modo)
         {
             string _modo = modo;
             #region Declaration Tables
             // step 1: creation of a document-object
             iTextSharp.text.Document document = new iTextSharp.text.Document();
             //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
             //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
             document.SetPageSize(iTextSharp.text.PageSize.A4);
             //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

             filePDF = filePDF + "report.pdf";
             // step 2: we create a writer that listens to the document
             PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

             //create an instance of your PDFpage class. This is the class we generated above.
             pdfPage page = new pdfPage();
             writer.PageEvent = page;
             // step 3: we open the document
             document.Open();
             // step 4: we Add content to the document
             // we define some fonts
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
             var rutaImg = GetApplicationConfigValue("Logo");
             var imageLogo = new PdfPCell(HandlingItextSharp.GetImageLogo(rutaImg.ToString(), null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            cells = new List<PdfPCell>()
                 {
                     new PdfPCell(imageLogo){Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda2,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                     new PdfPCell(new Phrase("VENTAS POR PRODUCTO DEL "+FechaInicio+" AL "+fechaFin, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                     new PdfPCell(new Phrase("usuario: "+"\r\nFecha y hora de Impresión: ", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                 };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
             filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
             document.Add(filiationWorker);
             #endregion

             #region Area

             List<string> areaList = new List<string>();
             areaList = ObtenerAreaConsultorio();
             List<string> tipoList = new List<string>();
             tipoList = ObtenerTipoDeConsultorio();
             List<string> categoryList = new List<string>();
             categoryList = ObtenerCategorias();
             foreach (var tipo in tipoList){foreach (var cat in categoryList){if (tipo==cat){categoryList = categoryList.FindAll(p => !p.Contains(tipo));}}}
             bool find;
             List<ComponentReport> cr;
             foreach (var area in areaList)
             {
                 
                 List<ProduccionReprt> result = produccions.FindAll(p => p.v_Area == area);
                 if (result.Count > 0)
                 {
                     double acumuladorArea = 0;
                     acumuladorArea = ObtenerTotal(result);
                     int nroAtencionesArea = result.Count;
                     //Pintar Area
                     cells = new List<PdfPCell>()
                     {
                         new PdfPCell(new Phrase("AREA: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                         new PdfPCell(new Phrase(area, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                         new PdfPCell(new Phrase("Cantidad: " + nroAtencionesArea.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                         new PdfPCell(new Phrase("Monto: " + acumuladorArea.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    

                     };
                     columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                     filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                     document.Add(filiationWorker);
                 }
                 else{break;}
             

             #endregion

             #region Tipo

             double acumuladorTipo = 0;
                 int contadorTipo = 0;
                 List<ProduccionReprt> result1 = null;
                 double acumuladorComponent = 0;
                 int contadorComponent = 0;
                foreach (var tipo in tipoList)
                 {
                     result1 = new List<ProduccionReprt>(); 
                     result1 = produccions.FindAll(p => p.v_Tipo == tipo);
                     if (result1.Count > 0)
                     {
                         acumuladorTipo = ObtenerTotal(result1);
                         contadorTipo = result1.Count;
                         //Pintar Tipo
                         ImprimirTipo(tipo, contadorTipo, acumuladorTipo, cells, columnWidths, filiationWorker, document, fontTitleTable, _modo);
                     }
                     else{continue;}
                     result1 = result1.FindAll(p => p.v_Area == area);
                     List<string> names = new List<string>();
                     foreach (var rr in result1)
                     {
                         names.Add(rr.v_ComponentName);
                     }
                     names = names.Distinct().ToList();
                     cr = new List<ComponentReport>();
                     foreach (var nn in names)
                     {
                         contadorComponent = 0;
                         acumuladorComponent = 0;
                         ComponentReport crReport = new ComponentReport();
                         foreach (var rs in result1)
                         {
                             if (nn == rs.v_ComponentName)
                             {
                                 contadorComponent++;
                                 acumuladorComponent = acumuladorComponent + (double)rs.r_Price;
                             }
                         }

                         crReport.name = nn;
                         crReport.count = contadorComponent.ToString();
                         crReport.monto = acumuladorComponent.ToString();
                         cr.Add(crReport);
                     }

                     foreach (var cc in cr)
                     {
                         cells = new List<PdfPCell>()
                         {
                             new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                             new PdfPCell(new Phrase(cc.name, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                             new PdfPCell(new Phrase("Cantidad: " + cc.count.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                             new PdfPCell(new Phrase("Monto: " + cc.monto.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         };
                         columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                         filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                         document.Add(filiationWorker);
                     }
                 }

             #endregion

             #region Examenes

              List<ProduccionReprt> result2;
                foreach (var categoria in categoryList)
                 {
                     result2 = new List<ProduccionReprt>();
                     result2 = produccions.FindAll(p => p.v_Tipo == categoria);

                     if (result2.Count > 0)
                     {
                         acumuladorTipo = ObtenerTotal(result2);
                         contadorTipo = result2.Count;
                         ImprimirTipo(categoria, contadorTipo, acumuladorTipo, cells, columnWidths, filiationWorker, document, fontTitleTable, _modo);
                     }
                     else{continue;}

                     result2 = result2.FindAll(p => p.v_Area == area);
                    List<string> names = new List<string>();
                    foreach (var rr in result2)
                    {
                        names.Add(rr.v_ComponentName);
                    }

                    names = names.Distinct().ToList();
                    cr = new List<ComponentReport>();
                    foreach (var nn in names)
                    {
                        contadorComponent = 0;
                        acumuladorComponent = 0;
                        ComponentReport crReport = new ComponentReport();
                        foreach (var rs in result2)
                        {
                            if (nn == rs.v_ComponentName)
                            {
                                contadorComponent++;
                                acumuladorComponent = acumuladorComponent + (double) rs.r_Price;
                            }
                        }

                        crReport.name = nn;
                        crReport.count = contadorComponent.ToString();
                        crReport.monto = acumuladorComponent.ToString();
                        cr.Add(crReport);
                    }

                    foreach (var cc in cr)
                    {
                        cells = new List<PdfPCell>()
                         {
                             new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                             new PdfPCell(new Phrase(cc.name, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                             new PdfPCell(new Phrase("Cantidad: " + cc.count.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                             new PdfPCell(new Phrase("Monto: " + cc.monto.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         };
                         columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                         filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                         document.Add(filiationWorker);
                    }
                 }
             }

             #endregion

             #region Productos

              List<Productos> servicios = new List<Productos>();
            int countProduct = ObtenerProductoServicio(productos,0);
                List<string> prod = new List<string>();
                decimal monto = 0;
                foreach (var pp in productos)
                {
                    if (pp.i_EsServicio == 0)
                    {
                        prod.Add(pp.v_DescripcionProducto);
                        monto = monto + pp.d_Precio;
                    }
                }
                monto = decimal.Round(monto, 2);
            cells = new List<PdfPCell>()
                {
                 new PdfPCell(new Phrase("AREA: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("ALMACEN", fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("Cantidad: " + countProduct.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("Monto: " + monto.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    

                };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            

            prod = prod.Distinct().ToList();
            cr = new List<ComponentReport>();

            foreach (var ppp in prod)
            {
                countProduct = 0;
                monto = 0;
                ComponentReport crReport = new ComponentReport();
                foreach (var product in productos)
                {
                    if (ppp == product.v_DescripcionProducto)
                    {
                        countProduct++;
                        monto = monto + product.d_Precio;
                    }
                }
                monto = decimal.Round(monto, 2);
                crReport.name = ppp;
                crReport.count = countProduct.ToString();
                crReport.monto = monto.ToString();
                cr.Add(crReport);
            }
            foreach (var product in cr)
             {
                 
                 {
                     cells = new List<PdfPCell>()
                     {
                         new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase(product.name, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase("Cantidad: " + product.count.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase("Monto: " + product.monto.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

                     };
                     columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                     filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                     document.Add(filiationWorker);

                 }
            }
            countProduct = ObtenerProductoServicio(productos, 1);
                prod = new List<string>();
                monto = 0;
                foreach (var pp in productos)
                {
                    if (pp.i_EsServicio == 1)
                    {
                        prod.Add(pp.v_DescripcionProducto);
                        monto = monto + pp.d_Precio;
                    }
                }

                monto = decimal.Round(monto, 2);
            
            cells = new List<PdfPCell>()
            {
                new PdfPCell(new Phrase("AREA: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                new PdfPCell(new Phrase("SERVICIOS", fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                new PdfPCell(new Phrase("Cantidad: " + countProduct.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                new PdfPCell(new Phrase("Monto: " + monto.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    

            };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
            

            prod = prod.Distinct().ToList();
            cr = new List<ComponentReport>();

            foreach (var ppp in prod)
            {
                countProduct = 0;
                monto = 0;
                ComponentReport crReport = new ComponentReport();
                foreach (var product in productos)
                {
                    if (ppp == product.v_DescripcionProducto)
                    {
                        countProduct++;
                        monto = monto + product.d_Precio;
                    }
                }
                monto = decimal.Round(monto, 2);
                crReport.name = ppp;
                crReport.count = countProduct.ToString();
                crReport.monto = monto.ToString();
                cr.Add(crReport);
            }
            foreach (var product in cr)
            {

                {
                    cells = new List<PdfPCell>()
                     {
                         new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase(product.name, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase("Cantidad: " + product.count.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase("Monto: " + product.monto.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

                     };
                    columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                    filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                    document.Add(filiationWorker);

                }
            }

             #endregion

             #region RUN

             document.Close();
             writer.Close();
             writer.Dispose();
             RunFile(filePDF);

             #endregion
             

         }

        
         public static void CreateReportDetallado(string filePDF, List<ProduccionReprt> produccions, List<Productos> productos, string FechaInicio, string fechaFin, string modo)
         {
             string _modo = modo;
             #region Declaration Tables
             // step 1: creation of a document-object
             iTextSharp.text.Document document = new iTextSharp.text.Document();
             //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
             //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
             document.SetPageSize(iTextSharp.text.PageSize.A4);
             //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

             filePDF = filePDF + "report.pdf";
             // step 2: we create a writer that listens to the document
             PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

             //create an instance of your PDFpage class. This is the class we generated above.
             pdfPage page = new pdfPage();
             writer.PageEvent = page;
             // step 3: we open the document
             document.Open();
             // step 4: we Add content to the document
             // we define some fonts
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
             var rutaImg = GetApplicationConfigValue("Logo");
             var imageLogo = new PdfPCell(HandlingItextSharp.GetImageLogo(rutaImg.ToString(), null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
            cells = new List<PdfPCell>()
                 {
                     new PdfPCell(imageLogo){Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda2,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                     new PdfPCell(new Phrase("VENTAS POR PRODUCTO DEL "+FechaInicio+" AL "+fechaFin, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                     new PdfPCell(new Phrase("usuario: "+"\r\nFecha y hora de Impresión: ", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                 };
            columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
             filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
             document.Add(filiationWorker);
             #endregion

             #region Area

             List<string> areaList = new List<string>();
             areaList = ObtenerAreaConsultorio();
             List<string> tipoList = new List<string>();
             tipoList = ObtenerTipoDeConsultorio();
             List<string> categoryList = new List<string>();
             categoryList = ObtenerCategorias();
             foreach (var tipo in tipoList){foreach (var cat in categoryList){if (tipo==cat){categoryList = categoryList.FindAll(p => !p.Contains(tipo));}}}
             bool find;
             foreach (var area in areaList)
             {
                 
                 List<ProduccionReprt> result = produccions.FindAll(p => p.v_Area == area);
                 if (result.Count > 0)
                 {
                     double acumuladorArea = 0;
                     acumuladorArea = ObtenerTotal(result);
                     int nroAtencionesArea = result.Count;
                     //Pintar Area
                     cells = new List<PdfPCell>()
                     {
                         new PdfPCell(new Phrase("AREA: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                         new PdfPCell(new Phrase(area, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                         new PdfPCell(new Phrase("Cantidad: " + nroAtencionesArea.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                         new PdfPCell(new Phrase("Monto: " + acumuladorArea.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    

                     };
                     columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
                     filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                     document.Add(filiationWorker);
                 }
                 else{break;}
             

             #endregion

             #region Tipo

             double acumuladorTipo = 0;
                 int contadorTipo = 0;
                 List<ProduccionReprt> result1 = null;
                 double acumuladorComponent = 0;
                 int contadorComponent = 0;
                foreach (var tipo in tipoList)
                 {
                     result1 = new List<ProduccionReprt>(); 
                     result1 = produccions.FindAll(p => p.v_Tipo == tipo);
                     
                     if (result1.Count > 0)
                     {
                         acumuladorTipo = ObtenerTotal(result1);
                         contadorTipo = result1.Count;
                         //Pintar Tipo
                         ImprimirTipo(tipo, contadorTipo, acumuladorTipo, cells, columnWidths, filiationWorker, document, fontTitleTable, _modo);
                     }
                     else{continue;}
                    
                     foreach (var produccion in produccions)
                     {
                         find = false;
                         if (area == produccion.v_Area)
                         {
                             if (tipo == produccion.v_Tipo)
                             {
                                 acumuladorComponent = ObtenerTotalComponentes(produccions, produccion);
                                 contadorComponent = ContarComponentes(produccions, produccion);
                                 //Pintar compoentes
                                 cells = new List<PdfPCell>()
                                 {
                                     new PdfPCell(new Phrase(produccion.v_MedicoTratante, fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                                     new PdfPCell(new Phrase(produccion.v_ComponentName, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                                     new PdfPCell(new Phrase(produccion.v_PersonName.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                                     new PdfPCell(new Phrase(produccion.r_Price.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

                                 };
                                 columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
                                 filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                                 document.Add(filiationWorker);
                                 continue;
                             }
                             
                         }
                         if (find) { continue; }
                     }


                 }

             #endregion

             #region Examenes

              List<ProduccionReprt> result2;
                foreach (var categoria in categoryList)
                 {
                     result2 = new List<ProduccionReprt>();
                     result2 = produccions.FindAll(p => p.v_Tipo == categoria);

                     if (result2.Count > 0)
                     {
                         acumuladorTipo = ObtenerTotal(result2);
                         contadorTipo = result2.Count;
                         ImprimirTipo(categoria, contadorTipo, acumuladorTipo, cells, columnWidths, filiationWorker, document, fontTitleTable, _modo);
                     }
                     else{continue;}
                     foreach (var produccion in produccions)
                     {
                         find = false;
                         if (area == produccion.v_Area)
                         {
                             if (categoria == produccion.v_Tipo)
                             {
                                 //Pintar compoentes
                                 acumuladorComponent = ObtenerTotalComponentes(produccions, produccion);
                                 contadorComponent = ContarComponentes(produccions, produccion);
                                 //Pintar compoentes
                                 cells = new List<PdfPCell>()
                                 {
                                     new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                                     new PdfPCell(new Phrase(produccion.v_ComponentName, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                                     new PdfPCell(new Phrase(produccion.v_PersonName.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                                     new PdfPCell(new Phrase(produccion.r_Price.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

                                 };
                                 columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
                                 filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                                 document.Add(filiationWorker);
                                 continue;
                             }
                         }
                         if (find) { continue; }
                     }

                    
                 }
                 
             }

             #endregion

             #region Productos

              List<Productos> servicios = new List<Productos>();
            int countProduct = ObtenerProductoServicio(productos,0);
            decimal monto = ObtenerMontoProdcutServicio(productos);
            cells = new List<PdfPCell>()
                {
                 new PdfPCell(new Phrase("AREA: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("ALMACEN", fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("Cantidad: " + countProduct.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("Monto: " + monto.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    

                };
            columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
            filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
            document.Add(filiationWorker);
             foreach (var product in productos)
             {
                 decimal total = decimal.Round(product.d_Cantidad * product.d_Precio,2);
                 if (product.i_EsServicio == 0)
                 {
                     cells = new List<PdfPCell>()
                     {
                         new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase(product.v_DescripcionProducto, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase(product.v_Name.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                         new PdfPCell(new Phrase(product.d_Precio.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

                     };
                     columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
                     filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                     document.Add(filiationWorker);

                 }
                 else
                 {
                     servicios.Add(product);
                 }
             }

             countProduct = ObtenerProductoServicio(servicios, 1);
             monto = ObtenerMontoProdcutServicio(servicios);
             cells = new List<PdfPCell>()
                {
                 new PdfPCell(new Phrase("AREA: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("SERVICIOS", fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("Cantidad: " + countProduct.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    
                 new PdfPCell(new Phrase("Monto: " + monto.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.DARK_GRAY},    

                };
             columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
             filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
             document.Add(filiationWorker);
             foreach (var product in servicios)
             {
                 decimal total = decimal.Round(product.d_Cantidad * product.d_Precio,2);
                 cells = new List<PdfPCell>()
                 {
                     new PdfPCell(new Phrase("", fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                     new PdfPCell(new Phrase(product.v_DescripcionProducto, fontColumnValue)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                     new PdfPCell(new Phrase(product.v_Name.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                     new PdfPCell(new Phrase(product.d_Precio.ToString(), fontColumnValue)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

                 };
                 columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
                 filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                 document.Add(filiationWorker);
             }


             #endregion

             #region RUN

             document.Close();
             writer.Close();
             writer.Dispose();
             RunFile(filePDF);

             #endregion
             
         }

         private static decimal ObtenerMontoProdcutServicio(List<Productos> productos)
         {
             decimal monto = 0;
             foreach (var item in productos)
             {
                 monto = monto + (item.d_Precio*item.d_Cantidad);
             }

             return monto = decimal.Round(monto, 2);
         }

         private static int ObtenerProductoServicio(List<Productos> productos, int p)
         {
             int count = 0;
             foreach (var item in productos)
             {
                 if (item.i_EsServicio == p)
                 {
                     count = count + Convert.ToInt32(decimal.Round(item.d_Cantidad,2));
                 }
             }

             return count;
         }

         private static int ContarComponentes(List<ProduccionReprt> produccions, ProduccionReprt produccion)
         {
             int count = 0;
             foreach (var item in produccions)
             {
                 if (item.v_ComponentName == produccion.v_ComponentName)
                 {
                     count++;
                 }
             }

             return count;
         }

         private static double ObtenerTotalComponentes(List<ProduccionReprt> produccions, ProduccionReprt produccion)
         {
             double acumula = 0;
             foreach (var item in produccions)
             {
                 if (item.v_ComponentName == produccion.v_ComponentName)
                 {
                     acumula = acumula + (double) item.r_Price;
                 }
             }
             return acumula = (double)decimal.Round((decimal)acumula, 2);
         }

         private static void ImprimirTipo(string tipo, int contadorTipo, double acumuladorTipo, List<PdfPCell> cells, float[] columnWidths, PdfPTable filiationWorker, iTextSharp.text.Document document, Font fontTitleTable, string modo)
         {
             if (modo == "DETALLADO")
             {
                     cells = new List<PdfPCell>()
                    {
                     new PdfPCell(new Phrase("TIPO: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase(tipo, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase("Cantidad: " + contadorTipo.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase("Monto: " + acumuladorTipo.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase("EXAMEN", fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase("PACIENTE" , fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase("MONTO" , fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    

                    };
                     columnWidths = new float[] { 3f, 5f, 10f, 10f, 10f, 10f, 17f, 15f, 10f, 10f };
                     filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable, null);
                     document.Add(filiationWorker);
             }
             else if (modo == "ACUMULADO")
             {
                 cells = new List<PdfPCell>()
                    {
                     new PdfPCell(new Phrase("TIPO: ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase(tipo, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase("Cantidad: " + contadorTipo.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     new PdfPCell(new Phrase("Monto: " + acumuladorTipo.ToString(), fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, BackgroundColor = BaseColor.GRAY},    
                     
                    };
                 columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                 filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTable, null);
                 document.Add(filiationWorker);
             }
             
         }

         private static double ObtenerTotal(List<ProduccionReprt> result)
         {
             double acumulador = 0;
             foreach (var item in result)
             {
                 acumulador = acumulador + (double)item.r_Price;
             }

             return acumulador = (double)decimal.Round((decimal)acumulador, 2);
         }

         private static List<string> ObtenerCategorias()
         {
             ConexionSigesoft conectaSigesoft = new ConexionSigesoft();
             conectaSigesoft.opensigesoft();
             var cadena = "select distinct(v_Value1) from component CP " +
                          "inner join systemparameter SP1 on CP.i_CategoryId=SP1.i_ParameterId and SP1.i_GroupId=116 and SP1.i_IsDeleted=0";
             SqlCommand comando = new SqlCommand(cadena, conectaSigesoft.conectarsigesoft);
             SqlDataReader lector = comando.ExecuteReader();
             List<string> categorias = new List<string>();
             while (lector.Read())
             {
                 string categoria = lector.GetValue(0).ToString();
                 categorias.Add(categoria);
             }

             return categorias;
         }

         private static List<string> ObtenerAreaConsultorio()
         {
             ConexionSigesoft conectaSigesoft = new ConexionSigesoft();
             conectaSigesoft.opensigesoft();
             var cadena = "select distinct(v_Value1) from component CP " +
                          "inner join systemparameter SP1 on CP.i_ComponentTypeId=SP1.i_ParameterId and SP1.i_GroupId=358 and SP1.i_IsDeleted=0";
             SqlCommand comando = new SqlCommand(cadena, conectaSigesoft.conectarsigesoft);
             SqlDataReader lector = comando.ExecuteReader();
             List<string> areas = new List<string>();
             while (lector.Read())
             {
                 string area = lector.GetValue(0).ToString();
                 areas.Add(area);
             }

             return areas;
         }

         private static List<string> ObtenerTipoDeConsultorio()
         {
             ConexionSigesoft conectaSigesoft = new ConexionSigesoft();
             conectaSigesoft.opensigesoft();
             var cadena = "select v_Value1 from systemparameter where i_GroupId=361";
             SqlCommand comando = new SqlCommand(cadena, conectaSigesoft.conectarsigesoft);
             SqlDataReader lector = comando.ExecuteReader();
             List<string> tipos = new List<string>();
             while (lector.Read())
             {
                 string tipo = lector.GetValue(0).ToString();
                 tipos.Add(tipo);
             }

             return tipos;
         }




         public static void CreateReportEgreso(string filePDF, string inicio, string fin, OrganizationDto1 infoEmpresaPropietaria, int systemUserId, List<EgresoDetalleReport> egresoDetalle)
         {
             #region Declaration Tables
             // step 1: creation of a document-object
             iTextSharp.text.Document document = new iTextSharp.text.Document();
             //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
             //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
             document.SetPageSize(iTextSharp.text.PageSize.A4);
             //Document document = new Document(PageSize.A4, 0, 0, 20, 20);

             filePDF = filePDF + "report.pdf";
             // step 2: we create a writer that listens to the document
             PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

             //create an instance of your PDFpage class. This is the class we generated above.
             pdfPage page = new pdfPage();
             writer.PageEvent = page;
             // step 3: we open the document
             document.Open();
             // step 4: we Add content to the document
             // we define some fonts
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
                     new PdfPCell(new Phrase("REPORTE DE EGRESOS, DEL "+inicio+" AL "+fin, fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                     new PdfPCell(new Phrase("usuario: "+"\r\nFecha y hora de Impresión: ", fontColumnValueBold)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2},    
                 };
             columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
             filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
             document.Add(filiationWorker);
             #endregion

             #region ReportBody

             cells = new List<PdfPCell>()
             {
                 new PdfPCell(new Phrase("Fecha", fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("Entregado a", fontTitleTable)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("Monto", fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    
                 new PdfPCell(new Phrase("Condición de Egreso", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},    

             };
             columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
             filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
             document.Add(filiationWorker);
             decimal acumuladorTotal = 0; 
             
             string detalle = "";
             foreach (var egreso in egresoDetalle)
             {

                 if (detalle != egreso.DescripcionProducto)
                 {
                     
                     var grupo = egresoDetalle.FindAll(p => p.DescripcionProducto == egreso.DescripcionProducto).ToList();

                     if (grupo.Count != 0)
                     {
                         
                         cells = new List<PdfPCell>()
                             {
                                 new PdfPCell(new Phrase(egreso.DescripcionProducto + " - " + grupo.Count(), fontColumnValueBold)){Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, BackgroundColor = BaseColor.LIGHT_GRAY, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},  
                             };
                         columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                         filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
                         document.Add(filiationWorker);
                         decimal acumuladorSubTotal = 0;
                         foreach (var item1 in grupo)
                         {
                             decimal monto = Convert.ToDecimal(item1.MontoProducto);
                             monto = Decimal.Round(monto, 2);
                             if (egresoDetalle[0] != null)
                                 cells = new List<PdfPCell>()
                             {
                                 new PdfPCell(new Phrase(item1.fechaVenta, fontColumnValue)){Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                                 new PdfPCell(new Phrase(item1.NombreCLiente, fontColumnValue)){Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                                 new PdfPCell(new Phrase(monto.ToString(), fontColumnValue)){Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                                 new PdfPCell(new Phrase(item1.CondicionPagoVenta, fontColumnValue)){Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE},
                         
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
                 
                 detalle = egreso.DescripcionProducto;
                 
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

         public static void CreateSolicitud(string filePDF, PersonDataReport person, SolicitudDataReport solicitud)
         {
             #region Declaration Tables
             // step 1: creation of a document-object
             iTextSharp.text.Document document = new iTextSharp.text.Document();
             //Document document = new Document(new Rectangle(500f, 300f), 10, 10, 10, 10);
             //document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
             //document.SetPageSize(iTextSharp.text.PageSize.A4);
             document = new iTextSharp.text.Document(PageSize.A4, 30, 30, 15, 0);

             //filePDF = filePDF + "report.pdf";
             // step 2: we create a writer that listens to the document
             PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePDF, FileMode.Create));

             //create an instance of your PDFpage class. This is the class we generated above.
             pdfPage page = new pdfPage();
             writer.PageEvent = page;
             // step 3: we open the document
             document.Open();
             // step 4: we Add content to the document
             // we define some fonts
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
             //document.Add(new Paragraph("\r\n"));
             var tamaño_celda = 1f;
             var tamaño_celda2 = 70f;


             #endregion

             #region Fonts

             Font fontTitle1 = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
             Font fontTitle2 = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
             Font fontTitleTable = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
             Font fontTitleTableNegro = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
             Font fontSubTitle = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
             Font fontSubTitleNegroNegrita = FontFactory.GetFont("Calibri", 5, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));

             Font fontColumnValue = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
             Font fontColumnValueBold = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
             Font fontColumnValueApendice = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
             Font fontColumnValue1 = FontFactory.GetFont("Calibri", 6, iTextSharp.text.Font.NORMAL, new BaseColor(System.Drawing.Color.Black));
             Font fontColumnSubrayado = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));


             #endregion

             #region Title
             var rutaImg = GetApplicationConfigValue("Logo");
             var imageLogo = new PdfPCell(HandlingItextSharp.GetImageLogo(rutaImg.ToString(), null, null, 120, 50)) { HorizontalAlignment = PdfPCell.ALIGN_CENTER };
             cells = new List<PdfPCell>()
                 {
                     new PdfPCell(imageLogo){Colspan= 3, HorizontalAlignment = PdfPCell.ALIGN_CENTER,  MinimumHeight = tamaño_celda2,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase("FORMULARIO DE SOLICITUD", fontTitle2)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("“Año de la lucha contra la corrupción e impunidad” ", fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},

                     
                 };
             columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
             filiationWorker = HandlingItextSharp.GenerateTableFromCells(cells, columnWidths, null, fontTitleTableNegro, null);
             document.Add(filiationWorker);
             #endregion

             string fecha = DateTime.Now.ToLongDateString();
             #region Body Report
             cells = new List<PdfPCell>()
                 {
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("DIRECTOR MÉDICO DE LA CLÍNICA SAN MARCOS", fontTitle2)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("Yo:" , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.NombreSolicitante, fontColumnSubrayado)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase(" Identificado con DNI Nº:" , fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.dniSolicitante, fontColumnSubrayado)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("Parentesco con el paciente" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.parentesco, fontColumnSubrayado)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase("(adjuntar carta poder simple)" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("con teléfono:" , fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.phoneSolicitante, fontColumnSubrayado)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase(" correo electrónico : " , fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.emailSolicitante, fontColumnSubrayado)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("me dirijo a usted para solicitarle tenga a bien otorgarme lo siguiente:", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase(" " , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase("a)	    Copia Historia Clínica" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.historia==true?"( X )":" ", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                   
                     new PdfPCell(new Phrase(" " , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase("b)	    Copia de exámenes auxiliares" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.examenes==true?"( X )":" ", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                
                     new PdfPCell(new Phrase(" " , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase("c)	    Certificado de atención" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.certificado==true?"( X )":" ", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                
                     new PdfPCell(new Phrase(" " , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase("d)    	Informe Medico" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.informe==true?"( X )":" ", fontColumnValueBold)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                
                     new PdfPCell(new Phrase(" " , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase("e)	    Otro: " , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.otros==true?"( X )":" ", fontColumnValueBold)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase(solicitud.otrosDescripcion, fontColumnSubrayado)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("Propósito de la solicitud:" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.proposito, fontColumnSubrayado)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("Nombres y apellidos del paciente:" , fontTitleTable)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(person.personName, fontColumnSubrayado)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase("Edad:" , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(person.edad, fontColumnSubrayado)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("DNI:" , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(person.numberDoc, fontColumnSubrayado)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase("HC:" , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.nroHistoria, fontColumnSubrayado)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase("Domicilio :" , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(person.domicilio, fontColumnSubrayado)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("Distrito  :" , fontTitleTable)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(person.distrito, fontColumnSubrayado)) {Colspan = 4, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase("Departamento  :" , fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(person.departamento, fontColumnSubrayado)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion

                     new PdfPCell(new Phrase("Sin otro particular me despido cordialmente de usted,", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 5, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("San Juan de Lurigancho, ", fontTitleTable)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(fecha, fontColumnSubrayado)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("Firma", fontColumnSubrayado)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("Huella", fontColumnSubrayado)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontTitle2)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase("CONSIDERACIONES PARA EL TRAMITE:", fontColumnValueApendice)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                     new PdfPCell(new Phrase("•", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("La historia clínica es un documento médico legal por lo tanto solo puede ser entregado al titular de la misma o con autorización", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("firmada a un tercero. En caso de tratarse de menor de edad o persona discapacitada se entregará al responsable o representante", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("legal del paciente, autoridades judiciales previstas en la Norma Técnica N° 022-MINSA/DGSP-V.02 Numeral VI.2.1.4 (Aprobada por ", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("Resolución Ministerial N° 597- 2006-MINSA).", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                     new PdfPCell(new Phrase("•", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("Toda documentación se entregará en un plazo no mayor a 06 días hábiles, de acuerdo a ley.", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                     new PdfPCell(new Phrase("•", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("El paciente dispone de un plazo máximo de 30 días desde la confirmación de la entrega de la solicitud para recoger el documento,", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("pasado este plazo, se deberá volver a presentar la solicitud y los pagos correspondientes que estos ameriten.", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                     new PdfPCell(new Phrase("•", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("El horario de entrega de la documentación solicitada es de lunes a sabado de 8:00 am a 13:00 pm y de 14:00 pm a 5:00 pm.", fontColumnValueApendice)) {Colspan = 9, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                     #endregion

                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.BLACK},    
                     #endregion

                     new PdfPCell(new Phrase("CARGO DE ENTREGA DE DOCUMENTACION:", fontColumnValueApendice)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 7, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("(Este apartado se llenará cuando se entregue la documentación solicitada, SOLO se entregará la documentación al solicitante, previa ", fontColumnValueApendice)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, VerticalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED_ALL, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("presentación del DNI) ", fontColumnValueApendice)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT, VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    

                     new PdfPCell(new Phrase("Nombres y Apellidos:" , fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.NombreSolicitante, fontColumnValueApendice)) {Colspan = 6, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase("DNI:" , fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(solicitud.dniSolicitante, fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase("Fecha:" , fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(fecha, fontColumnValueApendice)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE },
                     new PdfPCell(new Phrase("Nro de folios:" , fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER ,  VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     #region Line
                     new PdfPCell(new Phrase(" ", fontTitleTable)) {Colspan = 10, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,   VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     #endregion
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.BLACK,  BorderColorRight=BaseColor.BLACK,  BorderColorBottom=BaseColor.BLACK, BorderColorTop=BaseColor.BLACK},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, MinimumHeight = tamaño_celda2, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("Firma", fontColumnValueApendice)) {Colspan = 3, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 1, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase("Huella", fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
                     new PdfPCell(new Phrase(" ", fontColumnValueApendice)) {Colspan = 2, HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER, VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE, UseVariableBorders=true, BorderColorLeft=BaseColor.WHITE,  BorderColorRight=BaseColor.WHITE,  BorderColorBottom=BaseColor.WHITE, BorderColorTop=BaseColor.WHITE},    
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
