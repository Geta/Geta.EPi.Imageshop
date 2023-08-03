﻿<%@ Page Language="C#" AutoEventWireup="false" %>

<%@ Import Namespace="Screentek.EPi.Imageshop.Configuration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=10">

    <title>Imageshop</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="https://client.imageshop.no/scripts/tiny_mce_popup.js"></script>

    <style type="text/css">
        iframe {
            width: 100%;
            overflow-x: hidden;
            overflow-y: scroll;
            height: 3500px;
            border: 0px none;
            margin: 0px;
            padding: 0px;
        }

            iframe.safari {
                overflow: hidden;
            }

        body {
            margin: 0px;
            padding: 0px;
            overflow: hidden;
        }

            body.safari {
                overflow-x: hidden;
                overflow-y: scroll;
            }
    </style>
    <script type="text/javascript">

        function setSize() {
            if (navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
                var columns = parseInt(($("iframe").width() - 50) / 152);
                var rows = Math.ceil(50 / columns);
                var height = $(window).width() + rows * 298 + 100;

                $("iframe").height(height);
                $("iframe").addClass("safari");
                $("body").addClass("safari");
            } else {
                $("iframe").height($(window).height());
                $("iframe").width($(window).width());
            }
        }

        function listener(event) {
            var eventdata = event.data.split(";");
            insert(eventdata[0], eventdata[1], eventdata[2], eventdata[3], eventdata[4], eventdata[5]);
        }

        $(function () {
            var token = "<%= ImageshopSettings.Instance.Token %>";
            var showSizeDialog = "<%= ImageshopSettings.Instance.ShowSizeDialog.ToString().ToLowerInvariant() %>";
            var showCropDialog = "<%= ImageshopSettings.Instance.ShowCropDialog.ToString().ToLowerInvariant() %>";
            var freeCrop = "<%= ImageshopSettings.Instance.FreeCrop.ToString().ToLowerInvariant() %>";
            var interfaceName = "<%= HttpUtility.UrlEncode(ImageshopSettings.Instance.InterfaceName) %>";
            var documentPrefix = "<%= HttpUtility.UrlEncode(ImageshopSettings.Instance.DocumentPrefix) %>";
            var sizePresets = "<%= HttpUtility.UrlEncode(ImageshopConfigurationSection.Instance.FormattedSizePresets) %>";

            var clienthost = "https://client.imageshop.no";

            var loc = window.location.toString(),
                params = loc.split('?')[1],
                iframe = document.getElementById("MyFrame");

            iframe.src = clienthost + "/InsertImage2.aspx?" + params + '&IFRAMEINSERT=true&IMAGESHOPTOKEN=' + token + '&SHOWSIZEDIALOGUE=' + showSizeDialog + '&SHOWCROPDIALOGUE=' + showCropDialog + '&FREECROP=' + freeCrop + '&IMAGESHOPINTERFACENAME=' + interfaceName + '&IMAGESHOPDOCUMENTPREFIX=' + documentPrefix + '&IMAGESHOPSIZES=' + sizePresets;

            $(window).resize(function () {
                setSize();
            });

            setSize();

            if (window.addEventListener) {
                addEventListener("message", listener, false);
            } else {
                attachEvent("onmessage", listener);
            }


        });

        function insert(file, title, width, height, alignment = null, descriptionstring = null) {

            var ed = tinyMCEPopup.editor, dom = ed.dom;

            var imgobj;
            var descriptionobject = null;

            if (descriptionstring != null) {
                descriptionobject = JSON.parse(descriptionstring);
                imgobj = {
                    src: file,
                    alt: descriptionobject.Description ? descriptionobject.Description : descriptionobject.Name,
                    border: null,
                    'data-credits': descriptionobject.Credits,
                    'data-authorName': descriptionobject.AuthorName,
                    'data-tags': descriptionobject.Tags,
                    'data-name': descriptionobject.Name,
                    'data-description': descriptionobject.Description

                }
                if (height != 0) {
                    imgobj.height = height;
                }
                if (width != 0) {
                    imgobj.width = width;
                }
            }
            else {

                if (height != 0) {

                    imgobj = {
                        src: file,
                        alt: title,
                        title: title,
                        border: 0,
                        width: width,
                        height: height
                    };
                }
                else if (width != 0) {
                    imgobj = {
                        src: file,
                        alt: title,
                        title: title,
                        border: 0,
                        width: width
                    };
                } else {
                    imgobj = {
                        src: file,
                        alt: title,
                        title: title,
                        border: 0
                    };
                }
            }

            if (alignment != null) {
                imgobj["style"] = "float: " + alignment;
            }

            tinyMCEPopup.execCommand('mceInsertContent', false, dom.createHTML('img', imgobj));

            tinyMCEPopup.close();

        }

    </script>
</head>
<body>
    <iframe id="MyFrame" />
</body>
</html>
