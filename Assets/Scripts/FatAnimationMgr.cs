using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatAnimationMgr : MonoBehaviour
{
    public GameObject Idle;
    public GameObject Idle2;
    public GameObject Angry;
    public GameObject GameOver;
    

    private bool isIdle = true;

    public static FatAnimationMgr Instance;
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
        SetIdle();
    }

    private void Update()
    {
        if (!isIdle) return;

        if (Random.Range(0, 1000) > 998)
        {
            CloseAll();
            Idle2.SetActive(true);
        }
    }

    private void CloseAll()
    {
        Idle.SetActive(false);
        Idle2.SetActive(false);
        Angry.SetActive(false);
        GameOver.SetActive(false);
        isIdle = false;
    }

    public void SetIdle()
    {
        CloseAll();
        Idle.SetActive(true);
        isIdle = true;
    }

    public void SetShaking()
    {
        CloseAll();
        Angry.SetActive(true);
    }

    public void SetGameOver()
    {
        CloseAll();
        GameOver.SetActive(true);
    }
}
