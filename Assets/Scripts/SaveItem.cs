using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveItem : MonoBehaviour
{
    private float distanceToPlayer;
    private bool colli;
    private void Update()
    {
        //if (!colli) return;

        if(Vector2.Distance(PlayerInfo.Instance.player.transform.position,transform.position)<2.26f)
        {
            ShakeManager.Instance.IsSave();
        }
        else if(Vector2.Distance(PlayerInfo.Instance.player.transform.position, transform.position) <3f)
        {
            ShakeManager.Instance.IsNotSave();
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //ShakeManager.Instance.IsSave();
            colli = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //ShakeManager.Instance.IsNotSave();
        colli = false;
    }*/
}
