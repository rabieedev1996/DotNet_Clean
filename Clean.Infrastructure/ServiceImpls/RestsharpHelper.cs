using Clean.Application.Contract.Services;
using RestSharp;

namespace Clean.Infrastructure.ServiceImpls;

public static class RestsharpHelper
{
    public static async Task<RestResponse> ExecuteAsync(this RestRequest request, ILogService _logService = null)
    {
        string logId = Guid.NewGuid().ToString();
        try
        {
            var parameters = request.Parameters;
            if (_logService != null)
                _logService.InsertLog("RestsharpHelper_ExecuteAsync", new { IsRequest = true, logId, parameters });

            var client = new RestClient();
            RestResponse response = await client.ExecuteAsync(request);

            if (_logService != null)
                _logService.InsertLog("RestsharpHelper_ExecuteAsync", new { IsRequest = false, logId, response });

            return response;
        }
        catch (Exception ex)
        {
            if (_logService != null)
                _logService.InsertLog("RestsharpHelper_ExecuteAsync", new { exception = ex, logId });
            throw;
        }
    }

    public static RestResponse Execute(this RestRequest request, ILogService _logService = null)
    {
        string logId = Guid.NewGuid().ToString();
        try
        {
            var parameters = request.Parameters;
            if (_logService != null)
                _logService.InsertLog("RestsharpHelper_ExecuteAsync", new { IsRequest = true, logId, parameters });

            var client = new RestClient();
            RestResponse response = client.Execute(request);

            if (_logService != null)
                _logService.InsertLog("RestsharpHelper_ExecuteAsync", new { IsRequest = false, logId, response });

            return response;
        }
        catch (Exception ex)
        {
            if (_logService != null)
                _logService.InsertLog("RestsharpHelper_Execute", new { exception = ex, logId });
            throw;
        }
    }
}