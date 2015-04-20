using System;
using EPiServer.Core;
using EPiServer.PlugIn;
using Newtonsoft.Json;

namespace Geta.EPi.ImageShop
{
    [PropertyDefinitionTypePlugIn(DisplayName = "ImageShop Image")]
    public class PropertyImageShopImage : PropertyData
    {
        private ImageShopImage _value;

        public PropertyImageShopImage()
        {
        }

        public PropertyImageShopImage(ImageShopImage image)
        {
            this.SetValue(image);
        }

        protected ImageShopImage DefaultValue
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
                return typeof(ImageShopImage);
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
                this.SetValue((ImageShopImage)value);
            }
        }

        private ImageShopImage DeserializeValue(string stringValue)
        {
            ImageShopImage img;
            try
            {
                img = JsonConvert.DeserializeObject<ImageShopImage>(stringValue);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                throw new Exception(string.Concat("Failed to deserialize property imageValue.", stringValue), exception);
            }
            return img;
        }

        public ImageShopImage GetValue()
        {
            return this._value;
        }

        public override void LoadData(object objValue)
        {
            var image = objValue as ImageShopImage;

            if (image != null)
            {
                this.SetValue(image);
                return;
            }

            this.SetValue(this.DeserializeValue((string)objValue));
        }

        public override PropertyData ParseToObject(string value)
        {
            return new PropertyImageShopImage(this.DeserializeValue(value));
        }

        public override void ParseToSelf(string value)
        {
            this.SetValue(this.DeserializeValue(value));
        }

        public override object SaveData(PropertyDataCollection properties)
        {
            return this.SerializeValue(this.GetValue());
        }

        private string SerializeValue(ImageShopImage imageValue)
        {
            return JsonConvert.SerializeObject(imageValue);
        }

        protected override void SetDefaultValue()
        {
            base.ThrowIfReadOnly();
            this._value = this.DefaultValue;
        }

        public void SetValue(ImageShopImage value)
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