using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLook : TouchControl
{

    Vector2 rotation = Vector2.zero;
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

        

        shouldUseTouch=delegate(Touch t){
            return t.position.x>Screen.width/2;
        };


        touchStart=delegate(Touch t){
            rotation=(Vector2) transform.eulerAngles/speed;
        };

        touchMove=delegate(Touch t){


            Vector2 move=t.deltaPosition;

            rotation.y+=move.x;
            rotation.x+=-move.y;


            Vector2 r=(Vector2) rotation * speed;
            r.x=Mathf.Clamp(r.x, -80, 80);

            transform.eulerAngles=r;
        };
    }
    
}
