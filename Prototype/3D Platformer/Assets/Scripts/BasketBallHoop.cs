using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallHoop : Interactable {

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if(other.gameObject.name == "Basketball")
        {
            if (other.gameObject.GetComponent<Rigidbody>().velocity.y < 0)
            {
                if (other.gameObject.GetComponent<PhysGunTarget>().IsTargetted)
                    return;

                GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }
}
