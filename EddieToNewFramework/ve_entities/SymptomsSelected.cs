﻿using System;
using System.Collections.Generic;

namespace EddieToNewFramework.ve_entities
{
    public partial class SymptomsSelected
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public int SymptomId { get; set; }
        public string Selection { get; set; }
    }
}
