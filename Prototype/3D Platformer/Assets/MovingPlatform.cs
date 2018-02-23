using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] private Vector2 positionRange;
    public enum MovementAxis { x, y, z }
    [SerializeField] private MovementAxis movementAxis = MovementAxis.x;
    [SerializeField] private bool reverse = false;

    [SerializeField] private float movementSpeed = 1.0f;

    private bool moveToMax = true;
	// Use this for initialization
	void Start () {
        if (reverse)
            moveToMax = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(moveToMax)
        {
            Vector3 newPosition = new Vector3(0, 0, 0);
            Vector3 targetPosition = new Vector3(0, 0, 0);

            targetPosition = new Vector3(positionRange.y, transform.position.y, transform.position.z);
            newPosition = Vector3.Lerp(targetPosition, transform.position, Time.deltaTime );

            GetComponent<Rigidbody>().velocity = (newPosition - transform.position) * movementSpeed / Vector3.Distance(targetPosition, transform.position);

            if(Vector3.Distance(transform.position, targetPosition) < 0.25f)
            {
                moveToMax = !moveToMax;
            }
        }
        else
        {
            Vector3 newPosition = new Vector3(0, 0, 0);
            Vector3 targetPosition = new Vector3(0, 0, 0);

            targetPosition = new Vector3(positionRange.x, transform.position.y, transform.position.z);
            newPosition = Vector3.Lerp(targetPosition, transform.position, Time.deltaTime);

            GetComponent<Rigidbody>().velocity = (newPosition - transform.position) * movementSpeed / Vector3.Distance(targetPosition, transform.position);

            if (Vector3.Distance(transform.position, targetPosition) < 0.25f)
            {
                moveToMax = !moveToMax;
            }
        }
	}
}
