define([
        "dojo/_base/array",
        "dojo/_base/connect",
        "dojo/_base/declare",
        "dojo/_base/lang",
        "dojo/on",
        "dijit/focus",

        "dijit/_CssStateMixin",
        "dijit/_Widget",
        "dijit/_TemplatedMixin",
        "dijit/_WidgetsInTemplateMixin",
        "epi-cms/widget/_HasChildDialogMixin",

        "epi/epi",
        "epi/shell/widget/_ValueRequiredMixin",
        'dojo/text!./imageSelector.html',

        'xstyle/css!./imageSelector.css'
],
    function (
        array,
        connect,
        declare,
        lang,
        on,
        focusManager,

        _CssStateMixin,
        _Widget,
        _TemplatedMixin,
        _WidgetsInTemplateMixin,
        _HasChildDialogMixin,

        epi,
        _ValueRequiredMixin,
        template
    ) {

        return declare("geta-epi-imageshop.widgets.imageSelector", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _CssStateMixin, _ValueRequiredMixin, _HasChildDialogMixin], {
            newWindow: null,

            templateString: template,

            value: null,
            currentImage: null,

            baseClass: "imageExt",
            containerId: "imageshop-frame-container",
            iframeId: "imageshop-frame",
            closeBtnId: "imageshop-frame-container-close",
            innerClass: "imageshop-frame-wrapper",
            browserClass: "imageshop-browser",

            messageReceivedCallback: null,

            postMixInProperties: function () {
                this.inherited(arguments);
            },

            postCreate: function () {
                this.inherited(arguments);
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
            },

            createFrame: function () {
                var root = document.body;
                var div = document.createElement("div");
                div.setAttribute("id", this.containerId);
                div.setAttribute("class", this.containerId);

                var innerContainer = document.createElement("div");
                innerContainer.setAttribute("class", this.innerClass + " " + this.browserClass);

                var ifrm = document.createElement("iframe");
                var ifrmUrl = this.baseUrl;

                if (this.currentImage != null && this.currentImage.url) {
                    ifrmUrl += "&IMAGE=" + encodeURI(this.currentImage.url);
                }

                ifrm.setAttribute("src", ifrmUrl);
                ifrm.setAttribute("id", this.iframeId);
                ifrm.setAttribute("frameborder", "0");

                var closeBtn = document.createElement("a");
                closeBtn.setAttribute("class", this.closeBtnId);
                closeBtn.setAttribute("id", this.closeBtnId);
                closeBtn.setAttribute("title", "Close window");

                closeBtn.onclick = lang.hitch(this, function () {
                    this.closeWindow();
                });

                innerContainer.appendChild(ifrm);
                innerContainer.appendChild(closeBtn);

                div.appendChild(innerContainer);
                root.appendChild(div);
            },

            destroyFrame: function () {
                var container = document.getElementById(this.containerId);
                container.parentNode.removeChild(container);
            },

            onMessageReceived: function (event) {
                this.setImage(event.data);
                this._setValue(this.currentImage);
                this.closeWindow();
            },

            setImage: function (data) {
                var imageData = data.split(";");

                this.currentImage = {
                    url: imageData[0],
                    title: imageData[1],
                    width: imageData[2],
                    height: imageData[3]
                };
            },

            showHideAddButton: function (value) {
                if (value == null) {
                    this.noImageNode.style.display = "block";
                } else {
                    this.noImageNode.style.display = "none";
                }
            },

            openWindow: function (evt) {
                this.isShowingChildDialog = true;

                this.messageReceivedCallback = lang.hitch(this, this.onMessageReceived);

                if (window.addEventListener) {
                    addEventListener("message", this.messageReceivedCallback, false);
                } else {
                    attachEvent("onmessage", this.messageReceivedCallback);
                }

                this.createFrame();
            },

            loadThumb: function (value) {
                if (value == null) {
                    this.thumbContainer.style.display = "none";
                    this.thumbImg.src = "";
                } else {
                    this.thumbContainer.style.display = "block";
                    this.thumbImg.src = value.url;
                }
            },

            clearProperty: function (evt) {
                this._setValue(null);
                this.currentImage = null;
                this.onFocus();
            },

            closeWindow: function (evt) {
                if (window.removeEventListener) {
                    removeEventListener('message', this.messageReceivedCallback, false);
                } else {
                    detachEvent("onmessage", this.messageReceivedCallback);
                }

                this.messageReceivedCallback = null;
                this.destroyFrame();
                this.isShowingChildDialog = false;
                this.onBlur();
            }
        });
    });