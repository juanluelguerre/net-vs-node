//
// Ref: https://stackoverflow.com/questions/47815660/does-c-sharp-7-have-array-enumerable-destructuring
// 
using System.Collections.Generic;
using System.Linq;

namespace ElGuerre.DeconstructionConsole
{
	/// <summary>
	/// <code>
	/// var list = new[] { 1, 2, 3, 4 };
	/// var(a, b, rest1) = list;
	/// var(c, d, e, f, rest2) = rest1;
	/// Console.WriteLine($"{a} {b} {c} {d} {e} {f} {rest2.Any()}");
	/// Output: 1 2 3 4 0 0 False
	/// </code>
	/// </summary>
	public static class IEnumerableExtension
	{
		public static void Deconstruct<T>(this IEnumerable<T> seq,
			out T first,
			out IEnumerable<T> rest)
		{
			first = seq.FirstOrDefault();
			rest = seq.Skip(1);
		}

		public static void Deconstruct<T>(this IEnumerable<T> seq,
			out T first,
			out T second,
			out IEnumerable<T> rest)
				=> (first, (second, rest)) = seq;

		public static void Deconstruct<T>(this IEnumerable<T> seq,
			out T first,
			out T second,
			out T third,
			out IEnumerable<T> rest)
				=> (first, second, (third, rest)) = seq;

		
		// ...

	}
}
