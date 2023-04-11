using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 飞机管理器
/// </summary>
public class ShipManager : MonoBehaviour
{
    private GameObject model;
    private GameObject player;
    void Start()
    {
        //读取角色信息
        string ship = PlayerPrefs.GetString("PlayerName");
        int speed = PlayerPrefs.GetInt("PlayerSpeed");
        int rotate = PlayerPrefs.GetInt("PlayerRotate");
        
        //动态加载模型，实例化角色
        GameObject model = Resources.Load<GameObject>(ship);
        player = Instantiate(model, Vector3.zero, Quaternion.identity);
        
        //角色添加组件，设置属性
        Ship myShip = player.AddComponent<Ship>();
        myShip.Speed = speed;
        myShip.Rotate = rotate;
        player.tag = "Player";
        if (model.name != "Ship_4")
        {
            player.GetComponent<Transform>().localScale = new Vector3(2, 2, 2);
        }
        else
        {
            player.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
