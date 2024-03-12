using Shared.Domain.Enum;

namespace Site.Application.Contract.BanerApplication.Query
{
    public class BanerForAdminQueryModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; }
        public BanerState State { get; set; }
        public bool ActivationChange { get; set; }
        public string CreationDate { get; set; }
    }
}
