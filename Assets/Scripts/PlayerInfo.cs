using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [HideInInspector] public Vector3 StartPos;

    public static int startHeart = 5;
    public static int maxHeart = 5;
    public GameObject hurtLight;

    [SerializeField] private int heart;

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
        if(heart> maxHeart-1)
        {
            HeartBar.Instance.SetHeart(maxHeart);
            return;
        }

        int addHeart = heart + 1;
        SetHeart(Mathf.Min(addHeart, maxHeart));
        //Debug.Log("Add Heart and now is " + heart);

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
        MusicManager.Instance.PlaySEHurt();

        if (!hurting)
        {
            hurting = true;
        }
        
    }

    private void LateDeduce()
    {

        hurting = false;
        int minusHeart = heart - 1;
        heart = Mathf.Max(minusHeart, 0);
        //Debug.Log("Reduce Heart and now is " + heart);
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

    public void SetHeart(int newHeart)
    {
        heart = newHeart;
    }

    public int GetHeart()
    {
        return heart;
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

    public void ResetPosition()
    {
        player.GetComponent<playerMovement>().isDrag = false;
        player.transform.position = StartPos;
        player.GetComponent<playerMovement>().isHurting = false;
        player.GetComponent<playerMovement>().Hint.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
    }



}
