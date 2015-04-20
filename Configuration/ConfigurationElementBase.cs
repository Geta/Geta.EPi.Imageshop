using System.Configuration;

namespace Geta.EPi.Imageshop.Configuration
{
	public abstract class ConfigurationElementBase : ConfigurationElement
	{
		public abstract string GetElementKey();
	}
}