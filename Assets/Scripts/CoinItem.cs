using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Enter Coin");

            MusicManager.Instance.PlaySECoin();

            GetComponent<Animator>().SetTrigger("Coin");

            LevelManager.Instance.AddScore(CoinManager.Instance.CoinScore * (LevelManager.Instance.PassLevels+1));

            //gameObject.SetActive(false);


        }
    }
}
