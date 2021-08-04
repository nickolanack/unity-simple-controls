using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : TouchControl
{
    public float speedForward=0.05f;
    public float speedSide=0.05f;


    public bool clampMinY=true;
    public float minY=0;

    Vector2 move=Vector2.zero;
    Vector2 start=Vector2.zero;

    public bool clampVertical=true;

    
    /**
     * prevent forward and sideways speed from adding so that the velocity is greater than a maximum forward speed with no sideways motion 
     * ie: turning/running on an angle should not increase the maximum speed
     * @type {Boolean}
     */
    public bool normalize=true;


    void Start()
    {
        
        shouldUseTouch=delegate(Touch t){
            Debug.Log(t.position.x<=Screen.width/2?"use touchMove":"dont use touchMove");
        
            return t.position.x<=Screen.width/2;
        };

        touchStart=delegate(Touch t){
            start=t.position;
        };

        touchMove=delegate(Touch t){
           
            move=t.position-start;
        };
        touchEnd=delegate(Touch t){
             move=Vector2.zero;
        };

    }

    protected override void Update(){

        base.Update();

        if(Mathf.Abs(move.x)<0.01&&Mathf.Abs(move.y)<0.01){
            return;
        }   
            
        Vector3 pos=transform.position;

        Vector3 forwardDirection=transform.forward;
        if(clampVertical){
            forwardDirection.y=0;
            forwardDirection=forwardDirection.normalized;
        }

        Vector3 forward=CalcForward(forwardDirection, move.y)*speedForward;
        Vector3 side=CalcSide(transform.right, move.x)*speedSide;

        if(normalize){
             Vector2 normalized=move.normalized; //ensures that forward + strafe doesn't allow a maximum velocity greater than forward only
             
             //Note: without Abs, this would change the directions of backward and left motion
             forward*=Mathf.Abs(normalized.y);
             side*=Mathf.Abs(normalized.x);
        }

        pos+=(forward+side)*Time.deltaTime;

        if(clampMinY){
            pos.y=Mathf.Max(minY, pos.y);
        }

        transform.position=pos;


    }

    protected virtual Vector3 CalcForward(Vector3 direction, float touchYDelta){
        return direction*touchYDelta;
    }

    protected virtual Vector3 CalcSide(Vector3 direction, float touchXDelta){
       return direction*touchXDelta;
    }




}
