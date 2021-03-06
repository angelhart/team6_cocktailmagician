﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Models;
using CM.Models.BaseClasses;

namespace CM.DTOs
{
	public class BarDTO
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public string FullAddress { get; set; }
        public string Phone { get; set; }
        public string Details { get; set; }
        public double? AverageRating { get; set; }
        public string ImagePath { get; set; }
        public bool IsUnlisted { get; set; }

        public ICollection<BarCommentDTO> Comments { get; set; }
        public ICollection<CocktailDTO> Cocktails { get; set; }
    }
}
