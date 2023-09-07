namespace Infrastructure.Identity.Models
{
    internal class RefreshToken
    {
        public string Token { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpriesOn { get; set; }
        public DateTime? RevokedOn { get; set; }
    }
}
