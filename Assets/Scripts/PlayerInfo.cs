using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPos;

    public int startHeart = 4;//��ʼѪ��
    public int maxHeart = 5;//���Ѫ��

    private bool hurting = false;
    public int heart;

    public static PlayerInfo Instance;
    public GameObject player;
    

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
        
        StartPos = player.transform.position;
        ResetCharacter();
    }

    /// <summary>
    /// ��Ѫ
    /// </summary>
    public void AddHeart()
    {
        if(heart+1> maxHeart)
        {
            HeartBar.Instance.SetHeart(maxHeart);
            return;
        }

        heart = Mathf.Min((heart + 1), maxHeart);
        Debug.Log("Add Heart");

        CatAnimationMgr.Instance.SetAddHeart();
        MusicManager.Instance.StopAddMew(heart);
        HeartBar.Instance.SetHeart(heart);
    }

    /// <summary>
    /// ��Ѫ
    /// </summary>
    public void DeductHeart()
    {
        GlitchEffect.Instance.Glitch();

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
        HeartBar.Instance.SetHeart(heart);
        if (heart == 0)
        {
            LevelManager.Instance.GameOver();
            yield return null;
        }
        else
        {
            MusicManager.Instance.PlayAddMew(heart);
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
        player.GetComponent<playerMovement>().isDrag = false;
        player.transform.position = StartPos;
        StartCoroutine(LateResetPos());
    }

    private IEnumerator LateResetPos()
    {
        yield return new WaitForSeconds(0.01f);
        
    }

}
