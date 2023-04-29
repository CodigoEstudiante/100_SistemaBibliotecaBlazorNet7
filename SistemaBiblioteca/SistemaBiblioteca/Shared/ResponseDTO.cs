﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBiblioteca.Shared
{
    public class ResponseDTO<T>
    {
        public bool status { get; set; }
        public string? msg { get; set; }
        public T? value { get; set; }
    }
}
