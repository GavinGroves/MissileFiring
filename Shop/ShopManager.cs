using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商城模块总控制器
/// </summary>
public class ShopManager : MonoBehaviour
{
    private Transform m_Transform;

    //商城数据对象
    private ShopData shopData;
    //引用持有
    private StartUIManager m_StartUIManager;

    //xml路径
    private string dataPath = "Assets/Datas/ShopData.xml";
    private string savePath = "Assets/Datas/SaveData.xml";

    //商城商品模板
    private GameObject ui_ShopItem;

    //shop左右按键
    private Button btn_Left;
    private Button btn_Right;

    private Text text_StartNum;
    private Text text_ScoreNum;

    //要展示的UI的索引
    private int index = 0;

    //商品UI的集合
    private List<GameObject> shopUI = new List<GameObject>();

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        //按键事件绑定
        btn_Left = m_Transform.Find("Left").GetComponent<Button>();
        btn_Right = m_Transform.Find("Right").GetComponent<Button>();
        btn_Left.onClick.AddListener(LeftButtonClick);
        btn_Right.onClick.AddListener(RightButtonClick);
        //实例化商城数据对象
        shopData = new ShopData();
        m_StartUIManager = GameObject.Find("Canvas").GetComponent<StartUIManager>();
        //加载xml
        shopData.ReadXMLByPath(dataPath);
        shopData.ReadXMLByGoldScoreAndState(savePath);
        //加载Item
        ui_ShopItem = Resources.Load<GameObject>("UI/ShopItem");
        //同步UI与xml中的 分数、金币数值
        text_StartNum = GameObject.Find("Canvas/StartPanel/Star/StarNum").GetComponent<Text>();
        text_ScoreNum = GameObject.Find("Canvas/StartPanel/Score/ScoreNum").GetComponent<Text>();
        UpdateUI();
        SetPlayerInfo(shopData.shopList[0].Model);
        // m_StartUIManager.SetPlayButtonState(shopData.shopStateList[index]);
        CreateAllShopUI();
    }

    /// <summary>
    /// 更新UI数据显示
    /// </summary>
    private void UpdateUI()
    {
        text_StartNum.text = shopData.goldCount.ToString();
        text_ScoreNum.text = shopData.heightScore.ToString();
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
            //UI赋值
            item.GetComponent<ShopItemUI>().SetItemValue(shopData.shopList[i].ID, shopData.shopList[i].Speed,
                shopData.shopList[i].Rotate,
                shopData.shopList[i].Price, ship, shopData.shopStateList[i]);
            //将生成的UI添加进集合，进行管理
            shopUI.Add(item);
        }

        //隐藏UI
        ShopUIHideAndShow(index);
    }
    
    /// <summary>
    /// 左侧按钮点击事件
    /// </summary>
    private void LeftButtonClick()
    {
        if (index > 0)
        {
            index--;
            int temp = shopData.shopStateList[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index].Model);
            ShopUIHideAndShow(index);
        }
    }

    /// <summary>
    /// 右侧按钮点击事件
    /// </summary>
    private void RightButtonClick()
    {
        if (index < shopUI.Count - 1)
        {
            index++;
            int temp = shopData.shopStateList[index];
            m_StartUIManager.SetPlayButtonState(temp);
            SetPlayerInfo(shopData.shopList[index].Model);
            ShopUIHideAndShow(index);
        }
    }

    /// <summary>
    /// 商城UI的显示与隐藏
    /// </summary>
    private void ShopUIHideAndShow(int index)
    {
        for (int i = 0; i < shopUI.Count; i++)
        {
            shopUI[i].SetActive(false);
        }

        shopUI[index].SetActive(true);
    }

    /// <summary>
    /// 计算商品价格
    /// 从ShopItemUI脚本接收对象，执行方法
    /// </summary>
    private void CalcItemPrice(ShopItemUI item)
    {
        if (shopData.goldCount >= item.itemPrice)
        {
            //购买成功 → 隐藏购买UI按钮
            item.BuyEnd();
            //购买后减掉花费的金币
            shopData.goldCount -= item.itemPrice;
            //减完需要更新UI数据
            UpdateUI();
            //更新xml文档数值,金币数量更新
            shopData.UpdateXMLData(savePath, "GoldCount", shopData.goldCount.ToString());
            //更新xml商品购买状态
            shopData.UpdateXMLData(savePath, "ID" + item.itemId, "1");
        }
        else
        {
            Debug.Log("购买失败，金币不足");
        }
    }

    /// <summary>
    /// 存储当前角色信息到PlayerPrefs中去
    /// </summary>
    private void SetPlayerInfo(string name)
    {
        PlayerPrefs.SetString("PlayerName",name);
    }
}