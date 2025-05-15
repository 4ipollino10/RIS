using System.Xml.Serialization;

namespace CrackHash.Worker.Models
{
    [XmlRoot(ElementName = "CrackHashWorkerResponse", 
        Namespace = "http://ccfit.nsu.ru/schema/crack-hash-response")]
    public class CrackHashWorkerResponse
    {
        [XmlElement(ElementName = "RequestId")]
        public string RequestId { get; set; }

        [XmlElement(ElementName = "PartNumber")]
        public int PartNumber { get; set; }

        [XmlElement(ElementName = "Answers")]
        public AnswersContainer Answers { get; set; } = new AnswersContainer();

        public class AnswersContainer
        {
            [XmlElement(ElementName = "words")]
            public List<string> Words { get; set; } = new List<string>();
        }
    }
}