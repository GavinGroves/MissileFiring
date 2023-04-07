using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    private GameObject m_GamePanel;
    private GameObject m_OverPanel;
    private GameObject m_ButtonControl; //按键控制器1

    private Text text_GameStarNum; //Game界面分数
    private Text text_OverStarNum; //Over分数

    private Button button_Reset; //点击回到Start场景

    void Start()
    {
        m_GamePanel = GameObject.Find("GamePanel");
        m_OverPanel = GameObject.Find("OverPanel");
        m_ButtonControl = GameObject.Find("GamePanel/ButtonControl");

        text_GameStarNum = GameObject.Find("GamePanel/Star/StarNum").GetComponent<Text>();
        text_GameStarNum.text = "0";
        text_OverStarNum = GameObject.Find("OverPanel/Star/StarNum").GetComponent<Text>();

        button_Reset = GameObject.Find("OverPanel/Reset").GetComponent<Button>();
        button_Reset.onClick.AddListener((() => SceneManager.LoadScene("Start")));

        m_OverPanel.SetActive(false);
    }

    void Update()
    {
    }

    /// <summary>
    /// 实时更新游戏starNum UI显示
    /// </summary>
    public void UpdateStarNum(int star)
    {
        text_GameStarNum.text = star.ToString();
        text_OverStarNum.text = "+" + star;
    }

    /// <summary>
    /// 显示结束Over界面，按键隐藏
    /// </summary>
    public void ShowOverPanel()
    {
        m_ButtonControl.SetActive(false);
        m_OverPanel.SetActive(true);
    }
}