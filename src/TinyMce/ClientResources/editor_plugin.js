(function (tinymce, $) {
    tinymce.create('tinymce.plugins.getaepiimageshop', {

        initialized: false,

        init: function (ed, url) {
            // Register commands
            ed.addCommand('mcegetaepiimageshop', function () {
                var dialogUrl = url + "/InsertImage.aspx?TINYMCE=true";

                if (ed.selection != null && ed.selection.getNode() != null && ed.selection.getNode().src != null)
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
            ed.addButton('getaepiimageshopbutton', {
                title: 'Insert/Upload Imageshop Image',
                image: url + '/images/icon.gif',
                cmd: 'mcegetaepiimageshop'
            });

            function isImageshopPlugin(node) {
                return node && node.nodeName === 'IMG' && (ed.dom.hasClass(node, 'ScrImageshopImage') || !ed.dom.hasClass(node, 'mceItem'));
            };

            // Update media selection status
            ed.onNodeChange.add(function (editor, cm, node) {
                cm.setActive('getaepiimageshopbutton', isImageshopPlugin(node));
            });

            this.initialized = true;
        },
        getInfo: function () {
            return {
                longname: 'Imageshop image plugin',
                author: 'Geta AS, Mattias Olsson',
                authorurl: 'http://geta.se',
                infourl: 'http://geta.se',
                version: tinymce.majorVersion + "." + tinymce.minorVersion
            };
        }
    });

    // Register plugin
    tinymce.PluginManager.add('getaepiimageshop', tinymce.plugins.getaepiimageshop);

})(tinymce, epiJQuery);