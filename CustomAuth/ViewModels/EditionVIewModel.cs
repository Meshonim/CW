using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CW.ViewModels
{
    public class EditionViewModel
    {
        public int EditionId { get; set; }
        public string EditionTitle { get; set; }
        public short EditionYear { get; set; }
        public byte[] EditionImage { get; set; }

        public int HouseId { get; set; }

        public short LanguageId { get; set; }

        public List<short> GenreIds { get; set; }
        public MultiSelectList Genres { get; set; }

        public List<int> AuthorIds { get; set; }
        public MultiSelectList Authors { get; set; }

        public List<int> TranslatorIds { get; set; }
        public MultiSelectList Translators { get; set; }
    }
}