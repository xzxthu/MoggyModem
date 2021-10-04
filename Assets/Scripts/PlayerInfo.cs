using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int startHeart = 4;//初始血量
    public int maxHeart = 5;//最大血量


    [SerializeField] private int heart;

    public static PlayerInfo Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ResetCharacter();
    }

    /// <summary>
    /// 加血
    /// </summary>
    public void AddHeart()
    {
        heart = Mathf.Min((heart + 1), maxHeart);
        // UI接口 **
        // 动画接口 **
        // 音乐接口 **
    }

    /// <summary>
    /// 扣血
    /// </summary>
    public void DeductHeart()
    {
        heart = Mathf.Max((heart - 1), 0);

        // 屏幕闪烁 **
        // UI接口 **
        // 动画接口 **
        // 音乐接口 **

        if (heart==0)
        {
            LevelManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// 重置角色
    /// </summary>
    public void ResetCharacter()
    {
        heart = startHeart;
        ResetPosition();
    }

    private void ResetPosition()
    {
        
    }
}
