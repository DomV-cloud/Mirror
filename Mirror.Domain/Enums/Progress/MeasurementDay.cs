using System.ComponentModel.DataAnnotations;

namespace Mirror.Domain.Enums.Progress
{
    public enum MeasurementDay
    {
        [Display(Name = "Monday")]
        Monday,
        [Display(Name = "Tuesday")]
        Tuesday,
        [Display(Name = "Wednesday")]
        Wednesday,
        [Display(Name = "Thursday")]
        Thursday,
        [Display(Name = "Friday")]
        Friday,
        [Display(Name = "Saturday")]
        Saturday,
        [Display(Name = "Sunday")]
        Sunday
    }
}
