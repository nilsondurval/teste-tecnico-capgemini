using capgemini_api.Domain.Models.Enum;

namespace capgemini_api.Domain.Models.Classes
{
    public class ErrorMessage
    {
        public ErrorEnum Codigo { get; set; }

        public string Message { get; set; }

        public ErrorMessage()
        {

        }

        public ErrorMessage(ErrorEnum codigo, string message)
        {
            Codigo = codigo;
            Message = message;
        }
    }
}