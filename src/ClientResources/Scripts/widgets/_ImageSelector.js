define([
    "dojo/_base/array",
    "dojo/_base/connect",
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/when",
    "dojo/on",

    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",
    "dijit/_OnDijitClickMixin",

    "epi/shell/request/xhr",
    "epi/shell/widget/_ValueRequiredMixin",
    "epi-cms/widget/_HasChildDialogMixin",

    "epi/epi",
    "epi/dependency"
],
    function (
        array,
        connect,
        declare,
        lang,
        when,
        on,

        _WidgetBase,
        _TemplatedMixin,
        _WidgetsInTemplateMixin,
        _OnDijitClickMixin,

        xhr,
        _ValueRequiredMixin,
        _HasChildDialogMixin,

        epi,
        dependency
    ) {

        return declare("geta-epi-imageshop/widgets/_ImageSelector", [_WidgetBase, _TemplatedMixin, _WidgetsInTemplateMixin, _ValueRequiredMixin, _HasChildDialogMixin, _OnDijitClickMixin], {
            baseUrl: null,
            browserClass: "imageshop-browser",
            closeBtnId: "imageshop-frame-container-close",
            containerId: "imageshop-frame-container",
            cropName: "",
            currentImage: null,
            innerClass: "imageshop-frame-wrapper",
            iframeId: "imageshop-frame",
            messageReceivedCallback: null,
            selectCurrentImage: true,
            store: null,

            //
            // Life cycle
            //

            postCreate: function () {
                this.inherited(arguments);
                this.store = dependency.resolve('epi.storeregistry').get('imageshopstore');
            },

            //
            // Public methods
            //

            closeWindow: function (evt) {
                if (window.removeEventListener) {
                    removeEventListener('message', this.messageReceivedCallback, false);
                } else {
                    detachEvent("onmessage", this.messageReceivedCallback);
                }

                this.messageReceivedCallback = null;
                this.destroyFrame();
                this.isShowingChildDialog = false;
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

                if (this.selectCurrentImage && this.currentImage != null && this.currentImage.url) {
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

            onImageSelected: function (image) {
            },

            onMessageReceived: function (event) {
                this.setBasicImage(event.data);

                xhr.get(this.store.target + '?permalink=' + encodeURIComponent(this.currentImage.url), {
                    handleAs: 'json'
                }).then(lang.hitch(this, function (extendedData) {
                    window.console && console.log('extendeddata', extendedData);
                    this.setExtendedImage(extendedData);
                    this.onImageSelected(this.currentImage);
                    this.closeWindow();
                }), lang.hitch(this, function (err) {
                    alert("WebService call to fetch extended metadata failed. Using basic image data (url, credits, width and height). Error is: " + err);
                    this.onImageSelected(this.currentImage);
                    this.closeWindow();
                }));
            },

            openWindow: function (evt) {
                this.isShowingChildDialog = true;

                this.messageReceivedCallback = lang.hitch(this, this.onMessageReceived);

                if (window.addEventListener) {
                    window.addEventListener("message", this.messageReceivedCallback, false);
                } else {
                    window.attachEvent("onmessage", this.messageReceivedCallback);
                }

                this.createFrame();
            },

            setBasicImage: function (data) {
                var imageData = JSON.parse(data.split(";")[0]);
                var textData = imageData.text[this.preferredLanguage];
                window.console && console.log(imageData);

                this.currentImage = {
                    code: imageData.code,
                    url: imageData.image.file,
                    width: imageData.image.width,
                    height: imageData.image.height,
                    changed: new Date(),
                    cropName: this.cropName
                };

                if (textData) {
                    this.currentImage.name = textData.title;
                    this.currentImage.credits = textData.credits;
                    this.currentImage.description = textData.description;
                    this.currentImage.rights = textData.rights;
                    this.currentImage.tags = textData.tags;
                }

                if (imageData.videos) {
                    this.currentImage.aspectRatio = imageData.aspectratio;
                    this.currentImage.videos = imageData.videos;
                }

                window.console && console.log(this.currentImage);
            },

            setExtendedImage: function (data) {
                this.currentImage = this.currentImage || {};

                this.currentImage.documentId = data.documentID;
                this.currentImage.comment = data.comment;
                this.currentImage.isImage = data.isImage;
                this.currentImage.isVideo = data.isVideo;
                this.currentImage.authorName = data.authorName;
                this.currentImage.description = data.description;
            }
        });
    });