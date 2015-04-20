using System.Configuration;

namespace Geta.EPi.ImageShop.Configuration
{
	public abstract class ConfigurationElementBase : ConfigurationElement
	{
		public abstract string GetElementKey();
	}
}