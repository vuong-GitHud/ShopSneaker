using System.Net;
using System.Runtime.Serialization;

namespace ShopSneaker.Infacture.Helper;

public class BaseResponse<TResponse>
    {
        public HttpStatusCode StatusCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Message { get; set; }
        public TResponse ResponseData { get; set; }
        public object ResponseHeader { get; set; }
        public string FullResponseString { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string FullMsg { get; set; }
        public bool IsSuccessStatusCode => StatusCode == HttpStatusCode.OK;
        public string oStatusCode { get; set; }
        public string ResourceKey { get; set; }
        public double TimeExecute { get; set; }
        public string TrackingMessage { get; set; }
        public bool IsValidResponse => IsSuccessStatusCode && ResponseData != null;

        public static BaseResponse<TResponse> Success(TResponse data = default(TResponse), string message = "Success", string resourceKey = "SUCCESS", string fullMsg = "", string fullResponseString = "", double timeExecute = 0, string trackingMessage = "")
        {
            return new BaseResponse<TResponse>()
            {
                StatusCode = HttpStatusCode.OK,
                oStatusCode = "200",
                ResponseData = data,
                ResourceKey = resourceKey,
                Message = message,
                ResponseHeader = null,
                FullResponseString = fullResponseString,
                FullMsg = fullMsg,
                TimeExecute = timeExecute,
                TrackingMessage = trackingMessage
            };
        }
        public static BaseResponse<TResponse> InternalServerError(Exception ex, TResponse data = default(TResponse), string resourceKey = "INTERNAL_SERVER_ERROR", string fullResponseString = "", double timeExecute = 0, string trackingMessage = "")
        {
            return new BaseResponse<TResponse>()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                oStatusCode = "500",
                ResponseData = data,
                ResourceKey = resourceKey,
                Message = ex.Message,
                ResponseHeader = null,
                FullResponseString = fullResponseString,
                FullMsg = ex.StackTrace,
                TimeExecute = timeExecute,
                TrackingMessage = trackingMessage
            };
        }

        public static BaseResponse<TResponse> InternalServerError(TResponse data = default(TResponse), string message = "Internal Server Error", string resourceKey = "INTERNAL_SERVER_ERROR", string fullMsg = "", string fullResponseString = "", double timeExecute = 0, string trackingMessage = "")
        {
            return new BaseResponse<TResponse>()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                oStatusCode = "500",
                ResponseData = data,
                ResourceKey = resourceKey,
                Message = message,
                ResponseHeader = null,
                FullResponseString = fullResponseString,
                FullMsg = fullMsg,
                TimeExecute = timeExecute,
                TrackingMessage = trackingMessage
            };
        }

        public static BaseResponse<TResponse> BadRequest(TResponse data = default(TResponse), string message = "Bad Request", string resourceKey = "BAD_REQUEST", string fullMsg = "", string fullResponseString = "", double timeExecute = 0, string trackingMessage = "")
        {
            return new BaseResponse<TResponse>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                oStatusCode = "400",
                ResponseData = data,
                ResourceKey = resourceKey,
                Message = message,
                ResponseHeader = null,
                FullResponseString = fullResponseString,
                FullMsg = fullMsg,
                TimeExecute = timeExecute,
                TrackingMessage = trackingMessage
            };
        }

        public static BaseResponse<TResponse> NotFound(TResponse data = default(TResponse), string message = "Data not found", string resourceKey = "DATA_NOT_FOUND", string fullMsg = "", string fullResponseString = "", double timeExecute = 0, string trackingMessage = "")
        {
            return new BaseResponse<TResponse>()
            {
                StatusCode = HttpStatusCode.NotFound,
                oStatusCode = "404",
                ResponseData = data,
                ResourceKey = resourceKey,
                Message = message,
                ResponseHeader = null,
                FullResponseString = fullResponseString,
                FullMsg = fullMsg,
                TimeExecute = timeExecute,
                TrackingMessage = trackingMessage
            };
        }

        public static BaseResponse<TResponse> NoContent(TResponse data = default(TResponse), string message = "No Content", string resourceKey = "NO_CONTENT", string fullMsg = "", string fullResponseString = "", double timeExecute = 0, string trackingMessage = "")
        {
            return new BaseResponse<TResponse>()
            {
                StatusCode = HttpStatusCode.NoContent,
                oStatusCode = "204",
                ResponseData = data,
                ResourceKey = resourceKey,
                Message = message,
                ResponseHeader = null,
                FullResponseString = fullResponseString,
                FullMsg = fullMsg,
                TimeExecute = timeExecute,
                TrackingMessage = trackingMessage
            };
        }
    }