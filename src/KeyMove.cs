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
        



       if(Input.GetKey(left)){
            transform.position-=transform.right*Time.deltaTime*sideSpeed;
       }else if(Input.GetKey(right)){
            transform.position+=transform.right*Time.deltaTime*sideSpeed; 
       }


       if(Input.GetKey(forward)){



            transform.position +=Forward()*Time.deltaTime*speed;
            Clamp();

       }else if(Input.GetKey(back)){
            transform.position -=Forward()*Time.deltaTime*speed;
            Clamp();
       }




    }

    Vector3 Forward(){

        bool clamp=clampVertical;
        if(clampVerticalModifier!=KeyCode.None&&Input.GetKey(clampVerticalModifier)){
            clamp=!clamp;
        }

        if(clamp){
            Vector3 dir=transform.forward;
            dir.y=0;
            dir=dir.normalized;
            return dir;
        }

        return transform.forward;
    }


    void Clamp(){

        if(clampMinY){
            Vector3 pos=transform.position;
            pos.y=Mathf.Max(minY, pos.y);
            transform.position=pos;
        }
    }
}
