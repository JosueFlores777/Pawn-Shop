using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection.Metadata;

namespace pawnShop.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PDF()
        {
       
            FileStream fs = new FileStream("c://pdf/report.pdf", FileMode.Create) ;
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER,0,0,0,0) ;

            PdfWriter pw =  PdfWriter.GetInstance(document,fs);

            document.Open();
            document.Add(new Paragraph("Factire \n"));
            document.Close();



            return View();
        }
    }
}
