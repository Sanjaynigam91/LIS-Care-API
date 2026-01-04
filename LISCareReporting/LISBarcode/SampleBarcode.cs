using Barcoder;
using Barcoder.Code128;
using Barcoder.Renderer.Image;
using LISCareDTO.SampleAccession;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

namespace LISCareReporting.LISBarcode
{
    public class SampleBarcode
    {
        public static byte[] GenerateBarcodeLabel(BarcodeResponse barcodeResponse)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // ✅ 100mm x 50mm label (KEEP AS IS)
                    page.Size(new PageSize(283.46f, 141.73f));
                    page.Margin(4);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Content()
                        .Border(0)
                        .Padding(6)
                        .Column(col =>
                        {
                            col.Spacing(2);

                            // 🔹 HEADER ROW
                            // 🔹 DATE & TIME (TOP RIGHT)
                            col.Item()
                                .AlignRight()
                                .Text(barcodeResponse.RegisteredDate
                                    .ToString("dd-MM-yyyy HH:mm"))
                                .Bold()
                                .FontSize(8);

                            // 🔹 PATIENT NAME (NEXT ROW)
                            col.Item()
                                .Text(barcodeResponse.PatientName)
                                .Bold()
                                .FontSize(10);



                            // 🔹 BARCODE (🔥 FIXED – COMPACT LIKE FIRST SCREEN)
                            col.Item()
                                .AlignCenter()
                                .Element(container =>
                                {
                                    var barcode = Code128Encoder.Encode(barcodeResponse.Barcode);
                                    var renderer = new ImageRenderer();
                                    using var ms = new MemoryStream();
                                    renderer.Render(barcode, ms);
                                    var bytes = ms.ToArray();

                                    col.Item().Border(0.0f).Padding(4).AlignCenter().Column(inner =>
                                    {
                                        inner.Spacing(2);
                                        inner.Item().AlignCenter().Height(75).Image(bytes);
                                    });
                                    
                                });

                            // 🔹 FOOTER DETAILS
                            col.Item()
                                .Text(barcodeResponse.TestShortName)
                                .Bold()
                                .FontSize(9);
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
