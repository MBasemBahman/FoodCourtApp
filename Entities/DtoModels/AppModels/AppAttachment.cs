using Entities.DBModels.AppModels;
using Entities.Features.RequestFeatures;

namespace Entities.DtoModels.AppModels
{
    public class AppAttachmentParameters : RequestParameters
    {
        public int Fk_Branch { get; set; }
    }

    public class AppAttachmentDto : AttachmentEntityDto
    {
        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }

        [ForeignKey(nameof(Branch))]
        [DisplayName(nameof(Branch))]
        public int? Fk_Branch { get; set; }

        [DisplayName(nameof(Branch))]
        public BranchDto Branch { get; set; }
    }

    public class AppAttachmentEditDto
    {
        [ForeignKey(nameof(Fk_Branch))]
        public int? Fk_Branch { get; set; }

        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }
    }
}
