using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndItem : MonoBehaviour
{
    private bool hasEnd = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if(!hasEnd)
            {
                hasEnd = true;

                Debug.Log("Pass Level");

                LevelManager.Instance.PassALevel();

                PlayerInfo.Instance.ResetCharacter();

                gameObject.SetActive(false);

                StartCoroutine(lateClose());
            }

        }
    }

    private IEnumerator lateClose()
    {
        yield return new WaitForSeconds(0.01f);
        hasEnd = false;
        gameObject.SetActive(false);
    }

}
