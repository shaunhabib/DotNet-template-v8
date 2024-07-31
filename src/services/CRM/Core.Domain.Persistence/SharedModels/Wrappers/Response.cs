namespace Core.Domain.Persistence.SharedModels.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public List<string> ApiMessages { get; set; }

        public static Response<T> Success()
        {
            var result = new Response<T> 
            { 
                Succeeded = true 
            };
            return result;
        }
        public static Response<T> Success(string message)
        {
            var result = new Response<T> 
            { 
                Succeeded = true, 
                Message = message 
            };
            return result;
        }

        public static Response<T> Success(T data, string message)
        {
            var result = new Response<T> 
            { 
                Succeeded = true, 
                Data = data, 
                Message = message 
            };
            return result;
        }

        public static Response<T> Success(T data, string message, List<string> messages)
        {
            var result = new Response<T>
            {
                Succeeded = true,
                Data = data,
                Message = message,
                ApiMessages = messages
            };
            return result;
        }

        public static Response<T> Fail()
        {
            var result = new Response<T> 
            { 
                Succeeded = false 
            };
            return result;
        }

        public static Response<T> Fail(string message)
        {
            var result = new Response<T> 
            { 
                Succeeded = false,
                Message = message 
            };
            return result;
        }

        public static Response<T> Fail(string message, List<string> errors)
        {
            var result = new Response<T> 
            { 
                Succeeded = false,
                Message = message, 
                Errors = errors 
            };
            return result;
        }

        public static Response<T> Fail(string message, List<string> errors, List<string> messages)
        {
            var result = new Response<T>
            {
                Succeeded = false,
                Message = message,
                Errors = errors,
                ApiMessages = messages
            };
            return result;
        }

        public static Response<T> Fail(List<string> errors)
        {
            var result = new Response<T> 
            { 
                Succeeded = false,
                Errors = errors 
            };
            return result;
        }
    }
}
