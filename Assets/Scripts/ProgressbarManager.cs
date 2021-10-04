using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressbarManager : MonoBehaviour
{
    [Header("最初的走一次的时间")]
    public float TimeForOneTurnOrigin = 10f;

    [Header("当前进度")]
    public int ProgressBar = 100;

    private float TimeForOneTurn;
    private int failTimes = 0;
    private bool startBar = false;
    private float timer;

    public static ProgressbarManager Instance;

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
        StopProgressBar();
    }

    private void Update()
    {
        if(startBar)
        {
            timer += Time.deltaTime;
            ProgressBar = (int)(100 * (1-timer / TimeForOneTurn));

            if (timer> TimeForOneTurn) //失败
            {
                
                ResetProgressBar();
                failTimes++;
                timer = TimeForOneTurn * (1f-  1f/ (2f * failTimes));
                ProgressBar = (int)(100 * (1f - timer / TimeForOneTurn));
                PlayerInfo.Instance.DeductHeart();
            }
        }
    }

    public void SetTimeForOneTurn(float time)
    {
        TimeForOneTurn = time;
    }

    public void StartProgressBar()
    {
        startBar = true;
    }

    public void StopProgressBar()
    {
        startBar = false;
        ResetProgressBar();
        SetTimeForOneTurn(TimeForOneTurnOrigin);
    }

    public void ResetProgressBar()
    {
        failTimes = 0;
        timer = 0;
        ProgressBar = 100;
    }

}
