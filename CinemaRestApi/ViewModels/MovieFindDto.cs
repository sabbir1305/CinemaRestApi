﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaRestApi.ViewModels
{
    public class MovieFindDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
