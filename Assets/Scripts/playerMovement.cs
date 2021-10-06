using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;  

public class playerMovement : MonoBehaviour
 {
 
    public float wallSize;
    public bool isDrag;

    public GameObject Hint;

    //[DllImport("user32.dll", EntryPoint = "SetCursorPos")]  
    //private static extern int SetCursorPos(float x, float y);

    private float collidRScale = 3;
    [HideInInspector] public bool isHurting;
    private float timer;
    private float hindTimer;
    private float colliderRidus;
    private Vector3 distanceBtMouseAndBall;
    private Vector3 recordedMousePos;

	// Use this for initialization
	void Start ()
    {
        isDrag = false;
        colliderRidus = GetComponent<CircleCollider2D>().radius;
        GetComponent<CircleCollider2D>().radius = colliderRidus * collidRScale;
        transform.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("Ignore");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(isHurting)
        {
            timer += Time.deltaTime;
            if((int)(timer * 10) % 2 == 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }

            if(timer>1f)
            {
                timer = 0;
                isHurting = false;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if(!isDrag)
        {
            hindTimer += Time.deltaTime;
            if(hindTimer>2f && hindTimer<2.01f)
            {
                Hint.SetActive(true);
            }
        }
    }
    
    void OnMouseDown()
    {
        if (!LevelManager.Instance.hasStart) return;

        isDrag = true;
        GetComponent<CircleCollider2D>().radius = colliderRidus;
        transform.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player");
        recordedMousePos = transform.position;
        Hint.SetActive(false);

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        distanceBtMouseAndBall = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseUp()
    {
        isDrag = false;
        hindTimer = 0;
        GetComponent<CircleCollider2D>().radius = colliderRidus * collidRScale;
        transform.tag = "Untagged";
        gameObject.layer = LayerMask.NameToLayer("Ignore");
    }

    void OnMouseDrag()
    {
        if(isDrag)
        {
            
            if (Vector2.Distance(recordedMousePos, transform.position) > 1f)
                return;

            recordedMousePos = transform.position;
            
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
            transform.position = objectPosition + distanceBtMouseAndBall;

            
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0f);
        var size = wallSize / 2f;
        Vector2 hitPoint = new Vector2(transform.position.x, transform.position.y);
        //Debug.Log(pos);
        //var count = Collision2D.contactCount;
        foreach (ContactPoint2D missileHit in collision.contacts)
        {
                hitPoint = missileHit.point;
                //Debug.Log(hitPoint);
        }
        
        if(collision.gameObject.tag == "wall")
        {
            if (!isHurting)
            {
                isHurting = true;
                PlayerInfo.Instance.DeductHeart();
                hindTimer = 0;
            }

            isDrag = false;

            Vector3 movePos = collision.contacts[0].normal * wallSize;

            transform.position += movePos;

            /*
            if ((pos.x < collision.gameObject.transform.position.x - (wallSize / 2f)))
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
                
            }*/


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

