using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndItem : MonoBehaviour
{
    private bool hasEnd = false;
    private float timer = 0;

    private void Update()
    {
        if(hasEnd)
        {
            timer += Time.deltaTime;
            if(timer>0.1)
            {
                hasEnd = false;
                gameObject.SetActive(false);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if(!hasEnd)
            {
                hasEnd = true;

                //Debug.Log("Pass Level");

                LevelManager.Instance.PassALevel();

                PlayerInfo.Instance.ResetCharacter();


            }

        }
    }


}
