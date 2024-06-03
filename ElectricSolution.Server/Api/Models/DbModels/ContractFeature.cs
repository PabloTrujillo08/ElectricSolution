namespace ElectricSolution.Server.Api.Models.DbModels
{
    public class ContractFeature: BaseEntity
    {
        public int ContractId { get; set; } // Foreign key to Contract table
        public virtual Contract? Contract { get; set; }
        public int FeatureId { get; set; } // Foreign key to Feature table
        public virtual Feature? Feature { get; set; }
    }
}
