using BE;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
    public class RecieptViewModel
    {
        internal void createPdf(Purchase realPurchase)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "My First PDF";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 14, XFontStyle.Bold);
            XImage image = XImage.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//Pictures/grocery-cart.png");
            graph.DrawImage(image, 50, 50, 70, 70);

            if (realPurchase.products != null)
            {
                var groups = realPurchase.products.GroupBy(x => x.productName);
                graph.DrawString($"Receipt for Shopping at:  {realPurchase.seller}", font, XBrushes.Black, new XRect(20, 70, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
                graph.DrawString($"on {realPurchase.purchaseDate}", font, XBrushes.Black, new XRect(20, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);

                graph.DrawString("Product Name", new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(150, 200));

                graph.DrawString("Quantity", new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(350, 200));

                graph.DrawString("Price", new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(450, 200));
                int currentY = 238;
                float total = 0;
                foreach (var g in groups)
                {

                    var prodd = g.FirstOrDefault();

                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//ProductPictures", $"{prodd.productName}.png", SearchOption.AllDirectories);
                    XImage prodImage = XImage.FromFile(files[0]);

                    graph.DrawImage(prodImage, 100, currentY - 10, 20, 20);
                    graph.DrawString(" " + prodd.productName, new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(150, currentY));


                    float price = prodd.price;
                    total += price * g.Count();

                    graph.DrawString(g.Count().ToString(), new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(350, currentY));
                    graph.DrawString(price.ToString(), new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(450, currentY));
                    currentY += 38;
                }

                graph.DrawString("Total Price: " + total + " Shekels", new XFont("Verdama", 14, XFontStyle.Regular), XBrushes.Black, new XPoint(235, currentY));
                graph.DrawString("Thank You for Choosing Retail Roundup!", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.BottomCenter);
                string pdfFilename = "receipt.pdf";
                pdf.Save(pdfFilename);
                Process.Start(pdfFilename);
            }
        }
    }
}
