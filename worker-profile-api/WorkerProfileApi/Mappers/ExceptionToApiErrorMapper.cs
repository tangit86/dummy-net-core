using System;
using WorkerProfileApi.Dto;
using WorkerProfileApi.Exceptions;

namespace WorkerProfileApi.Mappers
{
    public class ExceptionToApiErrorMapper : IExceptionToApiErrorMapper
    {
        public ApiError map(Exception ex, string eventId)
        {
            var x = ex as ApiException;
            if (x != null)
            {
                return new ApiError(x.TextCode, x.Message, x.Code);
            }
            return new ApiError("UNHANDLED_EXCEPTION", eventId, 500);
        }
    }

    public interface IExceptionToApiErrorMapper
    {
        ApiError map(Exception ex, string eventId);
    }
}