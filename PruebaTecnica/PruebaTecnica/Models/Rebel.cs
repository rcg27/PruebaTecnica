using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class Rebel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Planet { get; set; }
        public bool Register { get; set; }
        public DateTime Date { get; set; }

    }

    public class NullValueException : Exception
    {
        public NullValueException(string message) : base("Name can't be null")
        {

        }
        public NullValueException(string message, Exception inner) : base("Name can't be null", inner)
        {

        }
    }
}
