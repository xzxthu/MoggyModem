using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void GoodJobEnd()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
