using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AvorionDataConverter.ViewModels {
    [XmlRoot(ElementName = "coordinates")]
    public class Coordinates {
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }

        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }

        public override string ToString() => $"{X:###}:{Y:###}";
    }

    [XmlRoot(ElementName = "arg")]
    public class Arg {
        [XmlAttribute(AttributeName = "key")]
        public string Key { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "title")]
    public class Title {
        [XmlElement(ElementName = "arg")]
        public List<Arg> Arg { get; set; }

        [XmlAttribute(AttributeName = "str")]
        public string Str { get; set; }

        public string GetformattedName => Arg.Aggregate(Str, (result, formatArg) => result.Replace($"${{{formatArg.Key}}}", formatArg.Text));
    }

    [XmlRoot(ElementName = "titles")]
    public class Titles {
        [XmlElement(ElementName = "title")]
        public List<Title> Title { get; set; }
    }

    [XmlRoot(ElementName = "gates")]
    public class Gates {
        [XmlElement(ElementName = "coordinates")]
        public List<Coordinates> Coordinates { get; set; }
    }

    [XmlRoot(ElementName = "wormholes")]
    public class Wormholes {
        [XmlElement(ElementName = "coordinates")]
        public Coordinates Coordinates { get; set; }
    }

    [XmlRoot(ElementName = "ships")]
    public class Ships {
        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }

        [XmlAttribute(AttributeName = "faction")]
        public string Faction { get; set; }

        [XmlAttribute(AttributeName = "uuid")]
        public string Uuid { get; set; }
    }

    [XmlRoot(ElementName = "shipNumbers")]
    public class ShipNumbers {
        [XmlElement(ElementName = "ships")]
        public List<Ships> Ships { get; set; }
    }

    [XmlRoot(ElementName = "shipUuids")]
    public class ShipUuids {
        [XmlElement(ElementName = "ships")]
        public List<Ships> Ships { get; set; }
    }

    [XmlRoot(ElementName = "view")]
    public class View {
        [XmlElement(ElementName = "coordinates")]
        public Coordinates Coordinates { get; set; }

        [XmlElement(ElementName = "titles")]
        public Titles Titles { get; set; }

        [XmlElement(ElementName = "wormholes")]
        public Wormholes Wormholes { get; set; }

        [XmlElement(ElementName = "gates")]
        public Gates Gates { get; set; }

        [XmlElement(ElementName = "shipNumbers")]
        public ShipNumbers ShipNumbers { get; set; }

        [XmlElement(ElementName = "shipUuids")]
        public ShipUuids ShipUuids { get; set; }

        [XmlAttribute(AttributeName = "numCrafts")]
        public string NumCrafts { get; set; }

        [XmlAttribute(AttributeName = "numWrecks")]
        public string NumWrecks { get; set; }

        [XmlAttribute(AttributeName = "numAsteroids")]
        public string NumAsteroids { get; set; }

        [XmlAttribute(AttributeName = "numStations")]
        public string NumStations { get; set; }

        [XmlAttribute(AttributeName = "influence")]
        public string Influence { get; set; }

        [XmlAttribute(AttributeName = "factionIndex")]
        public string FactionIndex { get; set; }

        [XmlAttribute(AttributeName = "deathLocation")]
        public string DeathLocation { get; set; }

        [XmlAttribute(AttributeName = "manuallyTagged")]
        public string ManuallyTagged { get; set; }
    }
}