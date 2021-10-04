using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeItem : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enter Mine");

        if (collision.gameObject.tag == "player")
        {

            ShakeManager.Instance.StartShakeProcess();
            gameObject.SetActive(false);
            Debug.Log("In Mine");
            

        }
    }

}

