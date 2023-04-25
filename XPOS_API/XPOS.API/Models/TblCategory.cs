﻿using System;
using System.Collections.Generic;

namespace XPOS.API.Models
{
    public partial class TblCategory
    {
        public int Id { get; set; }
        public string NamaCategory { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool? IsDelete { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
