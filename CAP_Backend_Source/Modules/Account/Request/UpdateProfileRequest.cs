namespace CAP_Backend_Source.Modules.Account.Request
{
    public class UpdateProfileRequest
    {
        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public int? PositionId { get; set; }

        public int? FacultyId { get; set; }
    }
}
