using System;
using System.Collections.Generic;
using System.Text;

namespace EV.Service.Models
{
    public class ResultServiceListModel<T> where T : class
    {
        public bool IsError { get; set; } = true;
        public List<T> Model { get; set; }
    }
}
