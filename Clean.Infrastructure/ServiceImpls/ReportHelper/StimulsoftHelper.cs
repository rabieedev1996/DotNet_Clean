using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;

namespace Clean.Infrastructure.ServiceImpls.ReportHelper;

public class StimulsoftHelper
{
    public byte[] GenerateReport(string templateAddress, string jsonData)
    {
        //return null;
        StiReport report = new StiReport();
        StiLicense.LoadFromString("6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkcgIvwL0jnpsDqRpWg5FI5kt2G7A0tYIcUygBh1sPs7uPvgjp0GgDowCB/F6myz180QOXN+hAWpeqWhPokj7sFSjITHge+0Hvjw4vKQPmlfDn/OWCMfhCPY4cZMTeUW6cW2VSK+480C7TeIrX/O/tLgGrgklP1P/7MdEkP/gQjQIwyRizsmj17wLkWfRzMal1duePiYgMsYr8GE9AdT2Mz6RPH+SCwPKHdjCq5PI/k4SrswBNYyd60U3YHOve2dNPfteBnaTnzwpyfuKQSyJrPuccoqDVxIUWSF8GCXtQa2nf7qqvv7A9L4L2LSU3JS31y3NU4ykT1r2gg8lkLmXQlauRyq3SR3zhTCvr1gsuM8a/85YPA2KCT4T2X14/Sj6Z3uo9x8lFQPOsW3fk1us4HDqN54uz7DOynURHiLJ5Twy7m2SzZhgg7QKO07CZgff70N6ID1D/h2G8pjVhsUO5qkWEkdr2kj8ygbUq5OZcMYTuQXkt1+sVOet7/cmQGdjsxperXlhn/96fbzPPn/q4Q");
        string reportPath = templateAddress;
        var reportByteArray = FileToByteArray(reportPath);
        report.Load(reportByteArray);

        //رشته جیسون داده ها
        var json = jsonData;

        var data = StiJsonToDataSetConverterV2.GetDataSet(json);
        report.RegData(data);
        MemoryStream stream = new MemoryStream();
        StiPdfExportSettings settings = new StiPdfExportSettings();
        settings.AutoPrintMode = StiPdfAutoPrintMode.Dialog;
        report.Render(true);

        StiPdfExportService service = new StiPdfExportService();
        service.ExportPdf(report, stream, settings);
        var byteArray = stream.ToArray();
        stream.Close();
        stream.Dispose();
        return byteArray;
    }

    public static byte[] FileToByteArray(string path)
    {
        var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        var byteArray = memoryStream.ToArray();
        fileStream.Close();
        memoryStream.Dispose();
        return byteArray;
    }
}