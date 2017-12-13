using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CW.ViewModels
{
    public class EditionViewModel
    {
        public int EditionId { get; set; }
        [Display(Name = "Edition title")]
        [Required]
        public string EditionTitle { get; set; }
        [Display(Name = "Edition year")]
        [Required]
        public short EditionYear { get; set; }
        public byte[] EditionImage { get; set; }
        [Display(Name = "House Id")]
        public int HouseId { get; set; }
        [Display(Name = "Language Id")]
        public short LanguageId { get; set; }
        [Display(Name = "Ediiton type Id")]
        public int EditionTypeId { get; set; }

        public List<short> GenreIds { get; set; }
        public MultiSelectList Genres { get; set; }

        public List<int> AuthorIds { get; set; }
        public MultiSelectList Authors { get; set; }

        public List<int> TranslatorIds { get; set; }
        public MultiSelectList Translators { get; set; }

        public List<int> IllustratorIds { get; set; }
        public MultiSelectList Illustrators { get; set; }
    }
}