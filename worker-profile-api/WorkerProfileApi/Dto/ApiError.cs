namespace WorkerProfileApi.Dto
{
    public class ApiError
    {
        public ApiError(string error, string description, int statusCode)
        {
            this.Error = error;
            this.Description = description;
            this.StatusCode = statusCode;

        }
        public string Error { get; set; }

        public string Description { get; set; }

        public int StatusCode { get; set; }
    }
}