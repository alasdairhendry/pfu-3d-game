using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysGunTarget : MonoBehaviour {

    [SerializeField] public bool transformX = true;
    [SerializeField] public bool transformY = true;
    [SerializeField] public bool transformZ = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnStart()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void OnFinish()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Control(Transform seeker)
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (transformX)
            newPosition.x = seeker.position.x;
        if (transformY)
            newPosition.y = seeker.position.y;
        if (transformZ)
            newPosition.z = seeker.position.z;

        transform.position = newPosition;
    }
}
