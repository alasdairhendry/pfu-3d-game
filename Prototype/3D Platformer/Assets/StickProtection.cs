using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickProtection : MonoBehaviour {
    
    [SerializeField] private PhysicMaterial noFriction;
    private PlayerMovement playerMovement;

    private bool playerIsOnTop = false;

	// Use this for initialization
	void Start () {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Check if the player is on top of us, or just sticking to the side of us 
            // If he is on top, we want to have friction so the player can walk properly
            // If he is beside us, we want no friction so the player doesnt get stuck

            CheckOnPlatform();
        }
    }

    private void CheckOnPlatform()
    {
        bool _playerIsOnTop = playerMovement.CheckOnPlatform(this.gameObject);

        if(_playerIsOnTop)
        {
            playerIsOnTop = true;
            GetComponent<Collider>().material = null;
        }
        else
        {
            playerIsOnTop = false;
            GetComponent<Collider>().material = noFriction;
        }
    }
}
