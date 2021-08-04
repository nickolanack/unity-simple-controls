using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithCamera : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        float y=Camera.main.transform.eulerAngles.y;
        Vector3 rot=transform.eulerAngles;
        rot.y=y;
        //transform.eulerAngles=rot;
        Quaternion b=Quaternion.Euler(rot);
        transform.rotation=Quaternion.Lerp(transform.rotation, b, 10);

    }
}
