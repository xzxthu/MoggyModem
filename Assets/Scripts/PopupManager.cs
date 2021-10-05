using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject ShakeAngry;


    public static PopupManager Instance;
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

    public void ShakeWarning()
    {
        ShakeAngry.SetActive(true);
    }

}
