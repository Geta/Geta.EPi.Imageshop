"use strict";
var tinymce = tinymce || {};
tinymce.PluginManager.add("getaepiimageshop", function (ed, url) {
    var imageNode;
    // Register commands
    ed.addCommand('mcegetaepiimageshop', function () {
        var dialogUrl = url + "/InsertImage.aspx?TINYMCE=true";

        if (ed.selection !== null && ed.selection.getNode() !== null && ed.selection.getNode().src !== null)
            dialogUrl += "&IMAGE=" + encodeURIComponent(ed.selection.getNode().src);

        ed.windowManager.open({
            file: dialogUrl,
            width: 950,
            height: 650,
            scrollbars: 1,
            inline: 1
        }, {
                plugin_url: url
            });
    });
    // Register buttons
    ed.addButton("getaepiimageshop", {
        title: "Insert/Upload Imageshop Image",
        cmd: "mcegetaepiimageshop",
        image: url + "/images/icon.gif",
        onPostRender: function () {
            // Add a node change handler, selects the button in the UI when a image is selected
            ed.on("NodeChange", function (e) {
                //Prevent tool from being activated as objects represented as images.
                var isWebShopImage = e.element.tagName === "IMG" && (ed.dom.getAttrib(e.element, "class").indexOf("ScrImageshopImage") > -1 || ed.dom.getAttrib(e.element, "class").indexOf("mceItem") === -1);
                this.active(isWebShopImage);

                //Set or reset imageNode to the node cause Image Editor command enabled
                imageNode = isWebShopImage ? e.element : null;
            }.bind(this));
        }
    });
    return {
        getMetadata: function () {
            return {
                name: "Imageshop image plugin",
                url: "http://geta.se",
                author: 'Geta AS, Mattias Olsson, tinymce 2.x compatible by Inmeta AS, Fredrik Skarderud',
                authorurl: 'http://geta.se',
                infourl: 'http://geta.se',
                version: tinymce.majorVersion + "." + tinymce.minorVersion
            };
        }
    };
});

