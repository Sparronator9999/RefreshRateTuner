using System.Xml.Serialization;

namespace RefreshRateTuner.Config
{
    public class DispConf
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement]
        public bool ChangeOnBattery { get; set; }

        [XmlElement]
        public int RefreshRateAC { get; set; }

        [XmlElement]
        public int RefreshRateDC { get; set; }
    }
}
