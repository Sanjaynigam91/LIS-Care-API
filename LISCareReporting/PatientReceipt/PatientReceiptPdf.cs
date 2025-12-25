using LISCareDTO.FrontDesk;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class PatientReceiptPdf : IDocument
{
    private readonly PatientReceipt patientReceipt;

    public PatientReceiptPdf(PatientReceipt patientReceipt)
    {
        this.patientReceipt = patientReceipt;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(25);
            page.DefaultTextStyle(x => x.FontSize(10));

            page.Header().Element(Header);
            page.Content().Element(Content);
        });
    }

    void Header(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Row(row =>
            {
                if (!string.IsNullOrEmpty(patientReceipt.ReceiptLogo))
                {
                    byte[] logoBytes = Convert.FromBase64String(
                        patientReceipt.ReceiptLogo.Split(',')[1]
                    );

                    row.AutoItem()
                    .Height(50)
                    .Image(logoBytes)
                    .FitHeight();
                }

                row.RelativeItem().Column(c =>
                {
                    c.Item().Text(patientReceipt.MainLabName).FontSize(18).Bold();
                    c.Item().Text("Inspiring Better Health");
                });

                row.ConstantItem(180).AlignRight().Text(text =>
                {
                    text.Line($"Print Date : {patientReceipt.ReceiptDate}");
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                });
            });

            col.Item().AlignCenter().Text(patientReceipt.CentreName).Bold();
            col.Item().AlignCenter().Text("PATIENT RECEIPT").Bold().FontSize(14);

            col.Item().PaddingVertical(5).LineHorizontal(1);
        });
    }

    void Content(IContainer container)
    {
        container.Column(col =>
        {
            // ================= Patient + Centre Info =================
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(left =>
                {
                    InfoRow(left, "Patient UID", patientReceipt.PatientId);
                    InfoRow(left, "Patient Name", patientReceipt.PatientName);
                    InfoRow(left, "Age / Gender", patientReceipt.AgeGender);
                    InfoRow(left, "Date", patientReceipt.ReceiptDate.ToString("dd MMM yyyy"));
                });

                row.RelativeItem().Column(right =>
                {
                    InfoRow(right, "Centre Name", patientReceipt.CentreName);
                    InfoRow(right, "Ref. Doctor Name", patientReceipt.DoctorName);
                    InfoRow(right, "Bill No", patientReceipt.BillNo > 0
                      ? patientReceipt.BillNo.ToString()
                      : string.Empty);

                });
            });

            col.Item().PaddingVertical(8).LineHorizontal(1);

            // ================= Services Table =================
            col.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(6);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Test Name").Bold();
                    header.Cell().AlignCenter().Text("Qty").Bold();
                    header.Cell().AlignRight().Text("Test Price").Bold();
                    header.Cell().AlignRight().Text("Gross Amount").Bold();
                    header.Cell().AlignRight().Text("Net Amount").Bold();
                });

                int index = 1;
                foreach (var item in patientReceipt.Items)
                {
                    table.Cell().Text($"{index++}.".PadRight(2) + item.ServiceName);
                    table.Cell().AlignCenter().Text(item.Qty.ToString());
                    table.Cell().AlignRight().Text(item.Rate.ToString("0.00"));
                    table.Cell().AlignRight().Text(item.Gross.ToString("0.00"));
                    table.Cell().AlignRight().Text(item.Gross.ToString("0.00"));
                }
            });

            col.Item().PaddingVertical(8).LineHorizontal(1);

            // ================= Totals =================
            col.Item().Row(row =>
            {
                row.RelativeItem(3);

                row.RelativeItem(2).Column(totals =>
                {
                    TotalsRow(totals, "Total Bill Amount", patientReceipt.TotalAmount);
                    TotalsRow(totals, "Less Discount", patientReceipt.Discount);
                    TotalsRow(totals, "Net Amount", patientReceipt.NetAmount);
                    TotalsRow(totals, "Final Paid Amount", patientReceipt.PaidAmount);
                    TotalsRow(totals, "Balance Amount", patientReceipt.BalanceAmount);
                });
            });

            // ================= Bottom Info =================
            col.Item().PaddingTop(10).Column(bottom =>
            {
                InfoRow(bottom, "Amount in Words", patientReceipt.AmountInWords);
               // InfoRow(bottom, "Receipt No", patientReceipt.BillNo);
                InfoRow(bottom, "Receipt No", patientReceipt.BillNo > 0
                ? patientReceipt.BillNo.ToString()
                : string.Empty);

                InfoRow(bottom, "Cash Pay", patientReceipt.PaidAmount.ToString("0.00"));
            });

            // ================= Footer (CONTENT-BASED) =================
            Footer(col.Item());
        });
    }

    // ================= Helper Methods =================

    void InfoRow(ColumnDescriptor column, string label, string value)
    {
        column.Item().Row(row =>
        {
            row.ConstantItem(110).Text(label);
            row.ConstantItem(10).Text(":");
            row.RelativeItem().Text(value);
        });
    }

    void TotalsRow(ColumnDescriptor column, string label, decimal value)
    {
        column.Item().Row(row =>
        {
            row.RelativeItem(110).Text(label);
            row.RelativeItem(10).AlignLeft().Text(":");
            row.ConstantItem(80).AlignRight().Text(value.ToString("0.00"));
        });
    }
    void Footer(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().PaddingTop(15).LineHorizontal(1);

            col.Item().PaddingTop(10).Row(row =>
            {
                row.RelativeItem(); // left empty space

                row.ConstantItem(200).AlignRight().Column(c =>
                {
                    c.Item().Text(patientReceipt.PreparedBy).Bold();
                    c.Item().Text("Prepared By").FontSize(9);
                });
            });
        });
    }

}
