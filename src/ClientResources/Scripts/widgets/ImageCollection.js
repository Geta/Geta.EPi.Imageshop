define([
        "dojo/_base/array",
        "dojo/_base/declare",
        "dojo/_base/lang",
        "dojo/dom",
        "dojo/dom-construct",
        "dojo/dom-class",
        "dojo/on",

        "dijit/form/Button",

        "screentek-epi-imageshop/widgets/_ImageSelector",

        "epi/epi",
        "epi/dependency",

        "dojo/text!./templates/ImageCollection.html",
        "xstyle/css!./templates/ImageSelector.css",
        "xstyle/css!./templates/ImageCollection.css",
        "xstyle/css!//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css"
],
    function (
        array,
        declare,
        lang,
        dom,
        domConstruct,
        domClass,
        on,

        Button,

        _ImageSelector,

        epi,
        dependency,

        template
    ) {
        return declare("screentek-epi-imageshop/widgets/ImageCollection", [_ImageSelector], {
            //
            // Private
            //

            _imageCollection: null,

            //
            // Public
            //

            multiple: true,
            previewCropName: null,
            selectCurrentImage: false,
            templateString: template,
            value: null,

            // 
            // Life cycle
            //

            constructor: function() {
                this._imageCollection = [];
                this.inherited(arguments);
            },

            destroy: function() {
                var _a;
                while (_a = this._imageCollection.pop()) {
                }

                this.inherited(arguments);
            },

            postCreate: function() {
                this.inherited(arguments);

                if (this.isVideo) {
                    domClass.add(this.selectImageNode, 'isVideo');
                }
            },

            //
            // Public methods
            //

            onChange: function(value) {
            },

            onImageSelected: function (image) {
                this.inherited(arguments);

                this._addImageToCollection(image);
                this._onWidgetChanged();
            },

            updateValue: function () {
                var propertyValue = [];

                array.forEach(this._imageCollection, function(entry) {
                    propertyValue.push(entry.image);
                });

                if (this._started && epi.areEqual(this.value, propertyValue)) {
                    return;
                }

                this._updateSortIcons();
                this._set("value", propertyValue);
                this.onBlur();
            },

            //
            // Private methods
            //

            _addHtmlNodesForImages: function(imageCollection) {
                for (var i = 0; i < imageCollection.length; i++) {
                    var image = imageCollection[i];
                    this._addImageToCollection(image);
                }  
            },

            _addHtmlNodesForImage: function(image) {
                var li = domConstruct.create("li", { "class": "screentek-image-list-item clearfix" }, this.imageCollectionNode);
                var imageWrapperNode = domConstruct.create("div", { "class": "screentek-image-wrapper" }, li);

                domConstruct.create("img", { "class": "screentek-image", "src": this._getPreviewImageUrl(image) }, imageWrapperNode);
                domConstruct.create("a", { "href": image.url, "class": "screentek-image-url", "innerHTML": this._getPreviewImageUrl(image) }, li);

                var actionsWrapperNode = domConstruct.create("div", { "class": "screentek-actions-wrapper" }, li);

                // Sort up button
                var sortUpLink = domConstruct.create("a", { href: "javascript:void(0)" }, actionsWrapperNode);
                var sortUpIcon = domConstruct.create("i", null, sortUpLink);
                sortUpIcon.setAttribute("class", "fa fa-fw fa-arrow-up");

                // Sort down button
                var sortDownLink = domConstruct.create("a", { href: "javascript:void(0)" }, actionsWrapperNode);
                var sortDownIcon = domConstruct.create("i", null, sortDownLink);
                sortDownIcon.setAttribute("class", "fa fa-fw fa-arrow-down");

                on(sortUpLink, "click", lang.hitch(this, function (e) {
                    if (domClass.contains(sortUpLink, "disabled")) {
                        return false;
                    }

                    this._sortNodeUp(li);
                    return false;
                }));

                on(sortDownLink, "click", lang.hitch(this, function (e) {
                    if (domClass.contains(sortDownLink, "disabled")) {
                        return false;
                    }

                    this._sortNodeDown(li);
                    return false;
                }));

                var removeButton = new Button({
                    label: "X",
                    scope: this,
                    container: li
                });

                removeButton.on("click", lang.hitch(this, function() {
                    this._removeImageNode(li);
                }));

                removeButton.placeAt(actionsWrapperNode);

                li.sortUpLink = sortUpLink;
                li.sortDownLink = sortDownLink;

                return li;
            },

            _addImageToCollection: function (image) {
                if (image == null) {
                    return null;
                }

                var node = this._addHtmlNodesForImage(image);
                var o = {
                    image: image,
                    node: node
                };

                this._imageCollection.push(o);
                return o;
            },

            _getNodeIndex: function (node) {
                for (var i = 0; i < this._imageCollection.length; i++) {
                    var image = this._imageCollection[i];

                    if (image.node == node) {
                        return i;
                    }
                }

                return -1;
            },

            _getPreviewImageUrl: function (image) {
                if (this.previewCropName) {
                    return image.url + "-" + this.previewCropName;
                }

                return image.url;
            },

            _moveNode: function (index, step) {
                var newIndex = index + step;
                this._imageCollection.splice(newIndex, 0, this._imageCollection.splice(index, 1)[0]);
            },

            _onWidgetChanged: function () {
                this.inherited(arguments);
                this.updateValue();
            },

            _removeImageNode: function(node) {
                for (var i = this._imageCollection.length - 1; i >= 0; i--) {
                    var image = this._imageCollection[i];

                    if (image.node == node) {
                        this._imageCollection.splice(i, 1);
                        domConstruct.destroy(image.node);
                        this._onWidgetChanged();
                        break;
                    }
                }
            },

            _setValueAttr: function (val) {
                this._set("value", val);

                if (!val || !(val instanceof Array)) {
                    this._set("value", []);
                }

                if (this._imageCollection.length == 0) {
                    array.forEach(this.value, this._addImageToCollection, this);
                    this._updateSortIcons();
                }
            },

            _sortNodeUp: function(node) {
                var prevNode = node.previousElementSibling;

                if (prevNode != null) {
                    this._moveNode(this._getNodeIndex(node), -1);
                    var parent = node.parentElement;
                    parent.insertBefore(node, prevNode);
                    this._onWidgetChanged();
                }
            },

            _sortNodeDown: function (node) {
                var nextNode = node.nextElementSibling;

                if (nextNode != null) {
                    this._moveNode(this._getNodeIndex(node), 1);
                    var parent = node.parentElement;

                    if (nextNode == parent.lastChild) {
                        parent.appendChild(node);
                    } else {
                        parent.insertBefore(node, nextNode.nextElementSibling);
                    }

                    this._onWidgetChanged();
                }
            },

            _updateSortIcons: function () {
                if (this._imageCollection.length < 1) {
                    return;
                }

                var firstIndex = 0;
                var lastIndex = this._imageCollection.length - 1;
                var firstItem = this._imageCollection[firstIndex];

                array.forEach(this._imageCollection, function (image) {
                    domClass.remove(image.node.sortUpLink, "disabled");
                    domClass.remove(image.node.sortDownLink, "disabled");
                });

                domClass.add(firstItem.node.sortUpLink, "disabled");

                if (lastIndex > firstIndex) {
                    var lastItem = this._imageCollection[lastIndex];
                    domClass.add(lastItem.node.sortDownLink, "disabled");
                } else {
                    domClass.add(firstItem.node.sortDownLink, "disabled");
                }
            }
        });
    });