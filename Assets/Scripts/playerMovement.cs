using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;  

public class playerMovement : MonoBehaviour
 {
 
    public float wallSize;
    public bool isDrag;
    //[DllImport("user32.dll", EntryPoint = "SetCursorPos")]  
    //private static extern int SetCursorPos(float x, float y);

    
	// Use this for initialization
	void Start ()
    {
        isDrag = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
    }
    
    void OnMouseDown()
    {
        isDrag = true;
    }
    void OnMouseUp()
    {
        isDrag = false;
    }

    void OnMouseDrag()
    {
        if(isDrag)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
            transform.position = objectPosition;
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0f);
        var size = wallSize / 2f;
        Vector2 hitPoint = new Vector2(transform.position.x, transform.position.y);
        Debug.Log(pos);
        //var count = Collision2D.contactCount;
        foreach (ContactPoint2D missileHit in collision.contacts)
        {
                hitPoint = missileHit.point;
                Debug.Log(hitPoint);
        }
        
        if(collision.gameObject.tag == "wall")
        {
            isDrag = false;
            if((pos.x < collision.gameObject.transform.position.x - (wallSize / 2f)))
            {
                transform.position = new Vector3(pos.x - (size / 4f), transform.position.y, 0f);
                
            }

            if(pos.x > collision.gameObject.transform.position.x + (wallSize / 2f))
            {
                transform.position = new Vector3(pos.x + (size / 4f), transform.position.y, 0f);
                
            }
            if(pos.y < collision.gameObject.transform.position.y - (wallSize / 2f))
            {
                transform.position = new Vector3(transform.position.x, pos.y - (size / 4f), 0f);
                
            }
            if(pos.y > collision.gameObject.transform.position.y + (wallSize / 2f))
            {
                transform.position = new Vector3(transform.position.x, pos.y + (size / 4f), 0f);
                
            }
            
        }
        if(collision.gameObject.tag == "obliqueWall")
        {
            isDrag = false;
            
            //transform.position = new Vector3(pos.x + ((collision.gameObject.transform.Localscale.x * -1f) * (size / 4f)), pos.y + ((collision.gameObject.transform.Localscale.y) * (size / 4f)), 0f);
            
            
            
            if((pos.x < hitPoint.x) && (pos.y < hitPoint.y))
            {
                transform.position = new Vector3(pos.x - (size / 2f), pos.y - (size / 2f), 0f);
            }
            if((pos.x < hitPoint.x) && (pos.y > hitPoint.y))
            {
                transform.position = new Vector3(pos.x - (size / 2f), pos.y + (size / 2f), 0f);
            }
            if((pos.x > hitPoint.x) && (pos.y < hitPoint.y))
            {
                transform.position = new Vector3(pos.x + (size / 2f), pos.y - (size / 2f), 0f);
            }
            if((pos.x > hitPoint.x) && (pos.y > hitPoint.y))
            {
                transform.position = new Vector3(pos.x + (size / 2f), pos.y + (size / 2f), 0f);
            }
        }
    }
}

