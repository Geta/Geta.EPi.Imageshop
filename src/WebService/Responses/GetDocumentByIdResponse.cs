using System;
using System.Xml.Serialization;

namespace Geta.EPi.Imageshop.WebService.Responses
{
    [Serializable]
    [XmlRoot("V4Document", Namespace = "http://imageshop.no/V4")]
    public class GetDocumentByIdResponse
    {
        [XmlElement]
        public int DocumentID { get; set; }
        [XmlElement]
        public string Code { get; set; }
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public string Credits { get; set; }
        [XmlElement]
        public string Rights { get; set; }
        [XmlElement]
        public string Description { get; set; }
        [XmlElement]
        public string Tags { get; set; }
        [XmlElement]
        public string Comment { get; set; }
        [XmlElement]
        public string AuthorName { get; set; }
        [XmlElement]
        public bool IsImage { get; set; }
        [XmlElement]
        public bool IsVideo { get; set; }
    }
}