using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovePan : TouchControl
{

    public float speedForward=0.05f;
    public float speedSide=0.05f;


    public bool clampMinY=true;
    public float minY=0;


    void Start()
    {
        
        shouldUseTouch=delegate(Touch t){
            return t.position.x<=Screen.width/2;
        };


        touchMove=delegate(Touch t){


            Vector2 move=t.deltaPosition;
            Vector3 pos=transform.position;

            pos+=transform.forward*move.y*speedForward;
            pos+=transform.right*move.x*speedSide;

            if(clampMinY){
                pos.y=Mathf.Max(minY, pos.y);
            }

            transform.position=pos;

        };

    }


}
