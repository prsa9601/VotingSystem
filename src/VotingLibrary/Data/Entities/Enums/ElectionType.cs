using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VotingLibrary.Data.Entities.Enums
{
    public enum ElectionType
    {
        [Display(Name = "یک رای")]
        یک_رای, //= 1,
        [Display(Name = "دو رای")]
        دو_رای, //= 2,
        [Display(Name = "سه رای")]
        سه_رای,// = 3,
        [Display(Name = "پنج رای")]
        پنج_رای //= 5
    }
}
