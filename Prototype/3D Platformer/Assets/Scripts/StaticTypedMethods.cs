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
    
    public static Vector3 Clamp(this Vector3 target, Vector3 min, Vector3 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = Mathf.Clamp(target.z, min.z, max.z);
        return target;
    }

}
