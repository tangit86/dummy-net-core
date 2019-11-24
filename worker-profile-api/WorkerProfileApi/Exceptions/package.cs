using System;

namespace WorkerProfileApi.Exceptions
{
    public abstract class ApiException : Exception
    {
        public string TextCode { get; set; }
        public int Code { get; set; }
        public ApiException(string message, string textCode, int code) : base(message)
        {
            this.TextCode = textCode;
            this.Code = code;
        }
    }

    public class ProfileNotFoundException : ApiException
    {
        public ProfileNotFoundException(string msg) : base(msg, "RESOURCE_NOT_FOUND", 404) { }
    }

    public class UnauthorizedResourceAccessException : ApiException
    {
        public UnauthorizedResourceAccessException(string msg) : base(msg, "UNAUTHORIZED_RESOURCE_ACCESS", 403) { }
    }

    public class ProfileExistsException : ApiException
    {
        public ProfileExistsException(string msg) : base(msg, "DUPLICATE_RESOURCE", 409) { }
    }

    public class InvalidInputException : ApiException
    {
        public InvalidInputException(string msg) : base(msg, "INVALID_INPUT", 422) { }
    }
}