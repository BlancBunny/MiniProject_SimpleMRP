using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRPApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MRPApp.Tests
{
    [TestClass()]
    public class SettingsTest
    {
        [TestMethod()]
        public void IsDuplicateDataTest()
        {
            var expectVal = true;
            var inputCode = "PC010001";

            var code = Logic.DataAccess.GetSettings().Where(d => d.BasicCode.Contains(inputCode)).FirstOrDefault();
            var realVal = code != null ? true : false;

            Assert.AreEqual(expectVal, realVal);

        }
    }
}