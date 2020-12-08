using System;
using Newtonsoft.Json;

namespace ElGuerre.RxConsole
{
    [JsonObject("data")]
    public class User
    {
        [JsonProperty("id")]        
        public int Id { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }

        public static User Create(User source)
        {
            return new User()
            {
                Id = source.Id,
                Email = source.Email,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Avatar = source.Avatar
            };
        }

        public void Deconstruct(
            out int id,
            out string avatar)
        {
            id = Id;
            avatar = Avatar;
        }

        public void Deconstruct(
            out int id,
            out string avatar,
            out string firstName)
        {
            id = Id;
            avatar = Avatar;
            firstName = FirstName;
        }

        public void Deconstruct(
            out int id,
            out string avatar,
            out string firstName,
            out string lastName)
        {
            id = Id;
            avatar = Avatar;
            firstName = FirstName;
            lastName = LastName;
        }

        public void Deconstruct(
            out int id,
            out string avatar,
            out string firstName,
            out string lastName,
            out string email)
        {
            id = Id;
            avatar = Avatar;
            firstName = FirstName;
            lastName = LastName;
            email = Email;
        }
    }

    public class PagedUsers
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }
        
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("data")]
        public User[] Users { get; set; }
    }
}