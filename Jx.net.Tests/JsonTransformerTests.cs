﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Jx.net.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Jx.net.Transformer;

namespace Jx.net.Tests
{
    [TestClass]
    public class JsonTransformerTests
    {
        string testsRootPath;

        [TestInitialize]
        public void Setup()
        {
            testsRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "tests");
        }

        [TestMethod]
        public void BasicTest()
        {
            TestUseCase("basic");
        }

        [TestMethod]
        public void JxForTest()
        {
            TestUseCase("jx-for");
        }

        [TestMethod]
        public void NestedJxFor()
        {
            TestUseCase("multiple-jx-for");
        }

        private void TestUseCase(string name)
        {
            var testPath = Path.Combine(testsRootPath, name);
            var source = ReadFile(Path.Combine(testPath, "source.json"));
            var transformer = ReadFile(Path.Combine(testPath, "transformer.json"));
            var expected = ReadFile(Path.Combine(testPath, "expected.json"));

            var jx = new JsonTransformer();
            var actual = jx.Transform(source, transformer);

            Assert.IsTrue(JToken.DeepEquals(expected, actual), $"Test Case: {testPath}");
        }

        private JToken ReadFile(string fileName)
        {
            var fileText = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<JToken>(fileText);
        }
    }
}