using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("�Ѿ�������")]
    public int PassLevels = 0;

    [Header("�ܷ���")]
    public int Score = 0;

    [Header("��һ�صü���")]
    public int PassScore = 111;

    [Header("�����Ѷ�")]
    public int Difficulty = 0;


    public ArtLetter scoreArtLetter;
    public ArtLetter packageArtLetter;

    public GameObject StartMenu;
    public GameObject goodJob;
    public GameObject gameOver;
    

    public bool hasStart = false;

    private static int[] levelUp = new int[]{0,0,1,1,1,2,2,3,3,4,5,3,6,7,8,};

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
    }

    public void AddScore(int addScore)
    {
        Score += addScore;

        scoreArtLetter.showNumber = Score;

        // ���ֽӿ� **
    }

    /// <summary>
    /// ����һ��
    /// </summary>
    public void PassALevel()
    {
        Debug.Log("Enter Pass a level");

        PassLevels++;
        packageArtLetter.showNumber = PassLevels;

        AddScore(PassScore * PassLevels);
        DisableAllItems();
        PlayerInfo.Instance.AddHeart();
        ProgressbarManager.Instance.ResetProgressBar();
        SetDifficulty();
        ShakeManager.Instance.ResetShake();

        Destroy(LeftTile);
        Destroy(RightTile);

        GeneratTiles();

        goodJob.SetActive(true);

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

    private void GeneratTiles()
    {
        Debug.Log("gener");
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
        LeftTile.GetComponent<ItemController>().DisableItems<StartItem>();
        RightTile.GetComponent<ItemController>().DisableItems<StartItem>();
        LeftTile.GetComponent<ItemController>().DisableItems<EndItem>();
        RightTile.GetComponent<ItemController>().DisableItems<EndItem>();
    }

    public void StartLevel()
    {
        
        StopAllCoroutines();

        GeneratTiles();

        LeftTile.GetComponent<ItemController>().EnableItems<StartItem>();
        RightTile.GetComponent<ItemController>().EnableItems<EndItem>();

        hasStart = true;

        LevelEvents(0);

        ProgressbarManager.Instance.StartProgressBar();

        MusicManager.Instance.StartMusic();

        FatAnimationMgr.Instance.SetIdle();
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

        scoreArtLetter.showNumber = 0;
        packageArtLetter.showNumber = 0;
        StartCoroutine(LatePlayFatGameOver());
        CatAnimationMgr.Instance.SetIdle(5);
        

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
        ResetLevel();

        gameOver.SetActive(true);

        Debug.Log("Game Over!");
    }

}
