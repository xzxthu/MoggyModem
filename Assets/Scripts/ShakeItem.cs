using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeItem : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            ShakeManager.Instance.StartShakeProcess();
            gameObject.SetActive(false);

            //Debug.Log("In Mine");
            

        }
    }

}

