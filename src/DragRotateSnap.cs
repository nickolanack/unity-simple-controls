using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotateSnap : DragRotate
{

	public bool snap=true;
    public Vector3 snapGrid=new Vector3(90f, 90f, 90f);

    public bool lockX=false;
    public bool lockY=false;
    public bool lockZ=false;

    public Vector3 lockAt=new Vector3(0,0,0);

   
    protected override Quaternion ApplyRotation(Quaternion rotation){


   		
		Vector3 eulerAngles=Vector3.zero;


		if(snap||lockX||lockY||lockZ){
        	eulerAngles=rotation.eulerAngles;
        }


   		if(snap){

            eulerAngles.x=Mathf.Round(eulerAngles.x/snapGrid.x)*snapGrid.x;
            eulerAngles.y=Mathf.Round(eulerAngles.y/snapGrid.y)*snapGrid.y;
            eulerAngles.z=Mathf.Round(eulerAngles.z/snapGrid.z)*snapGrid.z;
        }


        if(lockX){
            eulerAngles.x=lockAt.x;
        }
        if(lockY){
            eulerAngles.y=lockAt.y;
        }
        if(lockZ){
            eulerAngles.z=lockAt.z;
            
        }

        if(snap||lockX||lockY||lockZ){
        	rotation.eulerAngles=eulerAngles;
        }



        transform.rotation=rotation;

        return rotation;
    }
    
}
