using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Start Scenes UI界面管理器
/// </summary>
public class StartUIManager : MonoBehaviour
{
    private Transform m_Transform;
    private GameObject m_StartPanel; //开始面板
    private GameObject m_SetingPanel; //设置面板

    private Button button_Setting; //startPanel 设置按钮
    private Button button_Close; //setingPanel 关闭按钮
    private Button button_Play; //startPanel 开始游戏按钮

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_StartPanel = m_Transform.Find("StartPanel").gameObject;
        m_SetingPanel = m_Transform.Find("SetingPanel").gameObject;
        m_SetingPanel.SetActive(false);

        //startPanel 设置按键 点击开启SetingPanel
        button_Setting = m_Transform.Find("StartPanel/Setting").GetComponent<Button>();
        button_Setting.onClick.AddListener(SetingButtonClick);

        //SetingPanel 点击× 关闭设置界面
        button_Close = m_Transform.Find("SetingPanel/SmallBox/Title/Close").GetComponent<Button>();
        button_Close.onClick.AddListener((() => m_SetingPanel.SetActive(false)));

        //startPanel 开始游戏Play 跳转Game场景
        button_Play = m_Transform.Find("StartPanel/Play").GetComponent<Button>();
        button_Play.onClick.AddListener((() => SceneManager.LoadScene("Game")));
    }

    /// <summary>
    /// Start界面设置按钮点击
    /// </summary>
    private void SetingButtonClick()
    {
        //首先判断setingPanel是否已经打开，已打开就不重复执行
        if (m_SetingPanel.activeSelf == false)
        {
            m_SetingPanel.SetActive(true);
        }
    }

    /// <summary>
    /// 设置开始游戏按钮的状态
    /// </summary>
    /// <param name="state"></param>
    public void SetPlayButtonState(int state)
    {
        //为1，已购买，可以玩
        if (state == 1)
        {
            button_Play.gameObject.SetActive(true);
        }
        else
        {
            button_Play.gameObject.SetActive(false);
        }
    }
}