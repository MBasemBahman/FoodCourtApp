namespace Entities.DtoModels.AppModels
{
    public class BranchDto : AuditImageEntityDto
    {
        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }

        [DisplayName(nameof(ShopCount))]
        public int ShopCount { get; set; }
    }
    public class BranchCreateOrEditDto
    {

        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }

        [DisplayName(nameof(BranchImageFile))]
        public IFormFile BranchImageFile { get; set; }

        public string ImageUrl { get; set; }
    }
}
