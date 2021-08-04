using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveMultiSpeed : TouchMove
{
    

   public List<Vector2> forwardRanges;
   public List<Vector2> backwardRanges;
   public List<Vector2> sideRanges;

   public float pixelLengthForwardY=200;
   public float pixelLengthBackwardY=100;
   public float pixelLengthX=120;


    protected override Vector3 CalcForward(Vector3 direction, float touchYDelta){

        if(touchYDelta>0){
            float fraction=Mathf.Min(touchYDelta/pixelLengthForwardY, 1);
            float multiplierValue=RangeValue(fraction, forwardRanges);
            Debug.Log("Forward: "+multiplierValue);
            return direction*multiplierValue;
        }

        return CalcBackward(direction, touchYDelta);
    }

    protected Vector3 CalcBackward(Vector3 direction, float touchYDelta){
        
        float fraction=Mathf.Min(-touchYDelta/pixelLengthBackwardY, 1);
        float multiplierValue=RangeValue(fraction, backwardRanges);
        Debug.Log("Backward: "+multiplierValue);
        return -direction*multiplierValue;
    }

    protected override Vector3 CalcSide(Vector3 direction, float touchXDelta){

        float fraction=Mathf.Min(touchXDelta/pixelLengthX, 1);
        float multiplierValue=RangeValue(Mathf.Abs(fraction), sideRanges);
        if(touchXDelta<0){
            multiplierValue*=-1;
        }
        Debug.Log("Side: "+multiplierValue);
        return direction*multiplierValue;
    }

    float RangeValue(float fraction, List<Vector2> range){

        float multiplier=0;
        foreach(Vector2 segment in range){
            if(fraction<segment.x){
               return multiplier;
            }
            multiplier=segment.y;
        }
        return multiplier;
    }

}
