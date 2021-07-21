using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : TouchControl
{

    public float speedForward=0.05f;
    public float speedSide=0.05f;

    void Start()
    {
        
        shouldUseTouch=delegate(Touch t){
            return t.position.x<=Screen.width/2;
        };


        touchMove=delegate(Touch t){


            Vector2 move=t.deltaPosition;

            transform.position+=transform.forward*move.y*speedForward;
            transform.position+=transform.right*move.x*speedSide;

        };

    }


}
