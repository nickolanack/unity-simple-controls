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

   
    void Update()
    {
       
        if(freeLook){
            Cursor.visible = false;
        }else{
            Cursor.visible = false;
        }

        if(freeLook||Input.GetMouseButton(buttonIndex)){


            bool inv=freeLook;

            rotation.y += (inv?-1:1)*(invertX?-1:1)*-Input.GetAxis ("Mouse X");
            rotation.x += (inv?-1:1)*(invertY?-1:1)*Input.GetAxis ("Mouse Y");

            transform.eulerAngles = (Vector2)rotation * speed;
        }
       
    }
}
