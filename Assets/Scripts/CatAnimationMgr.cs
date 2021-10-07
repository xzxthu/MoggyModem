using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationMgr : MonoBehaviour
{
    public GameObject[] Idle;
    public GameObject ShakeWarning;
    public GameObject AddHeart;
    public GameObject Holding;
    public GameObject Fixing;
    public GameObject CatGameOver;

    private bool isIdle;

    public static CatAnimationMgr Instance;
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
        SetIdle(5);
    }

    private void Update()
    {
        if (PlayerInfo.Instance.GetHeart() < 3 || !isIdle) return;

        if(Random.Range(0,1000)>998)
        {
            CloseAll();
            Fixing.SetActive(true);
        }
    }

    public void CloseAll()
    {
        StopAllCoroutines();
        for(int i = 0;i<Idle.Length;i++)
        {
            Idle[i].SetActive(false);
        }
        ShakeWarning.SetActive(false);

        AddHeart.SetActive(false);
        
        Holding.SetActive(false);
        
        Fixing.SetActive(false);

        CatGameOver.SetActive(false);

        isIdle = false;
    }

    public void SetIdle(int heart)
    {
        CloseAll();
        Idle[5-heart].SetActive(true);
        isIdle = true;
    }

    public void SetWarning()
    {
        CloseAll();
        ShakeWarning.SetActive(true);
        StartCoroutine(LatePlayHolding());
    }

    private IEnumerator LatePlayHolding()
    {
        yield return new WaitForSeconds(8f/12f);
        SetIdle(PlayerInfo.Instance.GetHeart());
    }

    public void StartHolding()
    {
        CloseAll();
        Holding.SetActive(true);
    }

    public void EndHolding()
    {
        Holding.GetComponent<Animator>().SetTrigger("HoldingEnd");
    }

    public void SetAddHeart()
    {
        CloseAll();
        AddHeart.SetActive(true);
    }

    public void SetGameOver()
    {
        CloseAll();
        CatGameOver.SetActive(true);
    }
}


