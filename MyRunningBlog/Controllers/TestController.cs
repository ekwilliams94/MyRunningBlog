using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRunningBlog.Controllers
{
    public class TestController
    {
        [HttpGet("[action]")]
        public IEnumerable<TestData> GetTestData()
        {
            return new List<TestData>
            {
                new TestData{Number=1, Sentence = "Test Sentence 1"},
                new TestData{Number=2, Sentence = "Test Sentence 2"},
                new TestData{Number=3, Sentence = "Test Sentence 3"},
                new TestData{Number=4, Sentence = "Test Sentence 4"},
                new TestData{Number=5, Sentence = "Test Sentence 5"},
            };
        }
    }

    public class TestData
    {
        public int Number { get; set; }
        public string Sentence { get; set; }
    }
}
