using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Invoke("WaitForStart", 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ItemManager.GetInstance().dicItem.Count);
        //Debug.Log(ItemManager.GetInstance().dicItem[tile].Count);
    }

    public void WaitForStart()
    {
        LevelManager.Instance.StartMenu.SetActive(true);
        MusicManager.Instance.StartMusic();
    }
}
