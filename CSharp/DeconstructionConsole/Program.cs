using System;

namespace ElGuerre.DeconstructionConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("*** DECONSTRUCTION ***");

			//
			// Sample 1
			//
			var u = new User
			{
				Id = 1,
				Avatar = "http://...",
				FirstName = "Juan Luis",
				LastName = "Guerrero",
				Email = "my.email@tests.com"
			};

			var u2 = (u.Id, u.Avatar, u.FirstName, u.LastName, u.Email);

			//
			// Sample 2
			//
			var u_as_tuple = (
				Id: 1,
				Avatar: "http://...",
				FirstName: "Juan Luis",
				LastName: "Guerrero",
				Email: "my.email@tests.com"
			);

			var (_, _, FirstName, LastName, _) = u_as_tuple;


			//
			// Sample 3
			//
			// --- Array Deconstruction ----			
			var foo = ("one", "two", "three");
			var (red, yellow, green) = foo;
			Console.WriteLine($"{red}, {yellow}, {green}");

			//
			// --- Array deconstruction improved !
			var fooArr = new[] { "one", "two", "three" };
			var (red2, yellow2, green2) = fooArr;
			Console.WriteLine($"{red}, {yellow}, {green}");

			//
			// Sample 4
			//
			// --- Object Deconstruction ---
			var fooObj = new Foo
			{
				Id = 42,
				Is_Verified = true
			};
			var (Id, _) = fooObj;
			Console.WriteLine($"{Id}");


			Console.WriteLine("Press ENTER to finish...");
			Console.ReadLine();
		}
	}
}
