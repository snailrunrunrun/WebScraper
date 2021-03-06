﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebScraper.Helpers;

namespace WebScrapper.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestWikiUriBuilder()
        {
            var testUrl = UrlHelper.GetWikiUrl("Terracotta_Army");
            Assert.AreEqual(testUrl, "https://en.wikipedia.org/wiki/Terracotta_Army");
        }

        [TestMethod]
        public void TestGetFilePath()
        {
            var guidId = Guid.NewGuid().ToString();
            var filePath = FileHelper.GetFilePath(guidId);
            
            Assert.AreEqual(filePath, @"C:\_toDelete\" + guidId + ".json");
        }

        [TestMethod]
        public void TestScrapForResults()
        {
            var result = ScrapingJob.ScrapForResults(@"https://en.wikipedia.org/wiki/Terracotta_Army", "a");

            Assert.IsNotNull(result.Images);
        }

        [TestMethod]
        public void TestIsValidUrl()
        {
            var isValid = UrlHelper.IsValidUrl(@"https://en.wikipedia.org/wiki/The_Great_Wall_of_China/a");

            Assert.IsFalse(isValid);
        }
    }
}
