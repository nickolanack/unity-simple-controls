using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKeyMove : KeyMove
{
    
    public CharacterController character;

    void Start(){
        if(character==null){
            character=GetComponent<CharacterController>();
        }
    }


    protected override void ApplyMove(Vector3 move){
        character.Move(move);
    }


    protected override Vector3 RightDir(){
        return Camera.main.transform.right;
    }

    protected override Vector3 ForwardDir(){
        return Camera.main.transform.forward;
    }


}
