using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    [SerializeField] private BaseCamera initialCamera;
    [SerializeField] private BaseCamera currentCamera;

	// Use this for initialization
	void Start () {
        initialCamera.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCurrentCamera(BaseCamera target)
    {
        currentCamera = target;
    }
}
