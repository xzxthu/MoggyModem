using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtLetter : MonoBehaviour
{
    public Texture[] letters;
    public int num = 6;
    [Range(0,1)] public float lettersSize = 1;
    public float wordSpace;
    public int showNumber;
    
    private RawImage[] images;
    private float localScaleX = 0.47f;

    private void Start()
    {
        InitialNumbers();
    }

    private void Update()
    {
        SetLetters(lettersSize, wordSpace);
        UpdateShowLetters();
    }

    private void InitialNumbers()
    {
        
        images = new RawImage[num];
        for(int i = 0;i<num;i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/ArtLetter"));
            images[i] = obj.GetComponent<RawImage>();
            obj.transform.SetParent(transform);
            obj.GetComponent<RectTransform>().localPosition = Vector3.left * (wordSpace) * i;
            obj.GetComponent<RectTransform>().localScale = new Vector3(localScaleX * lettersSize, lettersSize, 1);
        }
    }

    /// <summary>
    /// 更新显示的数字
    /// </summary>
    public void UpdateShowLetters()
    {
        for (int i = 0; i < num; i++)
        {
            images[i].texture = letters[Mathf.FloorToInt((showNumber / Mathf.Pow(10, i))) % 10];
        }
    }

    /// <summary>
    /// 设置字间距和大小
    /// </summary>
    /// <param name="size"></param>
    /// <param name="space"></param>
    public void SetLetters(float size, float space)
    {
        for (int i = 0; i < num; i++)
        {
            images[i].gameObject.GetComponent<RectTransform>().localPosition = Vector3.left * (wordSpace) * i;
            images[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(localScaleX * lettersSize, lettersSize, 1);
        }
    }    
}


