using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enter Save");

        if (collision.gameObject.tag == "player")
        {
            ShakeManager.Instance.IsSave();
            Debug.Log("In Save");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        ShakeManager.Instance.IsNotSave();

    }
}
