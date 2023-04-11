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
    //商品购买状态集合
    public List<int> shopStateList = new List<int>();

    //金币数
    public int goldCount = 0;
    //最高分数
    public int heightScore = 0;
    
    /// <summary>
    /// 读取xml
    /// </summary>
    /// <param name="path">shopData.xml路径地址</param>
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
            string id = item.ChildNodes[4].InnerText;
            string player = item.ChildNodes[5].InnerText;

            //遍历打印测试后，存储到List集合中
            ShopItem shopItem = new ShopItem(speed, rotate, model, price,id,player);
            shopList.Add(shopItem);
        }
    }

    /// <summary>
    /// 读取金币数、最高分数,商品购买状态
    /// </summary>
    /// <param name="path">SavaData.xml</param>
    public void ReadXMLByGoldScoreAndState(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;
        goldCount = int.Parse(nodeList[0].InnerText);
        heightScore = int.Parse(nodeList[1].InnerText);
        //加载购买状态，添加到集合
        for (int i = 2; i < 6; i++)
        {
            shopStateList.Add(int.Parse(nodeList[i].InnerText));
        }
    }

    /// <summary>
    /// 更新xml文档内容
    /// </summary>
    /// <param name="path">xml路径地址</param>
    /// <param name="key">要更新的节点node </param>
    /// <param name="value">要更新的节点里的数值</param>
    public void UpdateXMLData(string path,string key,string value)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("SaveData");
        XmlNodeList nodeList = root.ChildNodes;
        foreach (XmlNode node in nodeList)
        {
            if (node.Name == key)
            {
                node.InnerText = value;
                doc.Save(path);
            }
        }
    }
}