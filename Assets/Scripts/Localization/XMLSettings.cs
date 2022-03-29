using System.IO;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("root")]
public class XMLSettings 
{
    [XmlElement("UIelement")]
    public UIelement[] UIelements;

    public static XMLSettings Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XMLSettings));
        StringReader reader = new StringReader(_xml.text);
        XMLSettings settings = serializer.Deserialize(reader) as XMLSettings;
        return settings;
    }
}

[System.Serializable]
public class UIelement
{
    [XmlAttribute("ElementKey")]
    public int ElementKey;

    [XmlElement("text")]
    public string text;

}








