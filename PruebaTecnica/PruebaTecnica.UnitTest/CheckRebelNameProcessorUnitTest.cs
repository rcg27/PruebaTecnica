using RebelName;
using System;
using Xunit;

namespace PruebaTecnica.UnitTest
{
    public class CheckRebelNameProcessorUnitTest
    {
        [Fact]
        public void Invalid_Rebel_Id()
        {
            var checkRebelName = new CheckRebelNameProcessor();
            Assert.Throws<ArgumentOutOfRangeException>(() => checkRebelName.CheckName(new Models.Rebel(), "name"));
        }

        [Fact]
        public void Invalid_Rebel_Name()
        {
            var checkRebelName = new CheckRebelNameProcessor();
            Assert.Throws<ArgumentNullException>(() => checkRebelName.CheckName(new Models.Rebel { Id = 1 }, ""));
        }
    }
}
