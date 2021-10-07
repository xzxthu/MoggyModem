using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemManager : BaseManager<ItemManager>
{
    public Dictionary<GameObject, List<MonoBehaviour>> dicItem = new Dictionary<GameObject, List<MonoBehaviour>>();

    /// <summary>
    /// ��õؿ��Ӧ��<>item list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tile"></param>
    /// <returns></returns>
    public List<T> GetItemsList<T>(GameObject tile) where T : MonoBehaviour
    {
        if (dicItem.ContainsKey(tile))
        {
            return dicItem[tile] as List<T>;
        }

        return null;
    }

    /// <summary>
    /// ��������Item�б�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="itemsList"></param>
    /// <param name="tile"></param>
    public List<T> UpadteItems<T>(GameObject tile) where T : MonoBehaviour
    {
        if (!dicItem.ContainsKey(tile)) return null;

        return GetItemsList<T>(tile);
    }
}
