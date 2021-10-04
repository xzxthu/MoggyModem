using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tileInfo : MonoBehaviour
{
    [Header("本地块的难度")]
    public int difficulty;

    [Header("地块类型")]
    public TileType tileType = TileType.LeftToRight;
     
    [Header("")]
    public Transform startPoint;
    [HideInInspector] public Transform startCentrePoint;
    [HideInInspector] public bool isCentreStartPoint;
    [HideInInspector] public bool isCentreEndPoint;
    public Transform centrePoint;
    public Transform endPoint;
    [HideInInspector] public Transform endCentrePoint;
    public float transx;
    public float transy;

    // Start is called before the first frame update
    void Awake()
    {
        //transx = transform.position.x - centrePoint.position.x;
        //transy = transform.position.y - centrePoint.position.y;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum TileType
{
    LeftToRight,
    LeftToUp,
}
