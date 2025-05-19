namespace HankoSpa.Nucleo
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Result { get; set; }

        // Constructor que inicializa las propiedades
        public Response(bool isSuccess, string message, T result)
        {
            IsSuccess = isSuccess;
            Message = message;
            Result = result;
            Errors = new List<string>();
        }

        // Constructor adicional para manejar errores
        public Response(bool isSuccess, string message, T result, List<string> errors)
        {
            IsSuccess = isSuccess;
            Message = message;
            Result = result;
            Errors = errors;
        }

        // Constructor adicional sin resultado
        public Response(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
            Result = default; // Asigna null o el valor predeterminado de T
            Errors = new List<string>();
        }

        public Response()
        {
            IsSuccess = false;
            Message = string.Empty;
            Result = default; // Asigna null o el valor predeterminado de T
            Errors = new List<string>();
        }
    }
}