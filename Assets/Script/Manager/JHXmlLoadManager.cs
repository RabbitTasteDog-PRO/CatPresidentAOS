using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class JHXmlLoadManager : MonoBehaviour
{
    // Resources/XML/TestItem.XML 파일.
    string xmlFileName = "TestItem";

    void Start()
    {
        LoadXML(xmlFileName);
    }

    private void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("XML/" + _fileName);
        XmlDocument xmlDoc = new XmlDocument();
        Debug.Log(txtAsset.text);
        xmlDoc.LoadXml(txtAsset.text);

        // 하나씩 가져오기 테스트 예제.
        XmlNodeList cost_Table = xmlDoc.GetElementsByTagName("cost");
        foreach (XmlNode cost in cost_Table)
        {
            Debug.Log("[one by one] cost : " + cost.InnerText);
        }

        // 전체 아이템 가져오기 예제.
        XmlNodeList all_nodes = xmlDoc.SelectNodes("dataroot/TestItem");
        foreach (XmlNode node in all_nodes)
        {
            // 수량이 많으면 반복문 사용.
            Debug.Log("[at once] id :" + node.SelectSingleNode("id").InnerText);
            Debug.Log("[at once] name : " + node.SelectSingleNode("name").InnerText);
            Debug.Log("[at once] cost : " + node.SelectSingleNode("cost").InnerText);
        }
    }
}
