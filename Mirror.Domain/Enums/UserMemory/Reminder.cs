using System.ComponentModel.DataAnnotations;

namespace Mirror.Domain.Enums.UserMemory
{
    public enum Reminder
    {
        [Display(Name = "Daily")]
        daily = 0,

        [Display(Name = "Weekly")]
        weekly = 1,
        
        [Display(Name = "monthly")]
        monthly = 2,
        
        [Display(Name = "yearly")]
        yearly = 3
    }
}
