﻿using System;
using System.Collections.Generic;

namespace EddieToNewFramework
{
    public partial class SelectedSymptomsN
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string SymptomName { get; set; }
        public string Selection { get; set; }
    }
}