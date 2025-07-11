﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Domaine.Core.DTO
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message {  get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
