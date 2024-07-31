using Core.Domain.Persistence.SharedModels.Enum;

namespace Core.Domain.Persistence.SharedModels.Entities
{
    public class ApiPostObj
    {
        public string ApiBasicUrl { get; set; }
        public string Url { get; set; }
        public ApiPostCategoryType ApiPostCategoryType { get; set; }
    }
}
