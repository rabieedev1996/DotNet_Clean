using Clean.Application.Contract.Services;
using Clean.Infrastructure.ServiceImpls.ReportHelper;

namespace Clean.Infrastructure.Service;

public class ReportService:IReportService
{
    private StimulsoftHelper _stimulsoftHelper;

    public ReportService()
    {
        _stimulsoftHelper = new StimulsoftHelper();
    }

    public byte[] SamplePDFReport()
    {
        string json = "{}";
        var report= _stimulsoftHelper.GenerateReport(Environment.CurrentDirectory + @"/reports/sample.mrt",json);
        return report;
    }

   
}