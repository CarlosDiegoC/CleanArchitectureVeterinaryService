namespace PetDream.Application.DTOs
{
    public class PetOwnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
    }
}