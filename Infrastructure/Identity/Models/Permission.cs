namespace Infrastructure.Identity.Models
{
    internal class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ApplicationUser> Users { get; set; }
    }
}
