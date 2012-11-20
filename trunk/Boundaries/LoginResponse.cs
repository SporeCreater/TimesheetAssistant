namespace Boundaries
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            ErrorMessage = string.Empty;
        }

        public bool WasSuccessful { get; set; }
        public string ErrorMessage { get; set; }

        public TimeCard TimeCard { get; set; }
    }
}