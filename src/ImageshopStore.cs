using System.Web.Mvc;
using EPiServer.Shell.Services.Rest;
using Screentek.EPi.Imageshop.Configuration;
using Screentek.EPi.Imageshop.WebService;
using Screentek.EPi.Imageshop.WebService.Responses;

namespace Screentek.EPi.Imageshop
{
    [RestStore("imageshopstore")]
    public class ImageshopStore : RestControllerBase
    {
        private readonly WebServiceWrapper _webServiceWrapper;

        public ImageshopStore()
        {
            _webServiceWrapper = new WebServiceWrapper(ImageshopSettings.Instance.WebServiceUrl, ImageshopSettings.Instance.Token);
        }

        [HttpGet]
        public ActionResult Get(string permalink)
        {
            GetDocumentIdFromPermalinkResponse idResponse = _webServiceWrapper.GetDocumentIdFromPermalink(permalink).Result;

            if (idResponse != null)
            {
                GetDocumentByIdResponse documentResponse = _webServiceWrapper.GetDocumentById(idResponse.DocumentID).Result;

                if (documentResponse != null)
                {
                    return Rest(documentResponse);
                }
            }

            return Rest(null);
        }
    }
}