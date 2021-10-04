using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPos;

    public int startHeart = 4;//��ʼѪ��
    public int maxHeart = 5;//���Ѫ��

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
        GlitchEffect.Instance.Glitch();
        // UI�ӿ� **
        // �����ӿ� **
        // ���ֽӿ� **
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
    /// ���ý�ɫ
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
