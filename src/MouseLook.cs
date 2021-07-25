using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public float speed = 3;
    public int buttonIndex=0;
    public bool freeLook=true;

    public bool invertX=false;
    public bool invertY=false;


    bool once=true;

   
    void OnStart(){

    	//zero initial rotation;
    	

    }

    void Update()
    {
       
    	if(!Input.mousePresent){
			return;
    	}




        if(freeLook){
            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Confined;
        }else{
            Cursor.visible = true;
        }

        if(freeLook||Input.GetMouseButton(buttonIndex)){


            if(once){
                rotation=(Vector2) transform.eulerAngles/speed;
                once=false;
            }

            bool inv=freeLook;


            rotation.y += (inv?-1:1)*(invertX?-1:1)*-Input.GetAxis ("Mouse X");
            rotation.x += (inv?-1:1)*(invertY?-1:1)*Input.GetAxis ("Mouse Y");

            Vector2 r=(Vector2) rotation * speed;
            r.x=Mathf.Clamp(r.x, -80, 80);
            transform.eulerAngles = r;
        }
       
    }
}
