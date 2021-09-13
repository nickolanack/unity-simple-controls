using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : MonoBehaviour
{
    


    public KeyMove keymove;
    public Vector2 keymoveRange;
    public Slider slider;


    public TouchMove touchmove;
    public Vector2 touchmoveRange;


    void Start()
    {
        if(slider==null){
            slider=gameObject.GetComponent<Slider>();
        }
        if(slider!=null){
            slider.onValueChanged.AddListener (delegate {

                if(keymove!=null){
                    float v=slider.value*(keymoveRange.y-keymoveRange.x)+keymoveRange.x;
                    keymove.speed=v;
                    keymove.sideSpeed =v;
                
                }

                if(touchmove!=null){
                    float v=slider.value*(touchmoveRange.y-touchmoveRange.x)+touchmoveRange.x;
                    touchmove.speedForward=v;
                    touchmove.speedSide =v;
                }


            });
            slider.value=(keymove.speed-keymoveRange.x)/(keymoveRange.y-keymoveRange.x);
        }

       
    }

}
