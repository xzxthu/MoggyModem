using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEvents : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Instance.StartLevel();
        transform.parent.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void TryAgain()
    {
        MusicManager.Instance.StopAllMusic();
        PlayerInfo.Instance.ResetCharacter();
        LevelManager.Instance.StartLevel();
        transform.parent.gameObject.SetActive(false);
        HeartBar.Instance.ResetHeartBar();
    }
}
