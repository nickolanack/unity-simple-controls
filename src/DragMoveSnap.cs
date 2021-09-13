using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMoveSnap : DragMove
{


    public bool snap=true;
    public Vector3 snapGrid=new Vector3(1f, 1f, 1f);


    public bool lockX=false;
    public bool lockY=false;
    public bool lockZ=false;

    public bool lockAtTransform=true;
  

    public Vector3 lockAt=Vector3.zero;


    public delegate Vector3 PositionModifier(Vector3 dragPosition, Vector3 snapPosition);
    public List<PositionModifier> modifiers=new List<PositionModifier>();


    protected override Vector3 ApplyMove(Vector3 rawPosition){


        Vector3 snapPosition=ApplySnap(rawPosition);
        snapPosition=ApplyLock(snapPosition);


        if(modifiers.Count>0){
            foreach(PositionModifier modifier in modifiers){
               snapPosition = modifier(rawPosition, snapPosition);
            }
        }



        transform.position=snapPosition;
        return snapPosition;
    }


    protected Vector3 ApplySnap(Vector3 position){
        if(snap){
        
            position.x=Mathf.Round(position.x/snapGrid.x)*snapGrid.x;
            position.y=Mathf.Round(position.y/snapGrid.y)*snapGrid.y;
            position.z=Mathf.Round(position.z/snapGrid.z)*snapGrid.z;
        }
        return position;
    }


    protected Vector3 ApplyLock(Vector3 position){
        if(lockX){
            position.x=lockAtTransform?transform.position.x:lockAt.x;
        }
        if(lockY){
            position.y=lockAtTransform?transform.position.y:lockAt.y;
        }
        if(lockZ){
            position.z=lockAtTransform?transform.position.z:lockAt.z;
        }
        return position;
    }



    protected override Vector3 DirectionY(){

        if(!lockY){
            return base.DirectionY();
        }

        /**
         * automatically switch w drag to move along forward plane if y is locked
         */


        return GetForward();
        
    }
    
}
