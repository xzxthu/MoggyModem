using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void Awake()
    {
        FindChildren<ShakeItem>();
        FindChildren<SaveItem>();
        //FindChildren<LockItem>();
        FindChildren<CoinItem>();
        //FindChildren<ShieldItem>();
        FindChildren<StartItem>();
        FindChildren<EndItem>();


        DisableItems<ShakeItem>();
        DisableItems<SaveItem>();
        //DisableItems<LockItem>();
        DisableItems<CoinItem>();
        //DisableItems<ShieldItem>();
        DisableItems<StartItem>();
        DisableItems<EndItem>();
    }
    private void FindChildren<T>() where T : MonoBehaviour
    {
        T[] items = this.GetComponentsInChildren<T>();
        if (items == null) return;

        for (int i = 0; i < items.Length; ++i)
        {

            if (ItemManager.GetInstance().dicItem.ContainsKey(transform.gameObject))
            {
                ItemManager.GetInstance().dicItem[transform.gameObject].Add(items[i]); // [tile(µØ×©), item(ÕðÆÁµØÀ×¡¢½ð±Ò...)]
            }
            else
            {
                ItemManager.GetInstance().dicItem.Add(transform.gameObject, new List<MonoBehaviour>() { items[i] });
            }
        }
    }

    public void DisableItems<T>() where T : MonoBehaviour
    {
        for (int i = 0; i < ItemManager.GetInstance().dicItem[transform.gameObject].Count; ++i)
        {
            if (ItemManager.GetInstance().dicItem[transform.gameObject][i] is T)
            {
                ItemManager.GetInstance().dicItem[transform.gameObject][i].gameObject.SetActive(false);
            }
        }
        
    }

    public void EnableItems<T>() where T : MonoBehaviour
    {
        for (int i = 0; i < ItemManager.GetInstance().dicItem[transform.gameObject].Count; ++i)
        {
            if (ItemManager.GetInstance().dicItem[transform.gameObject][i] is T)
            {
                ItemManager.GetInstance().dicItem[transform.gameObject][i].gameObject.SetActive(true);
            }
        }
    }

    public void SetStartPoint()
    {
        EnableItems<StartItem>();
    }

    public void SetEndPoint()
    {
        EnableItems<EndItem>();
    }
}
