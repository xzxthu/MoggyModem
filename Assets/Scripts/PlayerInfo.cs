using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPos;

    public int startHeart = 4;
    public int maxHeart = 5;
    public GameObject hurtLight;

    public int heart;

    private bool hurting = false;

    public static PlayerInfo Instance;
    public GameObject player;

    private float timer = 0;

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

    private void Update()
    {
        if(hurting)
        {
            timer += Time.deltaTime;
            if(timer>0.01f)
            {
                timer = 0;
                LateDeduce();
            }
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
        MusicManager.Instance.SetMusic(heart);
        HeartBar.Instance.SetHeart(heart);
    }

    /// <summary>
    /// ��Ѫ
    /// </summary>
    public void DeductHeart()
    {
        GlitchEffect.Instance.Glitch();
        hurtLight.SetActive(true);

        if (!hurting)
        {
            hurting = true;
        }
        
    }

    private void LateDeduce()
    {

        hurting = false;

        heart = Mathf.Max((heart - 1), 0);
        HeartBar.Instance.SetHeart(heart);
        if (heart == 0)
        {
            LevelManager.Instance.GameOver();
        }
        else
        {
            MusicManager.Instance.SetMusic(heart);
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
        player.GetComponent<playerMovement>().isHurting = false;
        player.GetComponent<playerMovement>().Hint.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void ResetPosition()
    {
        player.GetComponent<playerMovement>().isDrag = false;
        player.transform.position = StartPos;
    }



}
