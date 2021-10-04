using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    

    [Header("一个金币几分")]
    public int CoinScore = 42;

    private List<CoinItem> l_coinItems;
    private List<CoinItem> r_coinItems;

    public static CoinManager Instance;

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

    public void UpdateCoinItems()
    {
        l_coinItems = ItemManager.GetInstance().GetItemsList<CoinItem>(LevelManager.Instance.LeftTile);
        r_coinItems = ItemManager.GetInstance().GetItemsList<CoinItem>(LevelManager.Instance.RightTile);
        
    }

}
