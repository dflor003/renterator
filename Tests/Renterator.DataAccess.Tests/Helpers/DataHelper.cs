using System.Linq;

namespace Renterator.DataAccess.Tests.Helpers
{
	public static class DataHelper
	{
		public static string RepeatString(char character, int times)
		{
			return string.Join(string.Empty, Enumerable.Repeat(character, 70));
		}
	}
}
