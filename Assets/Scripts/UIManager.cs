using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: MonoBehaviour
{

    public bool gamePause;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        gamePause = false; // 监测状态，初始化的时候表示没有暂停
        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gamePause == false)
        {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
            
            gamePause = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && gamePause == true)
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
            // 在暂停过程按下空格，恢复原速度
            gamePause = false;
        }
    }
}
