using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "player")
        {
            Debug.Log("Pass Level");

            LevelManager.Instance.PassALevel();

            // 过关动画接口 **

            gameObject.SetActive(false);


        }
    }
}
