using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMove : MonoBehaviour
{
    public KeyCode left=KeyCode.A;
    public KeyCode right=KeyCode.D;
    public KeyCode forward=KeyCode.W;
    public KeyCode back=KeyCode.S;

    public float speed=5;
    public float sideSpeed=5;

    public bool clampMinY=true;
    public float minY=0.5f;

    public bool clampVertical=false;
    public KeyCode clampVerticalModifier=KeyCode.None;


    void Update()
    {
        
        if(!(Input.GetKey(left)||Input.GetKey(right)||Input.GetKey(forward)||Input.GetKey(back))){
            return;
        }

        Vector3 move=Vector3.zero;


       if(Input.GetKey(left)){
            move-=RightDir()*Time.deltaTime*sideSpeed;
       }else if(Input.GetKey(right)){
           move+=RightDir()*Time.deltaTime*sideSpeed; 
       }

       if(Input.GetKey(forward)){

            move +=Forward()*Time.deltaTime*speed;

       }else if(Input.GetKey(back)){
            move -=Forward()*Time.deltaTime*speed;
       }

       ApplyMove(move);
       Clamp();

    }

    protected virtual void ApplyMove(Vector3 move){
        transform.position+=move;
    }


    protected virtual Vector3 RightDir(){
        return transform.right;
    }

    protected virtual Vector3 ForwardDir(){
        return transform.forward;
    }

    Vector3 Forward(){

        bool clamp=clampVertical;
        if(clampVerticalModifier!=KeyCode.None&&Input.GetKey(clampVerticalModifier)){
            clamp=!clamp;
        }

        if(clamp){
            Vector3 dir=ForwardDir();
            dir.y=0;
            dir=dir.normalized;
            return dir;
        }

        return ForwardDir();
    }


    void Clamp(){

        if(clampMinY){
            Vector3 pos=transform.position;
            pos.y=Mathf.Max(minY, pos.y);
            transform.position=pos;
        }
    }
}
