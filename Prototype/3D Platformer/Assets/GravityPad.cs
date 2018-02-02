using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPad : MonoBehaviour {

    [SerializeField] private float force = 15.0f;
    [SerializeField] private float maxKineticForce = 3.0f;
    [SerializeField] private bool acceptsPlayer = true;
    [SerializeField] private bool acceptsObects = true;
    [SerializeField] private List<PhysGunTarget.CollidableType> acceptedColliders = new List<PhysGunTarget.CollidableType>();

    private bool bounce = false;
    private Collision collision;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!bounce)
        {
            //Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        bounce = true;
        this.collision = collision;
    }

    private void FixedUpdate()
    {
        if (!bounce) return;

        if (collision.gameObject.tag == "Player")
        {
            if (!acceptsPlayer) return;
            Debug.Log("Boop");
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.y < 0)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce((transform.up * force) * Mathf.Clamp((collision.gameObject.GetComponent<Rigidbody>().velocity.y * -1), 1, maxKineticForce), ForceMode.Impulse);
            }
            else
                collision.gameObject.GetComponent<Rigidbody>().AddForce((transform.up * force), ForceMode.Impulse);

            // TODO: Play sound effect?

            bounce = false;            
        }
        else if(collision.gameObject.GetComponent<PhysGunTarget>())
        {
            if(acceptsObects)
            {
                if (collision.gameObject.GetComponent<PhysGunTarget>().IsTargetted)
                    return;

                bool typeAccepted = false;

                foreach (PhysGunTarget.CollidableType accepted in acceptedColliders)
                {
                    if (accepted == collision.gameObject.GetComponent<PhysGunTarget>().GetCollidableType)
                        typeAccepted = true;
                }

                if (!typeAccepted)
                {
                    // TODO: Play sound effect?
                    return;
                }

                if (collision.gameObject.GetComponent<Rigidbody>().velocity.y < 0)
                {
                    collision.gameObject.GetComponent<Rigidbody>().AddForce((transform.up * force) * Mathf.Clamp((collision.gameObject.GetComponent<Rigidbody>().velocity.y * -1), 1, maxKineticForce), ForceMode.Impulse);
                }
                else
                    collision.gameObject.GetComponent<Rigidbody>().AddForce((transform.up * force), ForceMode.Impulse);

                // TODO: Play sound effect?

                bounce = false;
            }
        }
    }

    public void AcceptNone(bool clear)
    {
        if (clear)
            acceptedColliders.Clear();
        acceptedColliders.Add(PhysGunTarget.CollidableType.None);
    }

    public void AcceptBlue(bool clear)
    {
        if (clear)
            acceptedColliders.Clear();
        acceptedColliders.Add(PhysGunTarget.CollidableType.Blue);
    }

    public void AcceptRed(bool clear)
    {
        if (clear)
            acceptedColliders.Clear();
        acceptedColliders.Add(PhysGunTarget.CollidableType.Red);
    }

    public void AcceptGreen(bool clear)
    {
        if (clear)
            acceptedColliders.Clear();
        acceptedColliders.Add(PhysGunTarget.CollidableType.Green);
    }

    public void ClearAcceptedColliders()
    {
        acceptedColliders.Clear();
    }
}
