using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragRotate : MonoBehaviour
{
    public int mouseButton=0;
    Vector2 start = Vector2.zero;
    public float speed=0.1f;
    public float sideSpeed=0.1f;

    Quaternion rotation=Quaternion.identity;


    public delegate void RotateEvent(Quaternion rotation);
   


    public RotateEvent onRotateStart;

    public RotateEvent onRotate; //called with unprocessed target position
    public RotateEvent afterRotate; //called after move, with applied position (may be different that onDrag position)

    public RotateEvent onRotateEnd;

    public bool ignoreUI=true;
  
    void Update()
    {
        
        if(ignoreUI&&EventSystem.current.currentSelectedGameObject!=null){
            return;
        }


        if(Input.GetMouseButtonDown(mouseButton)){
            start=Input.mousePosition;
            rotation=CurrentRotation();


            if(onRotateStart!=null){
                onRotateStart(CurrentRotation());
            }
        }
        if(Input.GetMouseButton(mouseButton)){

            float y=(Input.mousePosition.y-start.y)*speed;
            float x=(Input.mousePosition.x-start.x)*sideSpeed;


            Quaternion rotate=rotation*Quaternion.Inverse(rotation)*RotateX(-x)*RotateY(y)*rotation;

            if(onRotate!=null){
                onRotate(rotate);
            }

            Quaternion rotationAfter=ApplyRotation(rotate);
            if(afterRotate!=null){
                afterRotate(rotationAfter);
            }
           
        }

        if(Input.GetMouseButtonUp(mouseButton)){
            if(onRotateEnd!=null){
                onRotateEnd(CurrentRotation()); 
            }
        }





    }


    /**
     * rotation to start from
     */
    protected virtual Quaternion CurrentRotation(){
         return transform.rotation;
    }


    protected Quaternion RotateX(float x){
        return Quaternion.AngleAxis(x, Camera.main.transform.up); 
    }

    protected Quaternion RotateY(float y){
        return Quaternion.AngleAxis(y, Camera.main.transform.right); 
    }

    protected virtual Quaternion ApplyRotation(Quaternion rotation){
        transform.rotation=rotation;
        return rotation;
    }
}
