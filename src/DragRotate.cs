using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotate : MonoBehaviour
{
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

            float y=(Input.mousePosition.y-start.y)*speed;
            float x=(Input.mousePosition.x-start.x)*sideSpeed;
            transform.RotateAround(transform.position, Camera.main.transform.up, -x); 
            transform.RotateAround(transform.position, Camera.main.transform.right, y); 


            start=Input.mousePosition;
        }

    }
}
