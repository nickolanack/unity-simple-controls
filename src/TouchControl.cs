using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TouchControl : MonoBehaviour
{


    public delegate void UseTouch(Touch t);
    public delegate bool FilterTouch(Touch t);
    public FilterTouch shouldUseTouch=delegate(Touch t){ return true; };
    public UseTouch touchStart=delegate(Touch t){  };
    public UseTouch touchMove=delegate(Touch t){  };
    public UseTouch touchEnd=delegate(Touch t){  };


    int fingerId=-1;


    void Update()
    {
        

        if(Input.touchCount==0){
            return;
        }

        foreach(Touch t in Input.touches){

            if(fingerId==-1){
                if(t.phase==TouchPhase.Began){
                    if(shouldUseTouch(t)){
                        fingerId=t.fingerId;
                        touchStart(t);
                        Debug.Log("touch start: "+t.fingerId);
                        return;
                    }
                }
            }


            if(fingerId!=-1){
            

                if(t.fingerId==fingerId&&t.phase==TouchPhase.Moved){
                    touchMove(t);
                    return;
                }


                if(t.fingerId==fingerId&&t.phase==TouchPhase.Ended){
                    touchEnd(t);
                    fingerId=-1;
                    return;
                }

            }

        }





    }
}
