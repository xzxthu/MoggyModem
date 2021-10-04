using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{

    public static mapManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public List<GameObject> normalNodes = new List<GameObject>();
    public List<GameObject> bendNodes = new List<GameObject>();
    public List<GameObject> tiles = new List<GameObject>();
    public Transform mapStartPoint;
    public Transform mapEndPoint;
    public int currentDifficulty;
    public int maxDifficulty;
    public GameObject nullobj;
    public int mapStyle;
    private int overTurn;
    void Start()
    {
        
        
        

    }
    private int mapRandom(int dif, bool isNormal)
    {
        int num = this.GetComponent<valueRandom>().finalRandom(dif);
        //Debug.Log(num);
        int value = 0;
        int end = 0;
        if(normalNodes.Count >= bendNodes.Count)
        {
            end = normalNodes.Count;
        }else
        {
            end = bendNodes.Count;
        }
        int[] arr = new int[end];
        int index = 0;
        List<int> RandomList = new List<int>();
        if(isNormal)
        {
            
            for(int i = 0; i < normalNodes.Count; i++)
            {
                if(normalNodes[i].GetComponent<tileInfo>().difficulty == num)
                {
                    arr[index] = i;
                    index ++;
                }
                value = arr[Random.Range(0, end)];
                
            }
        }
        if(!isNormal)
        {
            for(int i = 0; i < bendNodes.Count; i++)
            {
                if(bendNodes[i].GetComponent<tileInfo>().difficulty == num)
                {
                    arr[index] = i;
                    index ++;
                }
                value = arr[Random.Range(0, end)];
                
            }
        }
        return value;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> mapGenerator(int diff)
    {
        currentDifficulty = diff;
        mapStyle = 0;
        mapStart(mapStyle);
        mapEnd();
        return tiles;
    }
    void mapStart(int index)
    {
        GameObject obj = nullobj;
        GameObject obj1 = nullobj;
        if(index == 0)
        {
            obj = normalNodes[mapRandom(currentDifficulty, true)];
            overTurn = Random.Range(0, 1);
            obj1 = Instantiate(obj, this.transform);
            fatherPos(obj1);
            
            if(overTurn == 0)
            {
                obj1.transform.localScale = new Vector3(1f, -1f, 0f);

            }
            obj1.transform.GetComponent<tileInfo>().centrePoint.position = mapStartPoint.position;
            obj1.transform.position = new Vector3(obj1.transform.GetComponent<tileInfo>().centrePoint.position.x + obj1.transform.GetComponent<tileInfo>().transx, obj1.transform.GetComponent<tileInfo>().centrePoint.position.y + obj1.transform.GetComponent<tileInfo>().transy * obj1.transform.localScale.y, 0);
            tiles[0] = obj1;
        }
        if(index == 1 | index == 2)
        {
            obj = bendNodes[mapRandom(currentDifficulty, false)];
            obj1 = Instantiate(obj, this.transform);
            fatherPos(obj1);
            if(index == 1)
            {
                obj1.transform.rotation = Quaternion.Euler(0, 0, 270);
                obj1.transform.GetComponent<tileInfo>().centrePoint.position = mapStartPoint.position;
                obj1.transform.position = new Vector3(obj1.transform.GetComponent<tileInfo>().centrePoint.position.x + obj1.transform.GetComponent<tileInfo>().transx, obj1.transform.GetComponent<tileInfo>().centrePoint.position.y - obj1.transform.GetComponent<tileInfo>().transy, 0);
                tiles[0] = obj1;
            }
            if(index == 2)
            {
                obj1.transform.rotation = Quaternion.Euler(0, 0, 90);
                obj1.transform.localScale = new Vector3(1f, -1f, 0f);
                obj1.transform.GetComponent<tileInfo>().centrePoint.position = mapStartPoint.position;
                obj1.transform.position = new Vector3(obj1.transform.GetComponent<tileInfo>().centrePoint.position.x + obj1.transform.GetComponent<tileInfo>().transx, obj1.transform.GetComponent<tileInfo>().centrePoint.position.y + obj1.transform.GetComponent<tileInfo>().transy, 0);
                tiles[0] = obj1;
            }

        }     
        
    }
    
    void mapEnd()
    {
        GameObject obj = nullobj;
        GameObject obj1 = nullobj;
        int tileStyle = Random.Range(0, 3);
        Debug.Log(tileStyle);
        int overTurn = Random.Range(0, 2);
        if(tileStyle == 0)
        {
            obj = normalNodes[mapRandom(currentDifficulty, true)];
            overTurn = Random.Range(0, 1);
            obj1 = Instantiate(obj, this.transform);
            fatherPos(obj1);
            
            if(overTurn == 1)
            {
                obj1.transform.localScale = new Vector3(1f, -1f, 0f);

            }
        }
        if(tileStyle == 1)
        {
            obj = bendNodes[mapRandom(currentDifficulty, false)];
            obj1 = Instantiate(obj, this.transform);
            fatherPos(obj1);
        }
        if(tileStyle == 2)
        {
            obj = bendNodes[mapRandom(currentDifficulty, false)];
            obj1 = Instantiate(obj, this.transform);
            fatherPos(obj1);
            obj1.transform.localScale = new Vector3(1f, -1f, 0f);
        }
        obj1.transform.GetComponent<tileInfo>().centrePoint.position = mapEndPoint.position;
        obj1.transform.position = new Vector3(obj1.transform.GetComponent<tileInfo>().centrePoint.position.x + obj1.transform.GetComponent<tileInfo>().transx, obj1.transform.GetComponent<tileInfo>().centrePoint.position.y + obj1.transform.GetComponent<tileInfo>().transy * obj1.transform.localScale.y, 0);
        tiles[1] = obj1;
    }
    void fatherPos(GameObject obj)
    {
        Transform father = obj.transform;
        Transform child = father.transform.GetComponent<tileInfo>().centrePoint;
        father.transform.GetComponent<tileInfo>().transx = father.localPosition.x - child.localPosition.x;
        father.transform.GetComponent<tileInfo>().transy = father.localPosition.y - child.localPosition.y;
         
    }
    
}
