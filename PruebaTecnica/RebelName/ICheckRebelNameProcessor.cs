using PruebaTecnica.Models;
using System;

namespace RebelName
{
    interface ICheckRebelNameProcessor
    {
        void CheckName(Rebel rebel, string name);
    }
}
