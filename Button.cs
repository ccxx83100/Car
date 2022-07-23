using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public GameManager GameMainScript;

    public void OnClickGearUP()
    {
        GameMainScript.ChanegeGearUP();
    }

    public void OnClickGearDown()
    {
        GameMainScript.ChanegeGearDown();
    }

    public void OnClickAccelerator()
    {
        GameMainScript.StepAccelerator();
    }

    public void OnClickBrake()
    {
        GameMainScript.StepBrake();
    }

}
