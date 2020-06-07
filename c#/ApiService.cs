using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Aqua.VIC.Mobile.App.DataService
{
    public class ApiService
    {
        private const string vicBaseUrl = "https://vic-webapi.azurewebsites.net/";

        public ApiService()
        {
        }

        public async static Task<string> GetFromVicWebApi(string url)
        {
            try
            {
                var client = new HttpClient() // todo HttpClient should be reused!
                {
                    Timeout = TimeSpan.FromMilliseconds(50000)
                };

                if (App.Current.Properties.ContainsKey("AuthToken"))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", App.Current.Properties["AuthToken"].ToString());
                }

                if (App.Current.Properties.ContainsKey("SiteId"))
                {
                    client.DefaultRequestHeaders.Add("Current-Site-Id", App.Current.Properties["SiteId"].ToString());
                }
                
                var res = await client.GetAsync(vicBaseUrl + url);

                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsStringAsync();
                }
                else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // need to login again
                    removeCredentials();
                    return null;
                }
                else
                {
                    // problems handling here
                    return null;
                }
            }
            catch (System.Exception)
            {
                // handle timeout
                return null;
            }
        }

        public async static Task<string> PostToVicWebApi(string url, string body)
        {
            try
            {
                var client = new HttpClient()
                {
                    Timeout = TimeSpan.FromMilliseconds(5000),
                };

                if (App.Current.Properties.ContainsKey("AuthToken"))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(
                            "Bearer",
                            App.Current.Properties["AuthToken"].ToString());
                }

                if (App.Current.Properties.ContainsKey("SiteId"))
                {
                    client.DefaultRequestHeaders.Add("Current-Site-Id", App.Current.Properties["SiteId"].ToString());
                }

                var data = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(vicBaseUrl + url, data);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // need to login again
                    removeCredentials();
                    return null;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // credentials incorrect
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // problems handling here
                    return null;
                }
            }
            catch (System.Exception)
            {
                // handle timeout
                return null;
            }
        }

        public static LoginResponse ParseLoginResponse(string json)
        {
            if (json != null)
            {
                LoginResponse data = JsonConvert.DeserializeObject<LoginResponse>(json);
                return data;
            }

            return null;
        }



        /// <summary>
        /// Creates json from data
        /// </summary>
        /// <typeparam name="T">Type of model.</typeparam>
        /// <param name="obj">Structured data to serialize</param>
        /// <returns>Returns the serialied json</returns>
        public static string CreateJsonString<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Populates data from json string.
        /// </summary>
        /// <typeparam name="T">Type of model.</typeparam>
        /// <param name="json">Json file to fetch data.</param>
        /// <returns>Returns the model object.</returns>
        public static T PopulateDataFromString<T>(string json)
        {
            T obj;

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }


        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        public static T PopulateDataFromFile<T>(string fileName)
        {
            var file = "Aqua.VIC.Mobile.App.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T obj;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }

        public static async Task<bool> Login(string email, string password)
        {
            string req = string.Format("{{\"Email\":\"{0}\",\"Password\":\"{1}\"}}", email, password);
            var res = await PostToVicWebApi("user/login", req);
            var loginResponse = ParseLoginResponse(res);
            bool ret = false;

            if (loginResponse != null)
            {
                if (loginResponse.Token != null)
                {
                    App.Current.Properties["CurrentUserId"] = loginResponse.Id;
                    App.Current.Properties["AuthToken"] = loginResponse.Token;
                    App.Current.Properties["LastName"] = loginResponse.LastName;
                    App.Current.Properties["FirstName"] = loginResponse.FirstName;
                    ret = await GetSites();
                    await App.Current.SavePropertiesAsync();
                    if (!ret)
                    {
                        removeCredentials();
                        App.Current.Properties["LoginMessage"] = "Sorry but we could not find your city in our records.";
                    }
                }
                else
                {
                    // login failed, get message
                    App.Current.Properties["LoginMessage"] = loginResponse.Message;
                }
            }

            return ret;
        }

        public static async Task<bool> GetSites()
        {
            string json = await GetFromVicWebApi("site");
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }
            else
            {
                var sites = JsonConvert.DeserializeObject<List<SiteResponse>>(json);
                foreach (var site in sites)
                {
                    // currently only keep the last site...
                    // todo allow user to switch between multiple sites
                    App.Current.Properties["SiteId"] = site.SiteId;
                    App.Current.Properties["SiteName"] = site.SiteName;
                    App.Current.Properties["SiteHoursDifferenceFromUtc"] = site.HoursDifferenceFromUtc;
                }
                
                return true;
            }
        }

        private static void removeCredentials()
        {
            App.Current.Properties.Remove("AuthToken");
            App.Current.Properties.Remove("LastName");
            App.Current.Properties.Remove("FirstName");
            App.Current.Properties.Remove("SiteId");
            App.Current.Properties.Remove("SiteName");
            App.Current.Properties.Remove("SiteHoursDifferenceFromUtc");
        }
    }

    public class LoginResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("expiryDate")]
        public DateTime? ExpiryDate { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("resources")]
        public IList<string> Resources { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class SiteResponse
    {
        [JsonProperty("id")]
        public int SiteId { get; set; }
        [JsonProperty("name")]
        public string SiteName { get; set; }
        [JsonProperty("hoursDifferenceFromUtc")]
        public int HoursDifferenceFromUtc { get; set; }
        [JsonProperty("hoursDifferenceFromUtca")]
        public int HoursDifferenceFromUtca { get; set; }
    }
}
