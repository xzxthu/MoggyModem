using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void GoodJobEnd()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void FixingEnd()
    {
        CatAnimationMgr.Instance.SetIdle(PlayerInfo.Instance.GetHeart());
    }

    public void CloseGameObject()
    {
        gameObject.SetActive(false);
    }

    public void FatEnd()
    {
        FatAnimationMgr.Instance.SetIdle();
    }
}
