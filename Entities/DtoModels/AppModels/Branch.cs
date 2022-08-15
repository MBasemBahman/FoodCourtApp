namespace Entities.DtoModels.AppModels
{
    public class BranchDto : AuditImageEntityDto
    {
        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        public int ShopCount { get; set; }
    }
}
