using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yuyu;

public class LevelManager : MonoBehaviour
{

    [Header("Number of Passed Levels")]
    public int PassLevels = 0;

    [Header("Score")]
    public int Score = 0;

    [Header("Score obtaining for passing a level")]
    public int PassScore = 111;

    [Header("now difficulty")]
    public int Difficulty = 0;


    public ArtLetter scoreArtLetter;
    public ArtLetter packageArtLetter;
    public ArtLetter gameOverScore;

    public GameObject StartMenu;
    public GameObject goodJob;
    public GameObject gameOver;
    

    public bool hasStart = false;

    private static int[] levelUp = new int[]{0,2,1,3,1,2,2,3,3,4,5,3,6,7,8,9,10,11};

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
        //ResetLevel();
        gameOver.SetActive(false);
    }

    public void AddScore(int addScore)
    {
        Score += addScore;
        scoreArtLetter.SetShowNumberWithEffect(Score);
        scoreArtLetter.Blink();
    }

    /// <summary>
    /// 过关
    /// </summary>
    public void PassALevel()
    {
        Debug.Log("Enter Pass a level");

        //StopAllCoroutines();

        PassLevels++;

        packageArtLetter.SetShowNumber(PassLevels);
        packageArtLetter.Blink();
        AddScore(PassScore * PassLevels);
        goodJob.SetActive(true);
        
        DisableAllItems();

        PlayerInfo.Instance.AddHeart();
        ProgressbarManager.Instance.ResetProgressBar();
        ShakeManager.Instance.ResetShake();
        MusicManager.Instance.PlaySEElectron();

        Destroy(LeftTile);
        Destroy(RightTile);

        SetDifficulty();

        GeneratTiles(Difficulty, true);

        ShakeManager.Instance.UpdateShakeItems();
        CoinManager.Instance.UpdateCoinItems();

        LeftTile.GetComponent<ItemController>().EnableItems<StartItem>();
        StartCoroutine(LateTurnOnEndItem());

        LevelEvents(PassLevels);
    }

    private IEnumerator LateTurnOnEndItem()
    {
        yield return new WaitForSeconds(0.01f);
        RightTile.GetComponent<ItemController>().EnableItems<EndItem>();

    }

    private void GeneratTiles(int diff, bool random = false)
    {
        //Debug.Log("gener");
        GameObject[] tiles = TileManager.Instance.GeneratTiles(diff, random);
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
        LeftTile.GetComponent<ItemController>().DisableItems<StartItem>();
        RightTile.GetComponent<ItemController>().DisableItems<StartItem>();
        LeftTile.GetComponent<ItemController>().DisableItems<EndItem>();
        RightTile.GetComponent<ItemController>().DisableItems<EndItem>();
    }

    public void StartLevel()
    {
        
        StopAllCoroutines();

        GeneratTiles(0);

        LeftTile.GetComponent<ItemController>().EnableItems<StartItem>();
        RightTile.GetComponent<ItemController>().EnableItems<EndItem>();

        hasStart = true;

        LevelEvents(0);

        ProgressbarManager.Instance.StartProgressBar();

        MusicManager.Instance.StartMusic(true);

        FatAnimationMgr.Instance.SetIdle();

        CatAnimationMgr.Instance.SetIdle(5);

        UIManager.Instance.pauseMenu.SetActive(false);

    }

    /// <summary>
    /// �������йؿ�
    /// </summary>
    public void ResetLevel()
    {
        StopAllCoroutines();
        

        ShakeManager.Instance.ResetShake();
        DisableAllItems();
        ProgressbarManager.Instance.StopProgressBar();

        PassLevels = 0;
        Score = 0;
        Difficulty = 0;

        
        Destroy(LeftTile);
        Destroy(RightTile);


        //scoreArtLetter.showNumber = 0;
        //packageArtLetter.showNumber = 0;
        StartCoroutine(LatePlayFatGameOver());
        

        hasStart = false;
        //MusicManager.Instance.StopAllMusic();
    }
    
    private IEnumerator LatePlayFatGameOver()
    {
        yield return new WaitForSeconds(1f);
        FatAnimationMgr.Instance.SetGameOver();
    }

    public void GameOver()
    {
        gameOver.SetActive(true); //Game Over Object
        gameOverScore.SetShowNumberWithEffect(Score);

        ResetLevel();

        PlayerInfo.Instance.player.SetActive(false);

        scoreArtLetter.StartKeepBlink();
        packageArtLetter.StartKeepBlink();
        
        CatAnimationMgr.Instance.SetGameOver();
        
    }


}
