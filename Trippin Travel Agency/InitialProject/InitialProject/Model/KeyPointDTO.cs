﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public  class KeyPointDTO
    {

        public string name {  get; set; }
        public bool visited { get; set; }

        public KeyPointDTO(string name, bool visited)
        {
            this.name = name;
            this.visited = visited;
        }
    }
}