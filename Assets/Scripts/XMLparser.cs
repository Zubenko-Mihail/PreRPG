using System.Collections.Generic;
using System.Xml.Linq;
using System;
using UnityEngine;

public class XMLparser 
{
    TextAsset XMLasset;
    XDocument xmlDoc;
    IEnumerable<XElement> wholeFile;
    public void SetDoc(string fullXmlName) {
        XMLasset = Resources.Load<TextAsset>(fullXmlName);
        xmlDoc = XDocument.Parse(XMLasset.text);
    }
    public XElement GetNPCReplic(int replicN)
    {
        XElement retS = null;
        wholeFile = xmlDoc.Element("document").Element("NPC").Elements();
        foreach(XElement elem in wholeFile)
        {
            if (replicN == Convert.ToInt32(elem.Attribute("n").Value))
            {
                retS=elem;
                return retS;
            }
        }
        return retS;
    }
    public XElement GetAnswers(int replicN, int count)
    {
        XElement retS = xmlDoc.Element("document").Element("Default");
        wholeFile = xmlDoc.Element("document").Element("Answers").Elements();
        foreach (XElement elem in wholeFile)
        {
            if (replicN == Convert.ToInt32(elem.Attribute("from").Value))
            {
                if (count == 1)
                {
                    retS = elem;
                    return retS;
                }
                else
                {
                    count--;
                    continue;
                }
            }
        }
        return retS;
    }
}
