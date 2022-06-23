using System.ComponentModel.DataAnnotations;

namespace Diabetes.Domain.Normalized.Enums.Units
{
    public enum CarbohydrateUnits
    {
        [Display(Name = "�������� � �������")]
        Carbohydrate,
        [Display(Name = "������� ������� (��)")]
        BreadUnits
    }
}