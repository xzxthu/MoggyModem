using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance;
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


    public AudioClip[] hearts;

    //public AudioClip oneHeart;
    //public AudioClip twoHeart;
    //public AudioClip threeHeart;
    //public AudioClip fourHeart;
    //public AudioClip fiveHeart;

    public AudioSource[] ads;

    public float Volume = 1;

    public void StartMusic()
    {
        for(int i = 0; i<5;i++)
        {
            ads[i].clip = hearts[i];
            ads[i].Play();
            ads[i].volume = 0;
            ads[i].loop = true;
        }

        ads[4].volume = Volume;
    }

    public void StopAllMusic()
    {
        for (int i = 0; i < 5; i++)
        {
            ads[i].Stop();
        }
    }

    public void PlayAddMew(int nowHeart)
    {
        
         ads[nowHeart].volume = Volume;
        
    }

    public void StopAddMew(int nowHeart)
    {
        ads[nowHeart-1].volume = 0;
    }

    public void SetVolume(float newV)
    {
        Volume = newV;
    }
}
