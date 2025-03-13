using System.Text.Json.Serialization;

namespace Common.Models.BaseModels
{
    public class ApiResponseModel<T> : IJSend<T>
    {
        [JsonIgnore]
        public StatusResponseEnum StatusAsEnum { get; set; } = StatusResponseEnum.Success;

        public string Status { get => StatusAsEnum.ToString(); }

        public T Data { get; set; }
    }

    public interface IJSend<T>
    {
        StatusResponseEnum StatusAsEnum { get; set; }

        public string Status { get; }

        public T Data { get; set; }
    }

    public enum StatusResponseEnum
    {
        Success,
        Fail,
        Error
    }
}
