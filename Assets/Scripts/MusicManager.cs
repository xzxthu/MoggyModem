using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] hearts;

    //public AudioClip oneHeart;
    //public AudioClip twoHeart;
    //public AudioClip threeHeart;
    //public AudioClip fourHeart;
    //public AudioClip fiveHeart;

    public AudioSource[] ads;

    public AudioSource se;

    public AudioClip[] hurts;
    public AudioClip win;
    public AudioClip coin;
    public AudioClip breakScreen;
    public AudioClip electron;
    public AudioClip button;

    private float[] musicVolume = new float[] { 0.5f, 0.5f,0.75f,1,1 };

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

    public void StartMusic(bool cover = false)
    {
        for (int i = 0; i < 5; i++)
        {
            if (!cover)
            {
                ads[i].clip = hearts[i];
                ads[i].Play(); 
            }
            ads[i].volume = 0;
            ads[i].loop = true;
        }

        ads[4].volume = musicVolume[4];
    }



    public void StopAllMusic()
    {
        for (int i = 0; i < 5; i++)
        {
            ads[i].Stop();
        }
    }


    public void SetMusic(int nowHeart)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < nowHeart-1)
            {
                ads[i].volume = 0;
            }
            else
            {
                ads[i].volume = musicVolume[i];
            }
        }
    }

    public void PlaySEHurt()
    {
        int index = Random.Range(0,hurts.Length);
        PlaySE(hurts[index]);
    }

    private void PlaySE(AudioClip clip)
    {
        se.clip = clip;
        se.Play();
        se.loop = false;
    }

    public void PlaySECoin()
    {
        PlaySE(coin);
    }

    public void PlaySEBreakScreen()
    {
        PlaySE(breakScreen);
    }

    public void PlaySEElectron()
    {
        PlaySE(electron);
        
    }

    public void PlaySEButton()
    {
        PlaySE(button);
    }
}
