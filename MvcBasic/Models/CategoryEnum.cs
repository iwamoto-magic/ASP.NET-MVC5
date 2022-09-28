using System.ComponentModel.DataAnnotations;

namespace MvcClient.Models
{
    public enum CategoryEnum
    {
        [Display(Name = ".NET")]
        DotNet,

        [Display(Name = "クラウド")]
        Cloud,

        [Display(Name = "リファレンス")]
        Reference

    }
}