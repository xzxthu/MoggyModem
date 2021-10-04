using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    public static mapManager instance;
    public List<GameObject> endNodes = new List<GameObject>();
    public List<GameObject> normalNodes = new List<GameObject>();
    public List<GameObject> bendNodes = new List<GameObject>();
    public List<GameObject> tiles = new List<GameObject>();
    public List<Transform> startPoints = new List<Transform>();
    public List<Transform> endPoints = new List<Transform>();
    public List<Transform> CentrePoints = new List<Transform>();
    public List<bool> nextIsCentre = new List<bool>();
    public List<int> tileTypes = new List<int>();
    public GameObject mapStartPoint;
    public int nodeLength;
    //private Transform startPoint;
    //private Transform endPoint;
    //private Transform CentrePoint;
    //private bool nextIsCentre;
    public int mapStyle;
    private int overTurn;
    //private Transform endPos;
    // Start is called before the first frame update
    void Start()
    {
        startPoints[0] = mapStartPoint.transform;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        //mapStyle = Random.Range(0,3);
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("start");
            mapStart();
            //mapConnect();
        }
    }
    private void mapStart()
    {
        
        var startTile = endNodes[Random.Range(0, endNodes.Count)];
        var obj = Instantiate(startTile);
        tiles[0] = obj;
        
        obj.transform.GetComponent<tileInfo>().startPoint.position = startPoints[0].transform.position;
        overTurn = Random.Range(0, 1);
        
        if(overTurn == 0)
        {
            obj.transform.localScale = new Vector3(1f, -1f, 0f);
            
        }
        startPoints[0] = obj.transform.GetComponent<tileInfo>().endPoint;
        CentrePoints[0] = obj.transform.GetComponent<tileInfo>().endCentrePoint;
        nextIsCentre[0] = obj.transform.GetComponent<tileInfo>().isCentreEndPoint;
        
    }
    void mapConnect()
    {
        if(mapStyle == 0)
        {
            mapStart();
            normalNode(1, 0);
            bendNode(2, 0, false);
            bendNode(3, 90, false);
            normalNode(4, 180);
            bendNode(5, 180, true);
            bendNode(6, 90, true);
            normalNode(7, 0);
            endNode(180);
        }
        if(mapStyle == 1)
        {
            mapStart();
            bendNode(1, 0, false);
            bendNode(2, 90, false);
            bendNode(3, 180, true);
            bendNode(4, 90, true);
            normalNode(5, 0);
            bendNode(6, 0, true);
            normalNode(7, 270);
            endNode(90);
        }
        if(mapStyle == 2)
        {
            mapStart();
            normalNode(1, 0);
            bendNode(2, 0, false);
            normalNode(3, 90);
            bendNode(4, 90, false);
            bendNode(5, 180, false);
            bendNode(6, 270, true);
            bendNode(7, 180, true);
           endNode(270);
        }
    }
    private void endNode(int direction)
    {
        var endTile = endNodes[Random.Range(0, endNodes.Count)];
        var obj1 = Instantiate(endTile);
        if(nextIsCentre[7])
        {
            tiles[8] = endTile;
            obj1 = Instantiate(endTile);
            obj1.transform.rotation = Quaternion.Euler(0, 0, direction);
            obj1.transform.GetComponent<tileInfo>().startPoint.position = startPoints[7].position;
        }
        if(!nextIsCentre[7])
        {
            tiles[8] = endTile;
            obj1 = Instantiate(endTile);
            obj1.transform.rotation = Quaternion.Euler(0, 0, direction);
            obj1.transform.localScale = new Vector3(1f, -1f, 0f);
            obj1.transform.GetComponent<tileInfo>().startCentrePoint.position = CentrePoints[7].transform.position;
        }       
    }
    private void normalNode(int i, int direction)
    {
        var obj = normalNodes[Random.Range(0, normalNodes.Count)];
        while(obj.transform.GetComponent<tileInfo>().isCentreStartPoint != nextIsCentre[i - 1])
        {
            obj = normalNodes[Random.Range(0, normalNodes.Count)];
        }
        if(nextIsCentre[i - 1])
        {
            var obj1 = Instantiate(obj);
            obj1.transform.GetComponent<tileInfo>().startCentrePoint.position = CentrePoints[i - 1].transform.position;
            obj1.transform.rotation = Quaternion.Euler(0, 0, direction);
            var overTurn = Random.Range(0, 1);
            if(overTurn == 0)
            {
                obj1.transform.localScale = new Vector3(1f, -1f, 0f);
                
            }
            //if(obj.GetComponet<tileInfo>().endPoint.transform.Position.y)
            startPoints[i] = obj1.transform.GetComponent<tileInfo>().endPoint;
            CentrePoints[i] = obj1.transform.GetComponent<tileInfo>().endCentrePoint;
            
            nextIsCentre[i] = obj1.transform.GetComponent<tileInfo>().isCentreEndPoint;
            tiles[i] = obj1;
                
        }
        if(!nextIsCentre[i - 1])
        {
            var obj1 = Instantiate(obj);
            obj1.transform.GetComponent<tileInfo>().startCentrePoint.position = CentrePoints[i - 1].transform.position;
            
            obj1.transform.rotation = Quaternion.Euler(0, 0, direction);
            
            if(obj1.transform.GetComponent<tileInfo>().startPoint == endPoints[i - 1])
            {
                startPoints[i] = obj1.transform.GetComponent<tileInfo>().endPoint;
                CentrePoints[i] = obj1.transform.GetComponent<tileInfo>().endCentrePoint;
            
                nextIsCentre[i] = obj1.transform.GetComponent<tileInfo>().isCentreEndPoint;
                tiles[i] = obj1;
            }
            if(obj1.transform.GetComponent<tileInfo>().startPoint != endPoints[i - 1])
            {
                obj1.transform.localScale = new Vector3(1f, -1f, 0f);
                startPoints[i] = obj1.transform.GetComponent<tileInfo>().endPoint;
                CentrePoints[i] = obj1.transform.GetComponent<tileInfo>().endCentrePoint;
            
                nextIsCentre[i] = obj1.transform.GetComponent<tileInfo>().isCentreEndPoint;
                tiles[i] = obj1;
            }
        } 
    }
    private void bendNode(int i, int direction, bool isTurn)
    {
        var obj = bendNodes[Random.Range(0, bendNodes.Count)];
        var obj1 = Instantiate(obj);
        var index = i - 1;
        do
        {
            obj = bendNodes[Random.Range(0, bendNodes.Count)];
            obj1 = Instantiate(obj);
            if(isTurn)
            {
                obj1.transform.localScale = new Vector3(1f, -1f, 0f);
            }
            obj1.transform.rotation = Quaternion.Euler(0, 0, direction);
            
            obj1.transform.GetComponent<tileInfo>().startCentrePoint.position = CentrePoints[index].transform.position;
            
            

        }while(obj1.transform.GetComponent<tileInfo>().startPoint == endPoints[index]);
        startPoints[i] = obj1.transform.GetComponent<tileInfo>().endPoint;
        CentrePoints[i] = obj1.transform.GetComponent<tileInfo>().endCentrePoint;
            
        nextIsCentre[i] = obj1.transform.GetComponent<tileInfo>().isCentreEndPoint;
        tiles[i] = obj1;
    }
}
