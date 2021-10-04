using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class timerControll : MonoBehaviour
{
    public int percent;
    public GameObject rawUnit;
    public Transform pos;
    //public List<GameObject> rawList = new List<GameObject>();
    public GameObject[] rawList;
    private GameObject obj;
    public GameObject timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rawList = new GameObject[50];
        //pos = this.transform;
        
        obj = rawUnit;
        for(int i = 0; i < 50; i++)
        {
            obj = Instantiate(rawUnit);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y, pos.localPosition.z);
            pos.localPosition = new Vector3(pos.localPosition.x, pos.localPosition.y + 0.07f, pos.localPosition.z);
            obj.SetActive(false);
            rawList[i] = obj;
            
           
        }
        //Destroy(obj);
    }

    // Update is called once per frame
    void Update()
    {
        timerText.GetComponent<Text>().text = percent + "%";
        rawGenerate();
        this.transform.localScale = new Vector3(1.55f, 2.1f, 1);
    }
    void rawGenerate()
    {
        //GameObject obj = rawUnit;
        int rawCount = Mathf.FloorToInt(percent / 2);
        
        for(int i = 0; i < rawCount; i++)
        {
            rawList[i].SetActive(true);           
        }
        for(int i = rawCount; i < 50; i++)
        {
            rawList[i].SetActive(false); 
        }
    }
    
}