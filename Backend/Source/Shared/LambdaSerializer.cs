namespace Shared
{
    using System;
    using System.IO;
    using Amazon.Lambda.Core;
    using Newtonsoft.Json;
    using Shared.Http;

    /// <summary>
    /// Custom serializer for lambda function to throw lambda exception when failing to parse the request
    /// </summary>
    public class LambdaSerializer : ILambdaSerializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaSerializer"/> class.
        /// </summary>
        public LambdaSerializer()
        {
            this.serializer = Newtonsoft.Json.JsonSerializer.Create();
        }

        /// <summary>
        /// Serializes a particular object to a stream.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="response">Object to serialize.</param>
        /// <param name="responseStream">Output stream.</param>
        public void Serialize<T>(T response, Stream responseStream)
        {
            if (!(response is T))
            {
                throw new LambdaException(HttpCode.InternalServerError, string.Empty);
            }

            StreamWriter writer = new StreamWriter(responseStream);
            try
            {
                this.serializer.Serialize(writer, response);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.InternalServerError, ex.Message);
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a stream to a particular type.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize to.</typeparam>
        /// <param name="requestStream">Stream to serialize.</param>
        /// <returns>Deserialized object from stream.</returns>
        public T Deserialize<T>(Stream requestStream)
        {
            StreamReader reader = new StreamReader(requestStream);
            JsonReader jsonReader = new JsonTextReader(reader);

            T obj;
            try
            {
                obj = this.serializer.Deserialize<T>(jsonReader);
            }
            catch (Exception ex)
            {
                throw new LambdaException(HttpCode.BadRequest, ex.Message);
            }

            return obj;
        }
    }
}