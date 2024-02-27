namespace B3110SQLInjectionProjectASPNETCore.Domain
{
    public class UserProfile
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;
        private string _role = string.Empty;

        
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Email { get { return _email; } set { _email = value; } }  
        public string Role { get { return _role; } set { _role = value; } }
        
    }
}
