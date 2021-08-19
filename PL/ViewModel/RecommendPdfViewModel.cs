using BE;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PL.Commands;
using PL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
    public class RecommendPdfViewModel
    {
        internal void createPdf(DayOfWeek v)
        {
            BL.BL db = new BL.BL();
            Recommender rec = new Recommender();
            List<string> recResults = rec.GetRules(v);
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "My First PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 18, XFontStyle.Bold);
            XImage image = XImage.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//Pictures/grocery-cart.png");
            graph.DrawImage(image, 50, 50, 70, 70);
            graph.DrawString($"Recommendations for {v.ToString()}", font, XBrushes.Black, new XRect(20, 70, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
            int currentY = 200;
            float total = 0;
            foreach (var g in recResults)
            {

                graph.DrawString(" " + g, new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(100, currentY));

                string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//ProductPictures", $"{g}.png", SearchOption.AllDirectories);
                XImage prodImage = XImage.FromFile(files[0]);

                graph.DrawImage(prodImage, 300, currentY - 10, 20, 20);

                Product prod = db.GetProducts().Where(x => x.productName == g).FirstOrDefault();
                float price = prod.price;
                total += price;
                graph.DrawString(price + " Shekels", new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(400, currentY));
                currentY += 38;
            }

            graph.DrawString("Total Price: " + total + " Shekels", new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(400, currentY));
            graph.DrawString("Thank You for Choosing Retail Roundup!", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.BottomCenter);
            string pdfFilename = $"{v.ToString()}_recommendations.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);
        }
    }
}
