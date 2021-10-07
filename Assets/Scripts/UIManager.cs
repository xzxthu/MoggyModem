using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: MonoBehaviour
{

    public bool gamePause;
    public GameObject pauseMenu;

    public static UIManager Instance;

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

    // Start is called before the first frame update
    void Start()
    {
        gamePause = false; // 监测状态，初始化的时候表示没有暂停
        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Escape) && gamePause == false))
        {
            Debug.Log("按了暂停" + LevelManager.Instance.hasStart);
            if(LevelManager.Instance.hasStart)
            {
                Time.timeScale = 0;
                pauseMenu.gameObject.SetActive(true);
                CatAnimationMgr.Instance.SetGameOver();
                LevelManager.Instance.hasStart = false;
                gamePause = true;
            }
            
        }
        /*else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) && gamePause == true))
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
            CatAnimationMgr.Instance.SetIdle(PlayerInfo.Instance.GetHeart());
            // 在暂停过程按下空格，恢复原速度
            LevelManager.Instance.hasStart = true;
            gamePause = false;
        }*/
    }
}
