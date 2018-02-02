using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticTypedMethods {

    public static Camera ReturnActiveCamera()
    {
        BaseCamera[] cameras = GameObject.FindObjectsOfType<BaseCamera>();

        foreach (BaseCamera cam in cameras)
        {
            if (cam.IsActive)
            {
                return cam.GetComponent<Camera>();
            }
        }

        return null;
    }
    
}
