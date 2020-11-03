using System;
using System.Collections.Generic;

namespace RestApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var exampleUri = "http://host/api/endpoint";
            var exampleToken = "example-jwt-token";

            // Get example
            var exampleQuery = new Dictionary<string, string>
            {
                { "key1", "value1" }, { "key2", "value2" }, { "key3", "value3" },
            };
            var get = RestApiClient.Build(exampleUri)
                                   .WithToken(exampleUri)
                                   .GetAsync<object>(exampleQuery);

            // Post example
            var post = RestApiClient.Build(exampleUri)
                                    .WithToken(exampleToken)
                                    .PostAsync<object, object>("aqui_vai_o_body");

            // Put example
            var put = RestApiClient.Build(exampleUri)
                        .WithToken(exampleToken)
                        .PutAsync<object, object>("aqui_vai_o_body");

            // Delete example
            var delete = RestApiClient.Build(exampleUri)
                                      .WithToken(exampleToken)
                                      .DeleteAsync();
        }
    }
}
