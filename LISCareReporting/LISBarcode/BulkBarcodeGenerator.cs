using Barcoder.Code128;
using Barcoder.Renderer.Image;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCareReporting.LISBarcode
{
    public class BulkBarcodeGenerator
    {
        public static byte[] GenerateBulkBarcodes(int SequenceStart, int SequenceEnd)
        {
            var barcodes = new List<string>();
            for (int i = SequenceStart; i <= SequenceEnd; i++)
                barcodes.Add(i.ToString("D7")); // Example: 00023989

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        foreach (var code in barcodes)
                            col.Item().Element(RenderBarcode(code)); // ✅ Correct usage
                    });
                });
            });

            return document.GeneratePdf();
        }

        // ✅ FIXED: Return Action<IContainer>, not IContainer
        private static Action<IContainer> RenderBarcode(string code)
        {
            var barcode = Code128Encoder.Encode(code);
            var renderer = new ImageRenderer();

            using var ms = new MemoryStream();
            renderer.Render(barcode, ms);
            var bytes = ms.ToArray();

            return container =>
            {
                container
                    .Border(1)
                    .Padding(10)
                    .AlignCenter()
                    .Column(col =>
                    {
                        col.Item().Text(code).Bold().FontSize(12).AlignCenter();
                        //col.Item().Image(bytes).FitHeight();
                        col.Item().Image(bytes).FitArea();

                    });
            };
        }
    }
}
