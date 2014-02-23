using System.Web.Optimization;

namespace Renterator.Web.Helpers
{
	public class LessTransform : IBundleTransform
	{
		public void Process(BundleContext context, BundleResponse response)
		{
			response.Content = dotless.Core.Less.Parse(response.Content);
			response.ContentType = "text/css";
		}
	}
}