using System;
using EPiServer.Core;
using Newtonsoft.Json;

namespace Geta.EPi.Imageshop
{
    public abstract class PropertyJsonSerializedObject<T> : PropertyLongString where T : class
    {
        protected T _value;

        public override Type PropertyValueType
        {
            get { return typeof(T); }
        }

        public override object Value
        {
            get
            {
                var value = LongString;

                if (string.IsNullOrWhiteSpace(value)) return null;

                _value = JsonConvert.DeserializeObject<T>(value);

                return _value;
            }
            set
            {
                if (value is T)
                {
                    _value = null;
                    base.Value = JsonConvert.SerializeObject(value);
                    return;
                }

                base.Value = value;
            }
        }

        public override void LoadData(object value)
        {
            base.LoadData(value);
        }

        public override object SaveData(PropertyDataCollection properties)
        {
            return LongString;
        }

        public override IPropertyControl CreatePropertyControl()
        {
            //No support for legacy edit mode
            return null;
        }
    }
}