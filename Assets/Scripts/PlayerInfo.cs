using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPos;

    public int startHeart = 4;//初始血量
    public int maxHeart = 5;//最大血量

    private bool hurting = false;
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
        StartPos = GameObject.FindWithTag("Player").transform.position;
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
        GlitchEffect.Instance.Glitch();
        // UI接口 **
        // 动画接口 **
        // 音乐接口 **
        if (!hurting)
        {
            hurting = true;
            StartCoroutine(LateDeduce());
        }
        
    }

    private IEnumerator LateDeduce()
    {
        yield return new WaitForSeconds(0.01f);

        hurting = false;

        heart = Mathf.Max((heart - 1), 0);

        if (heart == 0)
        {
            LevelManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// 重置角色
    /// </summary>
    public void ResetCharacter()
    {
        StopAllCoroutines();
        hurting = false;
        heart = startHeart;
        ResetPosition();
    }

    private void ResetPosition()
    {
        GameObject.FindWithTag("Player").GetComponent<playerMovement>().isDrag = false;
        GameObject.FindWithTag("Player").transform.position = StartPos;
        StartCoroutine(LateRestPos());
    }

    private IEnumerator LateRestPos()
    {
        yield return new WaitForSeconds(0.01f);
        
    }

}
