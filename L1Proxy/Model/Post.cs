using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace L1Proxy.Model
{
    /// <summary>
    /// Класс для разбора ответа по посту
    /// </summary>
    class Post
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }

        public bool Valid()
        {
            bool result = UserId > 0 && Id > 0 && Title.Length > 0;

            return result;
        }
    }
}
