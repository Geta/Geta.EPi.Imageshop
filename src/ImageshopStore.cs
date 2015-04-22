using System.Web.Mvc;
using EPiServer.Shell.Services.Rest;
using Geta.EPi.Imageshop.Configuration;
using Geta.EPi.Imageshop.WebService;
using Geta.EPi.Imageshop.WebService.Responses;

namespace Geta.EPi.Imageshop
{
    [RestStore("imageshopstore")]
    public class ImageshopStore : RestControllerBase
    {
        private readonly string _webServiceUrl;
        private readonly string _token;

        public ImageshopStore()
        {
            _webServiceUrl = ImageshopSettings.Instance.WebServiceUrl;
            _token = ImageshopSettings.Instance.Token;
        }

        [HttpGet]
        public ActionResult Get(string permalink)
        {
            var wrapper = new WebServiceWrapper(_webServiceUrl, _token);
            GetDocumentIdFromPermalinkResponse idResponse = wrapper.GetDocumentIdFromPermalink(permalink).Result;

            if (idResponse != null)
            {
                GetDocumentByIdResponse documentResponse = wrapper.GetDocumentById(idResponse.DocumentID).Result;

                if (documentResponse != null)
                {
                    return Rest(documentResponse);
                }
            }

            return Rest(null);
        }
    }
}