﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.Core.models
{
    public class GptRequest
    {
        //public string Prompt { get; set; }
        //public string Question { get; set; }

        public List<Message> Messages { get; set; }
    }
}
