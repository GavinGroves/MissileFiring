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
        string ship = PlayerPrefs.GetString("PlayerName");
        GameObject model = Resources.Load<GameObject>(ship);
        player = Instantiate(model, Vector3.zero, Quaternion.identity);
        player.AddComponent<Ship>();
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
