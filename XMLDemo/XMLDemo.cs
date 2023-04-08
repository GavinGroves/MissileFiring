using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml; //引入xml操作相关的命名空间

/// <summary>
/// XML操作演示
/// </summary>
public class XMLDemo : MonoBehaviour
{
    //定义字段，存储xml的路径
    private string xmlPath = "Assets/Datas/web.xml";

    void Start()
    {
        ReadXMLByPath(xmlPath);
    }

    /// <summary>
    /// 通过路径读取xml中的数据进行显示
    /// </summary>
    private void ReadXMLByPath(string path)
    {
        //实例化xml文档操作对象
        XmlDocument xmlDoc = new XmlDocument();
        //使用xml对象加载xml
        xmlDoc.Load(path);
        //获取根节点
        XmlNode root = xmlDoc.SelectSingleNode("Web");
        //获取根节点下所有子节点
        XmlNodeList nodeList = root.ChildNodes;
        //for循环 遍历输出 node表示一个item（web.xml里面目前有三个item）
        foreach (XmlNode node in nodeList)
        {
            //获取<item>上属性id的值
            int id = int.Parse(node.Attributes["id"].Value);
            //获取node(item) 下的子节点，innertext用于获取值
            string name = node.ChildNodes[0].InnerText;
            string url = node.ChildNodes[1].InnerText;
            Debug.Log(id + "---" + name + "---" + url + "---");
        }
    }
}