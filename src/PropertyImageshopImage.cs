using System;
using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;

namespace Geta.EPi.Imageshop
{
    [PropertyDefinitionTypePlugIn(DisplayName = "Imageshop Image")]
    public class PropertyImageshopImage : PropertyData
    {
        private ImageshopImage _value;

        public PropertyImageshopImage()
        {
        }

        public PropertyImageshopImage(ImageshopImage image)
        {
            this.SetValue(image);
        }

        protected ImageshopImage DefaultValue
        {
            get
            {
                return null;
            }
        }

        public override Type PropertyValueType
        {
            get
            {
                return typeof(ImageshopImage);
            }
        }

        public override PropertyDataType Type
        {
            get
            {
                return PropertyDataType.LongString;
            }
        }

        public override object Value
        {
            get
            {
                return this.GetValue();
            }
            set
            {
                this.SetValue((ImageshopImage)value);
            }
        }

        private ImageshopImage DeserializeValue(string stringValue)
        {
            ImageshopImage img;
            try
            {
                img = JsonConvert.DeserializeObject<ImageshopImage>(stringValue);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                throw new Exception(string.Concat("Failed to deserialize property imageValue.", stringValue), exception);
            }
            return img;
        }

        public ImageshopImage GetValue()
        {
            return this._value;
        }

        public override void LoadData(object objValue)
        {
            var image = objValue as ImageshopImage;

            if (image != null)
            {
                this.SetValue(image);
                return;
            }

            this.SetValue(this.DeserializeValue((string)objValue));
        }

        public override PropertyData ParseToObject(string value)
        {
            return new PropertyImageshopImage(this.DeserializeValue(value));
        }

        public override void ParseToSelf(string value)
        {
            this.SetValue(this.DeserializeValue(value));
        }

        public override object SaveData(PropertyDataCollection properties)
        {
            return this.SerializeValue(this.GetValue());
        }

        private string SerializeValue(ImageshopImage imageValue)
        {
            return JsonConvert.SerializeObject(imageValue);
        }

        protected override void SetDefaultValue()
        {
            base.ThrowIfReadOnly();
            this._value = this.DefaultValue;
        }

        public void SetValue(ImageshopImage value)
        {
            this.SetPropertyValue(value, () =>
            {
                if (Equals(value, this.DefaultValue))
                {
                    this.Clear();
                    return;
                }

                this._value = value;
                this.Modified();
            });
        }
    }
}
