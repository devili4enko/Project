using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Domain;

namespace Project.Test
{
    [TestClass]
    public class TestForTranslitiration
    {
        private TranslitWithXMLOptions tr;

        [TestInitialize]
        public void Initialize()
        {
            tr = new TranslitWithXMLOptions();
        }

        [TestMethod]
        public void XMLTestGetЗгReturnsZgh()
        {
            var res = tr.Translit("Зг");
            Assert.AreEqual(res, "Zgh");
        }

        [TestMethod]
        public void XMLTestGetНнReturnsNn()
        {
            var res = tr.Translit("Нн");
            Assert.AreEqual(res, "Nn");

        }


        [TestMethod]
        public void XMLTestFirstLetterRule()
        {
            var res = tr.Translit("Яяя");
            Assert.AreEqual(res, "Yaiaia");

        }

        [TestMethod]
        public void XMLTestDoubleDotIAAReturnsYiAA()
        {

            var result = tr.Translit("ЇАА");

            Assert.AreEqual(result, "YiAA");

        }


        [TestMethod]
        public void XMLTestСумиDotIAAReturnsSumy()
        {

            var result = tr.Translit("Суми");

            Assert.AreEqual(result, "Sumy");
        }

        [TestMethod]
        public void XMLTestЄнакієвеDotIAAReturnsYenakiieve()
        {
            var result = tr.Translit("Єнакієве");
            Assert.AreEqual(result, "Yenakiieve");
        }

        [TestMethod]
        public void XMLTestТроцьDotIAAReturnsTrots()
        {
            var result = tr.Translit("Троць");
            Assert.AreEqual(result, "Trots");
        }

        [TestMethod]
        public void XMLTestХарків_ЮрійDotIAAReturnsKharkiv_Yurii()
        {
            var result = tr.Translit("Харків Юрій");
            Assert.AreEqual(result, "Kharkiv Yurii");
        }

        [TestMethod]
        public void XMLTestЗгораниReturnsZghorany()
        {

            var result = tr.Translit("Згорани Розгон");

            Assert.AreEqual(result, "Zghorany Rozghon");

        }
    }
}
