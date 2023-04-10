using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商城模块总控制器
/// </summary>
public class ShopManager : MonoBehaviour
{
    private Transform m_Transform;

    //商城数据对象
    private ShopData shopData;

    //xml路径
    private string dataPath = "Assets/Datas/ShopData.xml";

    //商城商品模板
    private GameObject ui_ShopItem;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        //实例化商城数据对象
        shopData = new ShopData();
        //加载xml
        shopData.ReadXMLByPath(dataPath);
        ui_ShopItem = Resources.Load<GameObject>("UI/ShopItem");
        CreateAllShopUI();
    }

    /// <summary>
    /// 创建商城模块中所有的商品
    /// </summary>
    private void CreateAllShopUI()
    {
        for (int i = 0; i < shopData.shopList.Count; i++)
        {
            //实例化item UI
            GameObject item = Instantiate(ui_ShopItem, m_Transform.position, Quaternion.identity, m_Transform);
            item.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
            //加载飞机模型
            GameObject ship = Resources.Load<GameObject>(shopData.shopList[i].Model);
            //UI层
            ship.layer = 5;
            Transform trans_Ship = ship.GetComponent<Transform>();
            trans_Ship.Find("Mesh").gameObject.layer = 5;
            //设置飞机模型缩放信息（基于model父物体，使用local）
            trans_Ship.localPosition = new Vector3(0, -40, 0);
            trans_Ship.localRotation = Quaternion.Euler(-90, 0, 0);
            trans_Ship.localScale = new Vector3(8, 8, 8);

            //UI赋值
            item.GetComponent<ShopItemUI>().SetItemValue(shopData.shopList[i].Speed, shopData.shopList[i].Rotate,
                shopData.shopList[i].Price, ship);
        }
    }
}