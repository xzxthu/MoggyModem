using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ItemManager.GetInstance().dicItem.Count);
        //Debug.Log(ItemManager.GetInstance().dicItem[tile].Count);
    }
}
