namespace Shared.Test
{
    using System;
    using System.IO;
    using Amazon.Lambda.TestUtilities;
    using Newtonsoft.Json;
    using Shared;
    using Xunit;

    public class LambdaSerializerTest
    {
        private class TestRequest
        {
            public string Test { get; set; }
        }

        [Fact]
        public void ValidObjectShouldSerialize()
        {
            var serializer = new LambdaSerializer();
            var stream = new MemoryStream();
            var obj = new TestRequest()
            {
                Test = "Test"
            };

            serializer.Serialize(obj, stream);
            stream.Position = 0;
            Assert.Equal("{\"Test\":\"Test\"}", new StreamReader(stream).ReadToEnd());
        }

        [Fact]
        public void InvalidObjectShouldNotSerialize()
        {
            var serializer = new LambdaSerializer();
            var stream = new MemoryStream();

            Assert.Throws(typeof(LambdaException), () => serializer.Serialize<TestRequest>(null, stream));
        }

        [Fact]
        public void ValidJsonShouldDeserialize()
        {
            var serializer = new LambdaSerializer();
            var stream = new MemoryStream();
            var json = "{\"Test\":\"Test\"}";
            var sw = new StreamWriter(stream);
            sw.Write(json);
            sw.Flush();
            stream.Position = 0;

            var d = serializer.Deserialize<TestRequest>(stream);
            Assert.Equal("Test", d.Test);
        }

        [Fact]
        public void InvalidJsonShouldNotDeserialize()
        {
            var serializer = new LambdaSerializer();
            var stream = new MemoryStream();
            var json = "123";
            var sw = new StreamWriter(stream);
            sw.Write(json);
            sw.Flush();
            stream.Position = 0;

            Assert.Throws(typeof(LambdaException), () => serializer.Deserialize<TestRequest>(stream));
        }
    }
}