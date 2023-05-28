﻿using Microsoft.EntityFrameworkCore;

namespace Revas.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        
    }
}
