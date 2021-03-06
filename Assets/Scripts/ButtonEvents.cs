using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Instance.StartLevel();
        transform.parent.gameObject.SetActive(false);
        PlayerInfo.Instance.player.SetActive(true);
        MusicManager.Instance.PlaySEButton();
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
        LevelManager.Instance.scoreArtLetter.SetShowNumber(0);
        LevelManager.Instance.packageArtLetter.SetShowNumber(0);
        LevelManager.Instance.gameOverScore.SetShowNumber(0);
        LevelManager.Instance.scoreArtLetter.StopKeepBlink();
        LevelManager.Instance.packageArtLetter.StopKeepBlink();

        MusicManager.Instance.StartMusic();
        PlayerInfo.Instance.ResetCharacter();
        LevelManager.Instance.StartLevel();
        transform.parent.gameObject.SetActive(false);
        HeartBar.Instance.ResetHeartBar();
        PlayerInfo.Instance.player.SetActive(true);

        MusicManager.Instance.PlaySEButton();

    }

    public void Replay()
    {
        Time.timeScale = 1;
        UIManager.Instance.gamePause = false;
        UIManager.Instance.pauseMenu.SetActive(false);
        CatAnimationMgr.Instance.SetIdle(PlayerInfo.Instance.GetHeart());
        LevelManager.Instance.hasStart = true;
    }
}
