using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStop : MonoBehaviour {

    // Careful when setting this to true - it might cause double
    // events to be fired - but it won't pass through the trigger
    public bool sendTriggerMessage = false;

    public LayerMask layerMask = -1; //make sure we aren't in this layer 
    public float skinWidth = 0.1f; //probably doesn't need to be changed 

    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector3 previousPosition;
    private Rigidbody myRigidbody;
    private Collider myCollider;

    //initialize values 
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        previousPosition = myRigidbody.position;
        minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    void FixedUpdate()
    {
        //have we moved more than our minimum extent? 
        Vector3 movementThisStep = myRigidbody.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;

            //check for obstructions we might have missed 
            if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            {
                if (!hitInfo.collider)
                    return;

                if (hitInfo.collider.isTrigger)
                    hitInfo.collider.SendMessage("OnTriggerEnter", myCollider);

                if (!hitInfo.collider.isTrigger)
                    myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;

            }
        }

        previousPosition = myRigidbody.position;
    }

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    //   private bool active = false;
    //   private float distance = 0.0f;
    //   private Vector3 direction = new Vector3(0, 0, 0);
    //   private Vector3 activePosition = new Vector3(0, 0, 0);
    //   private bool canCheckDistance = false;

    //   private void OnCollisionEnter(Collision collision)
    //   {
    //       active = true;
    //   }

    //   private void OnCollisionStay(Collision collision)
    //   {
    //       distance = Vector3.Distance(transform.position, collision.contacts[0].point);
    //       direction = collision.contacts[0].point - transform.position;

    //       if(canCheckDistance)
    //       {
    //           Debug.Log("Distance - " + Vector3.Distance(transform.position, activePosition));
    //       }
    //   }

    //   private void LateUpdate()
    //   {
    //       if(active)
    //       {
    //           activePosition = transform.position;
    //           canCheckDistance = true;
    //       }
    //   }

    //   private void OnCollisionExit(Collision collision)
    //   {
    //       if(active)
    //       {

    //       }
    //   }
}
