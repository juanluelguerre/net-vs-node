namespace ElGuerre.DeconstructionConsole
{
	public class User
    { 
        public int Id { get; set; } 
        public string Avatar { get; set; }     
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
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
}
