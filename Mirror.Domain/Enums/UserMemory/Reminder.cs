using System.ComponentModel.DataAnnotations;

namespace Mirror.Domain.Enums.UserMemory
{
    public enum Reminder
    {
        [Display(Name = "Daily")]
        Daily = 0,

        [Display(Name = "Weekly")]
        Weekly = 1,
        
        [Display(Name = "Monthly")]
        Monthly = 2,
        
        [Display(Name = "Yearly")]
        Yearly = 3
    }
}
