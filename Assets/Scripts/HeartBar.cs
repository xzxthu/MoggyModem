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


    public void SetHeart(int nowHeart)
    {
        for(int i = 0; i< hearts.Length;i++)
        {
            if(i<nowHeart)
            {
                hearts[i].SetActive(false);
            }
            else
            {
                hearts[i].SetActive(true);
            }
        }
    }

    public void ResetHeartBar()
    {
        for(int i = 0;i<hearts.Length;i++)
        {
            hearts[i].SetActive(false);
        }
    }

}
