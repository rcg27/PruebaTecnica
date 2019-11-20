using System;
using System.Collections.Generic;
using System.Text;
using PruebaTecnica.Models;

namespace RebelName
{
    public class CheckRebelNameProcessor : ICheckRebelNameProcessor
    {
        public void CheckName(Rebel rebel, string name)
        {
            if(rebel.Id <= 0)
            {
                throw new ArgumentOutOfRangeException("Rebel Id can't be less than 0");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Rebel has to have a name");
            }
        }
    }
}
