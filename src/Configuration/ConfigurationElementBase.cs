using System.Configuration;

namespace Screentek.EPi.Imageshop.Configuration
{
	public abstract class ConfigurationElementBase : ConfigurationElement
	{
		public abstract string GetElementKey();
	}
}