namespace ElGuerre.DeconstructionConsole
{
	public class Foo
	{
		public int Id { get; set; }
		public bool Is_Verified { get; set; }

		public void Deconstruct(out int id)
		{
			id = Id;
		}

		public void Deconstruct(out int id, out bool is_verified)
		{
			id = Id;
			is_verified = Is_Verified;
		}
	}
}
