using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums.Units
{
    public enum GlucoseUnits
    {
        [Display(Name = "�����/�")]
        MmolPerLiter,
        [Display(Name = "��/��")]
        MgramPerDeciliter
    }
}