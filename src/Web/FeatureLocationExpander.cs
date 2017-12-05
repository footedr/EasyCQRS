using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace EasyCQRS.Web
{
	public class FeatureLocationExpander : IViewLocationExpander
	{
		public void PopulateValues(ViewLocationExpanderContext context)
		{
			// Don't need anything here, but required by the interface
		}

		public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
		{
			// The old locations are /Views/{1}/{0}.cshtml and /Views/Shared/{0}.cshtml where {1} is the controller and {0} is the name of the View
			// Replace /Views with /Features
			return new[] { "/Features/{1}/{0}.cshtml", "/Features/Shared/{0}.cshtml" };
		}
	}
}