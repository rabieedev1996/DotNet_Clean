using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Utility;
namespace Clean.Application.Models
{
    public class LogBaseModel
    {
        public LogBaseModel()
        {
            var now = DateTime.Now;
            CreatedAt = now.ToString("yyyy-MM-dd HH:mm:ss");
            CreatedAtFa = now.ToFa("yyyy-MM-dd")+" "+now.ToString("HH:mm:ss");
            CreatedAtDateKey = long.Parse(now.ToString("yyyyMMddHHmmss"));
            CreatedAtFaDateKey = long.Parse(now.ToFa("yyyyMMdd")+now.ToString("HHmmss"));
        }
        public string CreatedAt { set; get; }
        public string CreatedAtFa { set; get; }
        public long CreatedAtDateKey { set; get; }
        public long CreatedAtFaDateKey { set; get; }
        public string Time { set; get; }
    }
    public class ApiServiceLogViewModels<TData> : LogBaseModel
    {
        public string ApiLogId { set; get; }
        public TData ApiData { set; get; }
        public bool IsResponse { set; get; }
        public string BrokerId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Path { get; set; }
    }
    public class ApiExceptionLogViewModels : LogBaseModel
    {
        public string ApiLogId { set; get; }
        public Exception Exception { set; get; }
        public bool IsResponse { set; get; }
        public string BrokerId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
    public class LogViewModels<TData> : LogBaseModel
    {
        public TData Data { set; get; }
        public string Title { get; set; }
    }
}
