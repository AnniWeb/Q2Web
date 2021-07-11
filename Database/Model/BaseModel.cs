using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class BaseModel<TUniqueId>
        where TUniqueId : struct
    {
        [Required]
        public TUniqueId Id { get; set; }
    }
}