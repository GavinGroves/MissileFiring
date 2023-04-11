using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    private GameObject m_GamePanel; //游戏面板
    private GameObject m_OverPanel; //结束面板
    private GameObject m_ButtonControl; //按键控制器1

    private Text text_GameStarNum; //Game界面金币
    private Text text_GameTime; //游戏时间

    //结束Over面板字段
    private Text text_OverStarNum; //Over界面+金币
    private Text text_OverMaxScore; //最高得分
    private Text text_OverTime; //结束界面+时间

    private Button button_Reset; //点击回到Start场景

    private int time; //时间

    public int Time
    {
        get { return time; }
        set
        {
            time = value;
            UpdateGameTime(time);
        }
    }

    void Start()
    {
        m_GamePanel = GameObject.Find("GamePanel");
        m_OverPanel = GameObject.Find("OverPanel");
        m_ButtonControl = GameObject.Find("GamePanel/ButtonControl");

        text_GameStarNum = GameObject.Find("GamePanel/Star/StarNum").GetComponent<Text>();
        text_GameStarNum.text = "0";
        text_GameTime = GameObject.Find("GamePanel/Time").GetComponent<Text>();
        text_GameTime.text = "0:0";
        StartCoroutine("AddTime");

        text_OverStarNum = GameObject.Find("OverPanel/Star/StarNum").GetComponent<Text>();
        text_OverMaxScore = GameObject.Find("OverPanel/Score/ScoreNum").GetComponent<Text>();
        text_OverTime = GameObject.Find("OverPanel/Time/TimeNum").GetComponent<Text>();

        button_Reset = GameObject.Find("OverPanel/Reset").GetComponent<Button>();
        button_Reset.onClick.AddListener((() => SceneManager.LoadScene("Start")));

        m_OverPanel.SetActive(false);
    }

    /// <summary>
    /// 实时更新游戏 UI数值显示
    /// </summary>
    public void UpdateStarNum(int star)
    {
        text_GameStarNum.text = star.ToString();
    }

    /// <summary>
    /// 更新游戏时间
    /// </summary>
    private void UpdateGameTime(int t)
    {
        if (t > 60)
        {
            text_GameTime.text = "0:" + t;
        }
        else
        {
            text_GameTime.text = t / 60 + ":" + t % 60;
        }
    }

    /// <summary>
    /// 显示结束Over界面，按键隐藏
    /// </summary>
    public void ShowOverPanel()
    {
        m_ButtonControl.SetActive(false);
        m_OverPanel.SetActive(true);
        StopAddTime();
        SetOverPanelInfo();
    }

    /// <summary>
    /// 协程累计时间，计时器
    /// </summary>
    IEnumerator AddTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Time++;
        }
    }

    /// <summary>
    /// 停止时间增加协程
    /// </summary>
    private void StopAddTime()
    {
        StopCoroutine("AddTime");
    }

    /// <summary>
    /// 结束面板UI赋值
    /// </summary>
    private void SetOverPanelInfo()
    {
        int star = int.Parse(text_GameStarNum.text);
        text_OverStarNum.text = "+" + star * 10;
        text_OverTime.text = "+" + time;
        int tempScore = star * 10 + time;
        text_OverMaxScore.text = tempScore.ToString();
        //存储最高分
        PlayerPrefs.SetInt("HeightScore", tempScore);
        //存储金币
        PlayerPrefs.SetInt("GoldCount", star * 10);
    }
}