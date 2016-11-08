define([
        "dojo/_base/declare",
        "dojo/_base/lang",

        "geta-epi-imageshop/widgets/_ImageSelector",

        "epi/epi",
        'dojo/text!./templates/imageSelector.html',

        'xstyle/css!./templates/imageSelector.css'
],
    function (
        declare,
        lang,

        _ImageSelector,

        epi,
        template
    ) {

        return declare("geta-epi-imageshop/widgets/imageSelector", [_ImageSelector], {
            //
            // Public
            //

            baseClass: "imageExt",
            store: null,
            templateString: template,
            value: null,

            //
            // Life cycle
            //

            postCreate: function () {
                this.inherited(arguments);
                this.showHideAddButton(this.value);
            },

            //
            // Public methods
            //

            clearProperty: function (evt) {
                this._setValue(null);
                this.currentImage = null;
                this.onFocus();
            },

            loadThumb: function (value) {
                if (value == null) {
                    this.thumbContainer.style.display = "none";
                    this.thumbImg.src = "";
                } else {
                    this.thumbContainer.style.display = "block";
                    this.thumbImg.src = this._getPreviewImageUrl(value);
                }
            },

            onImageSelected: function() {
                this.inherited(arguments);
                this._setValue(this.currentImage);
            },

            showHideAddButton: function (value) {
                if (value == null) {
                    this.noImageNode.style.display = "block";
                } else {
                    this.noImageNode.style.display = "none";
                }
            },

            //
            // Private methods
            //

            _getPreviewImageUrl: function (image) {
                if (this.previewCropName) {
                    return image.url + "-" + this.previewCropName;
                }

                return image.url;
            },

            _setValueAttr: function (value) {
                this.currentImage = value;
                this._set("value", this.currentImage);
                this.loadThumb(value);
                this.showHideAddButton(value);
            },

            _setValue: function (value) {
                if (this._started && epi.areEqual(this.value, value)) {
                    return;
                }

                this._set("value", value);
                this.loadThumb(value);
                this.showHideAddButton(value);

                if (this._started && this.validate()) {
                    // Trigger change event
                    this.onChange(this.value);
                }
            }
        });
    });