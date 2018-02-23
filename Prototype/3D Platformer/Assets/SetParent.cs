using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetObjectParent(Transform target)
    {
        transform.SetParent(target);
    }

    public void RemoveParent()
    {
        transform.parent = null;
    }
}
