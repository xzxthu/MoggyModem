using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("地块单位长度")]
    public float tileWidth = 0.32f;

    [Header("单边基础地块数量")]
    public int tileNum = 24;

    private Dictionary<int, List<List<GameObject>>> tilesDic = new Dictionary<int,List< List<GameObject>>>(); //<难度, 地块列表>
    private int maxDiff = 0;

    public static TileManager Instance;

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
        LoadTiles();
    }

    private void LoadTiles()
    {
        Object[] loadedTiles = Resources.LoadAll("Prefabs/MediumTile", typeof(GameObject));

        //Debug.Log(loadedTiles.Length);

        for(int i = 0; i<loadedTiles.Length;i++)
        {
            GameObject tile = (GameObject)loadedTiles[i];
            
            int diff = tile.GetComponent<tileInfo>().difficulty;
            maxDiff = Mathf.Max(maxDiff, diff);

            if (!tilesDic.ContainsKey(diff))
            {
                tilesDic.Add(diff,new List<List<GameObject>>());
                tilesDic[diff].Add(new List<GameObject>());
                tilesDic[diff].Add(new List<GameObject>());
            }

            if (tile.GetComponent<tileInfo>().tileType == TileType.LeftToRight)
            {
                //Debug.Log(tilesDic[diff][0].Count);
                tilesDic[diff][0].Add(tile);
            }
            else
            {
                tilesDic[diff][1].Add(tile);
            }

            //tile.transform.parent = transform;
            //tile.transform.localPosition = Vector3.zero;
            //tile.SetActive(false);
        }
        
    }

    public GameObject[] GeneratTiles(int difficulty, bool randomDiff = false)
    {
        GameObject LeftTile;
        GameObject RightTile;

        LeftTile = GetATile(difficulty, true, randomDiff);
        RightTile = GetATile(difficulty, false, randomDiff);

        //LeftTile.SetActive(true);
        //RightTile.SetActive(true);

        LeftTile.transform.parent = transform;
        RightTile.transform.parent = transform;

        LeftTile.transform.localPosition = Vector3.left * tileWidth * tileNum * 0.5f;
        RightTile.transform.localPosition = Vector3.right * tileWidth * tileNum * 0.5f;

        //LeftTile.transform.localScale = new Vector3(1, (Random.Range(0,2)==1?1:-1), 1);
        LeftTile.transform.localScale = Vector3.one;
        RightTile.transform.localScale = Vector3.one;

        //Debug.Log(LeftTile.name);
        //Debug.Log(RightTile.name);

        return new GameObject[] { LeftTile, RightTile };
    }

    private GameObject GetATile(int diff, bool isLeft, bool randomDiff = false)
    {
        int getDiff = diff;

        if(randomDiff)
        {
            int randomSeed = Random.Range(0, 5);
            if (randomSeed==0)
            {
                getDiff = Mathf.Max(getDiff - 1, 0);
            }
            else if(randomSeed==1)
            {
                getDiff = Mathf.Max(getDiff - 2, 0);
            }
            else if(randomSeed == 2)
            {
                getDiff = Mathf.Max(getDiff - 3, 0);

            }
            else if(randomSeed == 3)
            {
                getDiff = Mathf.Min(getDiff + 1, maxDiff);

            }
        }

        int randomIndex = 0;
        int randomUpRight = 0;

        if (isLeft)
        {
            while (tilesDic[getDiff][0].Count==0)
            {
                Debug.Log("No to right");
                getDiff--;
            }

            randomIndex = Random.Range(0, tilesDic[getDiff][randomUpRight].Count); //随机选取一个横的


        }
        else
        {
            randomUpRight = Random.Range(0,2);//随机横或拐

            while((tilesDic[getDiff][0].Count + tilesDic[getDiff][1].Count)==1)//此难度只有一种
            {
                getDiff--;
            }

            while (tilesDic[getDiff][randomUpRight].Count == 0)
            {
                getDiff--;
            }

            randomIndex = Random.Range(0, tilesDic[getDiff][randomUpRight].Count);//随机选取一个
        }

        return Instantiate(tilesDic[getDiff][randomUpRight][randomIndex]);
    }
}
