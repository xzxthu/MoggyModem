
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
//实现效果：图片指定任意不规则区域显示
//如何实现芒星图或者战力图
//先画出显示范围，然后以此为遮罩显示需要显示的部分
 
//延申：图片指定不规则区域透明  
//逆向思维  不规则区域透明其实就是不规则区域显示
//先画出显示范围，然后以此为遮罩显示需要显示的部分
public class timerHorizontal : Graphic
{
    public RectTransform r0;//中心点
    public RectTransform r1;//可调节点
    public RectTransform r2;//可调节点
    public RectTransform r3;//可调节点
    public RectTransform r4;//可调节点
    public RectTransform r5;//可调节点
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (r1)
        {
            vh.Clear();
            //可任意添加顶点
            vh.AddVert(r0.anchoredPosition, new Color(1,1,1,1/255f), Vector2.zero);
            vh.AddVert(r1.anchoredPosition, new Color(1, 0, 1, 1 / 255f), Vector2.zero);
            vh.AddVert(r2.anchoredPosition, new Color(0, 1, 1, 1 / 255f), Vector2.zero);
            vh.AddVert(r3.anchoredPosition, new Color(1, 1, 1, 1 / 255f), Vector2.zero);
            vh.AddVert(r4.anchoredPosition, new Color(1, 1, 0, 1 / 255f), Vector2.zero);
            vh.AddVert(r5.anchoredPosition, new Color(0, 0, 1, 1 / 255f), Vector2.zero);
            //可任意绘制不规则区域
            vh.AddTriangle(3, 4, 5);
            vh.AddTriangle(2, 3, 4);

        }
        
    }
 
    private void Update()
    {
        SetAllDirty();
    }
}