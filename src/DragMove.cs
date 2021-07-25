using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{


    /**
     * this should work with touch drag and mouse drag
     */
    

    public int mouseButton=0;
    Vector2 start = Vector2.zero;
    public float speed=0.1f;
    public float sideSpeed=0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


        if(Input.GetMouseButtonDown(mouseButton)){
            start=Input.mousePosition;
        }
        if(Input.GetMouseButton(mouseButton)){
            transform.position+=(Input.mousePosition.y-start.y)*Vector3.up*speed;
            transform.position+=(Input.mousePosition.x-start.x)*Camera.main.transform.right*sideSpeed;
            start=Input.mousePosition;
        }

    }
}
