using Barcoder.Code128;
using Barcoder.Renderer.Image;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LISCareReporting.LISBarcode
{
    public class BulkBarcodeGenerator
    {
        public static byte[] GenerateBulkBarcodes(int sequenceStart, int sequenceEnd)
        {
            var barcodes = new List<string>();
            for (int i = sequenceStart; i <= sequenceEnd; i++)
                barcodes.Add(i.ToString("D7"));

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // 🎯 Label size: 100 mm × 50 mm (≈ 283 × 142 points)
                    // page.Size(PageSizes.A4);
                    page.Size(new PageSize(283.46f, 141.73f)); // Custom label size (100mm x 50mm)
                    page.Margin(4); // very small margin
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Content().Element(RenderBarcodeBox(barcodes));
                });
            });

            return document.GeneratePdf();
        }

        private static Action<IContainer> RenderBarcodeBox(IEnumerable<string> barcodes)
        {
            return container =>
            {
                var barcodeList = barcodes.ToList();

                container
                    .Padding(0)
                    .Column(col =>
                    {
                        for (int i = 0; i < barcodeList.Count; i++)
                        {
                            var code = barcodeList[i];

                            var barcode = Code128Encoder.Encode(code);
                            var renderer = new ImageRenderer();
                            using var ms = new MemoryStream();
                            renderer.Render(barcode, ms);
                            var bytes = ms.ToArray();

                            col.Item().Border(0.0f).Padding(4).AlignCenter().Column(inner =>
                            {
                                inner.Spacing(2);
                                inner.Item().AlignCenter().Text(code).Bold();
                                inner.Item().AlignCenter().Height(110).Image(bytes);
                            });

                            // ✅ Add page break only between items, not after the last one
                            if (i < barcodeList.Count - 1)
                                col.Item().PageBreak();
                        }
                    });
            };
        }

    }
}
