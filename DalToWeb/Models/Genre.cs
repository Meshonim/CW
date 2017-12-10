﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb.Models
{
    public class Genre
    {
        public short GenreId { get; set; }
        public string GenreName { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}