using System.Diagnostics.CodeAnalysis;

namespace HRMS.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class JsonResultModel<T>
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public bool IsError { get; set; }

        public T? Data { get; set; }
    }

    public class JsonResultModelList<T>
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public bool IsError { get; set; }
        public List<T>? Data { get; set; }
    }
}
