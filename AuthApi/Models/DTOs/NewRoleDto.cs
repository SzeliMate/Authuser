namespace AuthApi.Models.DTOs
{
    public class NewRoleDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime? RegTime { get; set; }
    }
}
