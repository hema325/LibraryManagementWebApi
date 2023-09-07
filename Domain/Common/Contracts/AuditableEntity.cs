namespace Domain.Common.Contracts
{
    public abstract class AuditableEntity: EntityBase
    {
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
