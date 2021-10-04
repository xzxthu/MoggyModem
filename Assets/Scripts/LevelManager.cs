using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("已经过关数")]
    public int PassLevels = 0;

    [Header("总分数")]
    public int Score = 0;

    [Header("过一关得几分")]
    public int PassScore = 111;

    [Header("现在难度")]
    public int Difficulty = 0;

    private static int[] levelUp = new int[]{0,0,1,1,1,2,3,4,5,6,7,8,9,10};

    [HideInInspector] public GameObject LeftTile;
    [HideInInspector] public GameObject RightTile;


    public static LevelManager Instance;

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
        ResetLevel();
    }

    public void AddScore(int addScore)
    {
        Score += addScore;

        //调用艺术字 **
        // 音乐接口 **
    }

    /// <summary>
    /// 过了一关
    /// </summary>
    public void PassALevel()
    {
        PassLevels++ ;
        AddScore(PassScore * PassLevels);
        DisableAllItems();
        PlayerInfo.Instance.AddHeart();
        ProgressbarManager.Instance.ResetProgressBar();
        SetDifficulty();

        GeneratTiles();

        // 过关动画接口 **

        ShakeManager.Instance.UpdateShakeItems();
        CoinManager.Instance.UpdateCoinItems();

        LeftTile.GetComponent<ItemController>().EnableItems<StartItem>();
        RightTile.GetComponent<ItemController>().EnableItems<EndItem>();

        LevelEvents(PassLevels);
    }

    private void GeneratTiles()
    {
        List<GameObject> tiles = mapManager.Instance.mapGenerator(Difficulty);
        LeftTile = tiles[0];
        RightTile = tiles[1];
    }

    private void SetDifficulty()
    {
        if(PassLevels < levelUp.Length)
        {
            Difficulty = levelUp[PassLevels];
        }
    }

    private void LevelEvents(int nowLevel)
    {
        switch(nowLevel)
        {
            case 0:
                Level_0_Event();
                break;
            case 1:
                Level_0_Event();
                break;
            case 2:
                Level_2_Event();
                break;
            default:
                Level_Default_Event();
                break;
        }
    }

    private void Level_0_Event()
    {
        LeftTile.GetComponent<ItemController>().EnableItems<CoinItem>();
        RightTile.GetComponent<ItemController>().EnableItems<CoinItem>();
    }

    private void Level_2_Event()
    {
        LeftTile.GetComponent<ItemController>().EnableItems<ShakeItem>();
        RightTile.GetComponent<ItemController>().EnableItems<ShakeItem>();
    }

    private void Level_Default_Event()
    {
        int r1 = Random.Range(0, 10);
        int r2 = Random.Range(0, 10);
        if (r1 > 7) Level_0_Event();
        if (r2 > 7) Level_2_Event();
    }

    private void DisableAllItems()
    {
        LeftTile.GetComponent<ItemController>().DisableItems<ShakeItem>();
        RightTile.GetComponent<ItemController>().DisableItems<ShakeItem>();
        LeftTile.GetComponent<ItemController>().DisableItems<SaveItem>();
        RightTile.GetComponent<ItemController>().DisableItems<SaveItem>();
        LeftTile.GetComponent<ItemController>().DisableItems<CoinItem>();
        RightTile.GetComponent<ItemController>().DisableItems<CoinItem>();
    }

    public void StartLevel()
    {
        GeneratTiles();

        LeftTile.GetComponent<ItemController>().EnableItems<StartItem>();
        RightTile.GetComponent<ItemController>().EnableItems<EndItem>();

        LevelEvents(0);

    }

    /// <summary>
    /// 重置所有关卡
    /// </summary>
    public void ResetLevel()
    {
        ShakeManager.Instance.ResetShake();
        DisableAllItems();
        ProgressbarManager.Instance.StopProgressBar();
        PassLevels = 0;
        Score = 0;
        Difficulty = 0;
    }
    

    public void GameOver()
    {
        ResetLevel();
        Debug.Log("Game Over!");
    }

}
