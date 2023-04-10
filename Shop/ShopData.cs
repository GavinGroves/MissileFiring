using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 商城XML数据读取显示
/// </summary>
public class ShopData
{
    //用于存储xml数据的实体集合
    public List<ShopItem> shopList = new List<ShopItem>();

    /// <summary>
    /// 读取xml
    /// </summary>
    /// <param name="path">xml路径地址</param>
    public void ReadXMLByPath(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("Shop");
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode item in nodeList)
        {
            string speed = item.ChildNodes[0].InnerText;
            string rotate = item.ChildNodes[1].InnerText;
            string model = item.ChildNodes[2].InnerText;
            string price = item.ChildNodes[3].InnerText;

            //遍历打印测试后，存储到List集合中
            ShopItem shopItem = new ShopItem(speed, rotate, model, price);
            shopList.Add(shopItem);
        }
    }
}