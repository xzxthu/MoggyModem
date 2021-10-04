using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int startHeart = 4;//��ʼѪ��
    public int maxHeart = 5;//���Ѫ��


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
    /// ��Ѫ
    /// </summary>
    public void AddHeart()
    {
        heart = Mathf.Min((heart + 1), maxHeart);
        // UI�ӿ� **
        // �����ӿ� **
        // ���ֽӿ� **
    }

    /// <summary>
    /// ��Ѫ
    /// </summary>
    public void DeductHeart()
    {
        heart = Mathf.Max((heart - 1), 0);

        // ��Ļ��˸ **
        // UI�ӿ� **
        // �����ӿ� **
        // ���ֽӿ� **

        if (heart==0)
        {
            LevelManager.Instance.GameOver();
        }
    }

    /// <summary>
    /// ���ý�ɫ
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
