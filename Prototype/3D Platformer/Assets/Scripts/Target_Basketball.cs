using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Basketball : PhysGunTarget {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        GetComponent<Collider>().material = Resources.Load<PhysicMaterial>("PhysicsMaterials/Bouncey") as PhysicMaterial;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (wasPushed)
        {
            if (!isTargetted)
            {
                if (GetComponent<Rigidbody>().IsSleeping())
                    Die();
            }
        }
    }

    public override void OnStart()
    {
        base.OnStart();

        GetComponent<Collider>().material = null;
    }

    public override void OnFinish()
    {
        base.OnFinish();

        GetComponent<Collider>().material = Resources.Load<PhysicMaterial>("PhysicsMaterials/Bouncey") as PhysicMaterial;

        if (wasPushed)
        {
            isTargetable = false;
        }
    }

    public override void Control(Transform seeker, GameObject beam)
    {
        base.Control(seeker, beam);
    }    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void SetStartConstraints()
    {
        base.SetStartConstraints();
    }

    protected override void SetFinishConstraints()
    {
        base.SetFinishConstraints();
    }

    protected override void Die()
    {
        base.Die();
    }
}
