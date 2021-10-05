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
    
    public GameObject timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rawList = new GameObject[50];
        //pos = this.transform;
        


        for (int i = 0; i < 50; i++)
        {
            GameObject obj = Instantiate<GameObject>(rawUnit) as GameObject;
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
        //if (timerText != null)
        {
            timerText.GetComponent<Text>().text = percent.ToString() + "%";

        }
        rawGenerate();
        this.transform.localScale = new Vector3(1.55f, 2.1f, 1);
    }
    void rawGenerate()
    {
        //GameObject obj = rawUnit;
        int rawCount = Mathf.FloorToInt(percent / 2);


        for (int i = 0; i < rawCount; i++)
        {
            //if (rawList[i] != null)
                rawList[i].SetActive(true);
        }
        for (int i = rawCount; i < 50; i++)
        {

            //if(rawList[i]!=null)
                rawList[i].SetActive(false);
        }
        

        
    }
    
}