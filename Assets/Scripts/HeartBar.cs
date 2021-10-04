using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    public GameObject[] hearts;

    public static HeartBar Instance;
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

    public void SetHeartOff(int nowHeart)
    {
        hearts[nowHeart].SetActive(true);
    }

    public void SetHeartOn(int nowHeart)
    {
        hearts[nowHeart-1].SetActive(false);
    }

    public void ResetHeartBar()
    {
        for(int i = 0;i<hearts.Length;i++)
        {
            hearts[i].SetActive(false);
        }
    }

}
