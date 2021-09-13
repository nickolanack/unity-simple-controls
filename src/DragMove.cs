using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragMove : MonoBehaviour
{


    /**
     * this should work with mobile touches in addition to mouse dragging
     */
    

    public int mouseButton=0;
    public /*readonly*/ Vector2 start = Vector2.zero;
    Vector3 position = Vector3.zero;

    public float speed=0.1f;
    public float sideSpeed=0.1f;

    KeyCode toggleForward=KeyCode.LeftShift;


    public delegate void DragEvent(Vector3 position);
    public delegate bool ValidateDragEvent(Vector2 position);


    public DragEvent onDragStart;
    public DragEvent onDrag; //called with unprocessed target position
    public DragEvent afterDrag; //called after move, with applied position (may be different that onDrag position)
    public DragEvent onDragEnd;


    /**
     * use this to validate start position for drag, ie: check that cursor is on item
     */
    public ValidateDragEvent validStartClick=delegate(Vector2 position){
        return true;
    };

    /**
     * use this to initiate dragging, ie wait to make sure click wasn't a single click or force a drag threshold before updating
     */
    public ValidateDragEvent validStartThreshold=delegate(Vector2 position){
        return true;
    };


    public bool ignoreUI=true; 

    public /*readonly*/ bool dragStarted=false;
    public float startTime=-1;
    
    public /*readonly*/ bool dragging=false;



    
    public bool useRayCastPosition=false;
    public bool rayCastWarn=true;


    void Update()
    {


        
        if(Input.GetMouseButtonDown(mouseButton)){

            if(ignoreUI&&EventSystem.current.currentSelectedGameObject!=null){
                return;
            }

            if(!validStartClick(Input.mousePosition)){
                return;
            }

            dragStarted=true;
            startTime=Time.time;

            start=Input.mousePosition;
            position=CurrentPosition();

            
        }

        if(!dragStarted){
            return;
        }

        if(Input.GetMouseButton(mouseButton)){

            if(!dragging){
                if(validStartThreshold(Input.mousePosition)){

                    dragging=true;
                    if(onDragStart!=null){
                        onDragStart(position);
                    }

                }else{
                    return;
                }

            }

            if(useRayCastPosition){

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit)){

                    if(rayCastWarn&&hit.transform==transform){
                        Debug.LogWarning("Drag move is ray casting itself!");
                    }

                    position=hit.point;
                }else{
                    return;
                }


            }else{

                position+=(Input.mousePosition.y-start.y)*DirectionY()*speed;
                position+=(Input.mousePosition.x-start.x)*DirectionX()*sideSpeed;
                start=Input.mousePosition;

            }

            

            if(onDrag!=null){
                onDrag(position);
            }

            Vector3 positionAfter=ApplyMove(position);

            if(afterDrag!=null){
                afterDrag(positionAfter);
            }
           
        }
        if(Input.GetMouseButtonUp(mouseButton)){
            if(onDragEnd!=null){
                onDragEnd(CurrentPosition()); 
            }

            dragStarted=false;
            dragging=false;

        }

    }


    /**
     * position to start from
     */
    protected virtual Vector3 CurrentPosition(){
         return transform.position;
    }


    /**
     * override method to format position. align/snap/clamp or to change the drag target(s)
     * @param {[type]} Vector3 position [description]
     */
    protected virtual Vector3 ApplyMove(Vector3 position){
         transform.position=position;
         return position;
    }


    protected virtual Vector3 DirectionY(){

        if(toggleForward!=KeyCode.None&&Input.GetKey(toggleForward)){
            return GetForward();
        }

        return Vector3.up;
    }

    protected virtual Vector3 DirectionX(){
        return Camera.main.transform.right;
    }

    protected virtual Vector3 GetForward(){
        return Camera.main.transform.forward;
    }

}
