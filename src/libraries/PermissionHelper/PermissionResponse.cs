using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionHelper
{
    public class PermissionResponse<T>
    {
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public static PermissionResponse<T> Success()
        {
            var result = new PermissionResponse<T> { Succeeded = true };
            return result;
        }
        public static PermissionResponse<T> Success(string message)
        {
            var result = new PermissionResponse<T> { Succeeded = true, Message = message };
            return result;
        }

        public static PermissionResponse<T> Success(T data, string message)
        {
            var result = new PermissionResponse<T> { Succeeded = true, Data = data, Message = message };
            return result;
        }

        public static PermissionResponse<T> Fail()
        {
            var result = new PermissionResponse<T> { Succeeded = false };
            return result;
        }

        public static PermissionResponse<T> Fail(string message)
        {
            var result = new PermissionResponse<T> { Succeeded = false, Message = message };
            return result;
        }

        public static PermissionResponse<T> Fail(string message, List<string> errors)
        {
            var result = new PermissionResponse<T> { Succeeded = false, Message = message, Errors = errors };
            return result;
        }

        public static PermissionResponse<T> Fail(List<string> errors)
        {
            var result = new PermissionResponse<T> { Succeeded = false, Errors = errors };
            return result;
        }
    }
}
