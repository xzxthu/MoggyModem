using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerControll : MonoBehaviour
{
    public RectTransform r0;
    public RectTransform r1;
    public RectTransform r2;
    public int percent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        float i = percent * 0.96f;
        
        float posx = r0.anchoredPosition.x;
        //Debug.Log(posx);
        float j = posx + i;
        r1.anchoredPosition = new Vector2(j,r1.anchoredPosition.y);
        r2.anchoredPosition = new Vector2(j,r2.anchoredPosition.y);
    }
}
