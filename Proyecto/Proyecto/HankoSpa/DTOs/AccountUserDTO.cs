namespace HankoSpa.DTOs
{
    public class AccountUserDTO
    {
        public Guid Id { get; set; }

        public string Document { get; set; }
        public string FirstName { get; set; }
        public string lastName{ get; set; }
        public string PhoneNumber { get; set; }

        public string Email{ get; set; }
    }
}
