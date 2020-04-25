using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace iPractice.RestServices
{
    public class RestClient : IDisposable //where T: class 
    {
        private readonly string _baseAddress;

        #region ReferenceArticles
        //to see shared secret or HMAC code http://geekswithblogs.net/Frez/archive/2015/01/05/generic-wrapper-for-calling-asp.net-web-api-rest-service-using.aspx
        //http://codereview.stackexchange.com/questions/55647/async-wrapper-around-public-api
        //http://www.matlus.com/a-generic-restful-crud-httpclient/
        //https://github.com/faniereynders/WebAPIProxy
        //wrapper for multi-part post https://github.com/benpowell/HelloTxt.NET/blob/master/HelloTxt/HelloTxtAPI_v2.cs
        //Re-try message fir failure http://stackoverflow.com/questions/15716399/extend-httpclient
        //getting client ip using OWIN layer http://www.strathweb.com/2013/07/owin-middleware-asp-net-web-api-and-clients-ip-address/
        //very nice blog about WebAPI => http://blog.developers.ba/simple-way-implement-caching-asp-net-web-api/
        //WebAPi cmpression - http compression - https://github.com/azzlack/Microsoft.AspNet.WebApi.MessageHandlers.Compression
        //webapi compression - https://damienbod.wordpress.com/2014/07/16/web-api-using-gzip-compression/
        //http://benfoster.io/blog/aspnet-web-api-compression
        //facebook api https://www.facebook.com/AspnetWebApi?fref=nf
        //Test Client - http://www.devcurry.com/2013/07/aspnet-web-api-20-and-new-odata-keywords.html
        //http://tech.pro/tutorial/1408/building-remote-control-service-for-spotify-with-aspnet-web-api-and-signalr
        //building remote controller using webapi http://tech.pro/tutorial/1408/building-remote-control-service-for-spotify-with-aspnet-web-api-and-signalr
        #endregion

        private static string jsonMediaType = "application/json";
        public RestClient(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        private HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip });
            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));//MediaTypeWithQualityHeaderValue.Parse(jsonMediaType)
            // httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            // httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("defalte"));

            return httpClient;
            //Custom Header - if needed
            //httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Matlus_HttpClient", "1.0")));

        }

        private MediaTypeFormatter GetMediaTypeFormatter()
        {
            //var jsonSerializerSettings = new JsonSerializerSettings();
            //jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter());
            //var jsonFormatter = new JsonNetFormatter(jsonSerializerSettings);
            //return new MediaTypeFormatter[] { JsonMediaTypeFormatter };
            return new JsonMediaTypeFormatter();
        }

        public async Task<T> GetAsync<T>(string apiUrl)
        {
            T result = default(T);

            try
            {
                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    //if (response.StatusCode != HttpStatusCode.OK)  throw new CommunicationException();
                    
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted) throw x.Exception;
                        //if (result.GetType() == typeof(string))
                        //{
                        //    result =x.Result;//JsonConvert.DeserializeObject<T>(x.Result);
                        //}
                        //else { 
                            result = JsonConvert.DeserializeObject<T>(x.Result);
                        //}
                    });
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }           

            return result;
        }

        public async Task<string> GetStringAsync(string apiUrl)
        {
            string result = default(string);

            try
            {
                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    //if (response.StatusCode != HttpStatusCode.OK)  throw new CommunicationException();

                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted) throw x.Exception;
                        result = x.Result.ToString();//JsonConvert.DeserializeObject<string>(x.Result);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<List<T>> GetManyAsync<T>(string apiUrl)
        {
            List<T> result = new List<T>();
            try
            {
                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                    {
                        if (x.IsFaulted) throw x.Exception;
                        result = JsonConvert.DeserializeObject<List<T>>(x.Result);
                    });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
            //return await responseMessage.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task<T> PostRequestAsync<T>(string apiUrl, T postObject)
        {
            T result = default(T);

            using (var client = GetHttpClient())
            {
                HttpContent content = new ObjectContent<T>(postObject, GetMediaTypeFormatter());
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted) throw x.Exception;
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }

        public async Task<TOut> PostRequestGenericAsync<T, TOut>(string apiUrl, T postObject)
        {
            TOut result = default(TOut);

            using (var client = GetHttpClient())
            {
                HttpContent content = new ObjectContent<T>(postObject, GetMediaTypeFormatter());
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted) throw x.Exception;
                    result = JsonConvert.DeserializeObject<TOut>(x.Result);
                });
            }

            return result;
        }

        public async Task<string> PostRequestStringAsync<T>(string apiUrl, T postObject)
        {
            string result = default(string);

            using (var client = GetHttpClient())
            {
                HttpContent content = new ObjectContent<T>(postObject, GetMediaTypeFormatter());
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted) throw x.Exception;
                    result = x.Result.ToString();
                });
            }

            return result;
        }

        public async Task PutRequestAsync<T>(string apiUrl, T putObject)
        {
            using (var client = GetHttpClient())
            {
                var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<bool> DeleteRequestAsync(string apiUrl)
        {
            using (var client = GetHttpClient())
            {
                bool result = true;
                var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted) throw x.Exception;
                    result = JsonConvert.DeserializeObject<bool>(x.Result);
                });
                return result;
            }
        }

        #region commneted code
        /// <summary>
        /// For getting multiple (or all) items from a web api using GET
        /// </summary>
        /// <param name="apiUrl">Added to the base address to make the full url of the 
        /// api get method, e.g. "products?page=1" to get page 1 of the products</param>
        /// <returns>The items requested</returns>
        //public async Task<T[]> GetMany<T>(string apiUrl)
        //{
        //    T[] result = default(T);

        //    using (var client = GetHttpClient())
        //    {
        //        var response = await client.GetAsync(apiUrl).ConfigureAwait(false);
        //        response.EnsureSuccessStatusCode();
        //        await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
        //        {
        //            if (x.IsFaulted)  throw x.Exception;
        //            result = JsonConvert.DeserializeObject<T[]>(x.Result);
        //        });
        //    }

        //    return result;
        //}

        #endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}


