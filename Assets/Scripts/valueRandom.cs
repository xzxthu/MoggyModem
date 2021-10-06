using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yuyu;

public class valueRandom : MonoBehaviour
{
    public int maxDifficulty;
    public int currentDifficulty;
    
    void Update()
    {
        //Debug.Log(finalRandom(currentDifficulty));
    }
    public int finalRandom(int index)
    {
        maxDifficulty = this.GetComponent<mapManager>().maxDifficulty;
        currentDifficulty = this.GetComponent<mapManager>().currentDifficulty;
        int value = 0;
        if(index == 0 | index == maxDifficulty)
        {
            value = extremityRandom(index);
        }
        if(index > 0 && index < maxDifficulty)
        {
            value = middleRandom(index);
        }
        return value;
    }
    int extremityRandom(int index)
    {
        float rand = Random.Range(0, 1f);
        int value = 0;
        if(index == 0)
        {
            if(rand < 0.4f)
            {
                value = index + 1;
            }
            if(rand >= 0.4f)
            {
                value = index;
            }
        }
        if(index == maxDifficulty)
        {
            if(rand < 0.4f)
            {
                value = index - 1;
            }
            if(rand >= 0.4f)
            {
                value = index;
            }
        }
        return value;
    }
    int middleRandom(int index)
    {
        float rand = Random.Range(0, 1f);
        int value = 0;
        if(rand < 0.3f)
        {
            value = index - 1;
        }
        if(rand >= 0.3f && rand < 0.7f)
        {
            value = index;
        }
        if(rand >= 0.7f)
        {
            value = index + 1;
        }
        
        return value;
    }
}
