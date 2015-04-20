using System.Collections.Generic;
using System.Configuration;

namespace Geta.EPi.ImageShop.Configuration
{
	public class ConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T> where T: ConfigurationElementBase, new()
	{
        protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((T)element).GetElementKey();
		}

		public T this[int index]
		{
			get
			{
				return (T)BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
					BaseRemoveAt(index);
				BaseAdd(index, value);
			}
		}

		public new T this[string name]
		{
			get
			{
				return (T)BaseGet(name);
			}
		}

		public void Add(T item)
		{
			BaseAdd(item);
		}

		public int IndexOf(T item)
		{
			return BaseIndexOf(item);
		}

		protected override void BaseAdd(ConfigurationElement element)
		{
			BaseAdd(element, false);
		}

		public void Remove(T item)
		{
			if (BaseIndexOf(item) > -1)
				BaseRemove(item.GetElementKey());
		}

		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}

		public void Remove(string id)
		{
			BaseRemove(id);
		}

		public void Clear()
		{
			BaseClear();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			foreach (T item in this)
			{
				yield return item;
			}
		}
	}
}