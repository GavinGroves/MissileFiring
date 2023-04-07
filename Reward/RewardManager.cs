using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 奖励物品管理器
/// </summary>
public class RewardManager : MonoBehaviour
{
    private Transform m_Transform;
    private GameObject prefab_Reward;
    private int rewardCount = 0;//场景中存在几个，++计数器
    private int rewardMaxCount = 3;//当前场景最多存在多少奖励物品
    
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        prefab_Reward = Resources.Load<GameObject>("reward");
    }
    
    void Update()
    {
        //调用生成奖励物品方法
        InvokeRepeating("CreateReward",5,5);
    }

    /// <summary>
    /// 生成奖励礼物
    /// </summary>
    private void CreateReward()
    {
        //存在的奖励物品不能超过最大值
        if (rewardCount < rewardMaxCount)
        {
            Vector3 dir = new Vector3(Random.Range(-390,277), 0, Random.Range(-409,487));
            Instantiate(prefab_Reward, dir, Quaternion.identity, m_Transform);
            rewardCount++;
        }
    }

    /// <summary>
    /// 取消生成
    /// </summary>
    public void StopCreateReward()
    {
        CancelInvoke();
    }

    /// <summary>
    /// 飞机碰到一个奖励物品，当前场景存在的减少一个
    /// 从子级（Reward）收到被销毁消息，执行当前方法
    /// </summary>
    public void RewardCountDown()
    {
        rewardCount--;
        Debug.Log("我减少了可以创建新的了");
    }
}
