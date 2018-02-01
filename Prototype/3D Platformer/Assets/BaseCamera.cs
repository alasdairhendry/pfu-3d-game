using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour {

    protected bool isActive = false;

    protected virtual void Start()
    {

    }

    protected virtual void Update () {
		
	}

    public virtual void SetActive(bool state)
    {
        isActive = state;

        if(isActive)
        {
            GameObject.FindObjectOfType<PlayerCamera>().SetCurrentCamera(this);
        }
    }    

    public virtual void Switch(BaseCamera target)
    {
        target.SetActive(true);
        SetActive(false);
    }
}
