﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysGunTarget : MonoBehaviour {
    
    [SerializeField] public bool transformX, transformY, transformZ = true;

    protected Vector3 previousPosition = Vector3.zero;
    protected Vector3 transformVelocity = Vector3.zero;

    protected bool onFinishLateUpdate = false;

    [HideInInspector] public bool movementStartX, movementStartY, movementStartZ = false;
    [HideInInspector] public bool movementFinishX, movementFinishY, movementFinishZ = false;

    [HideInInspector] public bool rotationStartX, rotationStartY, rotationStartZ = false;
    [HideInInspector] public bool rotationFinishX, rotationFinishY, rotationFinishZ = false;

    protected RigidbodyConstraints startConstraints;
    protected RigidbodyConstraints finishConstraints;

    [HideInInspector] public bool wasPushed = false;
    public float pushForce = 45.0f;

    protected bool isTargetable = true;
    public bool IsTargetable { get { return isTargetable; } }

    protected bool isTargetted = false;    
    public bool IsTargetted { get { return isTargetted; } }

    // Use this for initialization
    protected virtual void Start () {
        SetStartConstraints();
        SetFinishConstraints();
    }

    // Update is called once per frame
    protected virtual void Update () {

	}

    public virtual void OnStart()
    {
        onFinishLateUpdate = false;
        GetComponent<Rigidbody>().constraints = startConstraints;
        wasPushed = false;
        isTargetted = true;
    }

    public virtual void OnFinish()
    {        
        onFinishLateUpdate = true;
        GetComponent<Rigidbody>().constraints = finishConstraints;
        isTargetted = false;
    }

    public virtual void Control(Transform seeker, GameObject beam)
    {        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
        relativePosition = new Vector3(mousePosition.x, mousePosition.y, beam.transform.position.z);

        relativePosition.x = (transformX) ? relativePosition.x : transform.position.x;
        relativePosition.y = (transformY) ? relativePosition.y : transform.position.y;
        relativePosition.z = (transformZ) ? relativePosition.z : transform.position.z;

        Vector3 targetedPosition = Vector3.Lerp(seeker.transform.position, relativePosition, Time.deltaTime * 5.0f / GetComponent<Rigidbody>().mass);        
        transform.position = targetedPosition;        
        seeker.transform.position = transform.position;
    }

    Vector3 relativePosition = Vector3.zero;

    protected virtual void FixedUpdate()
    {
        CalculateTransformVelocity();
    }

    protected virtual void LateUpdate()
    {
        if (onFinishLateUpdate)
        {
            if (!wasPushed)
            {
                GetComponent<Rigidbody>().velocity = transformVelocity;
                Debug.Log("wasnt pushed");
            }
            else
            {
                Debug.Log("Was pushed");
            }

            onFinishLateUpdate = false;
        }
    }

    protected void CalculateTransformVelocity()
    {
        transformVelocity = (relativePosition - previousPosition) / Time.fixedDeltaTime;

        previousPosition = relativePosition;
    }

    protected virtual void SetStartConstraints()
    {
        RigidbodyConstraints rb = GetComponent<Rigidbody>().constraints;

        if (movementStartX)
            rb = rb | RigidbodyConstraints.FreezePositionX;
        else rb &= ~RigidbodyConstraints.FreezePositionX;

        if (movementStartY)
            rb = rb | RigidbodyConstraints.FreezePositionY;
        else rb &= ~RigidbodyConstraints.FreezePositionY;

        if (movementStartZ)
            rb = rb | RigidbodyConstraints.FreezePositionZ;
        else rb &= ~RigidbodyConstraints.FreezePositionZ;

        if (rotationStartX)
            rb = rb | RigidbodyConstraints.FreezeRotationX;
        else rb &= ~RigidbodyConstraints.FreezeRotationX;

        if (rotationStartY)
            rb = rb | RigidbodyConstraints.FreezeRotationY;
        else rb &= ~RigidbodyConstraints.FreezeRotationY;

        if (rotationStartZ)
            rb = rb | RigidbodyConstraints.FreezeRotationZ;
        else rb &= ~RigidbodyConstraints.FreezeRotationZ;

        startConstraints = rb;
    }

    protected virtual void SetFinishConstraints()
    {
        RigidbodyConstraints rb = GetComponent<Rigidbody>().constraints;

        if (movementFinishX)
            rb = rb | RigidbodyConstraints.FreezePositionX;
        else rb &= ~RigidbodyConstraints.FreezePositionX;

        if (movementFinishY)
            rb = rb | RigidbodyConstraints.FreezePositionY;
        else rb &= ~RigidbodyConstraints.FreezePositionY;

        if (movementFinishZ)
            rb = rb | RigidbodyConstraints.FreezePositionZ;
        else rb &= ~RigidbodyConstraints.FreezePositionZ;

        if (rotationFinishX)
            rb = rb | RigidbodyConstraints.FreezeRotationX;
        else rb &= ~RigidbodyConstraints.FreezeRotationX;

        if (rotationFinishY)
            rb = rb | RigidbodyConstraints.FreezeRotationY;
        else rb &= ~RigidbodyConstraints.FreezeRotationY;

        if (rotationFinishZ)
            rb = rb | RigidbodyConstraints.FreezeRotationZ;
        else rb &= ~RigidbodyConstraints.FreezeRotationZ;


        finishConstraints = rb;
    }

    protected void RemoveSelfAsTarget()
    {
        isTargetted = false;
        onFinishLateUpdate = false;
        GameObject.FindObjectOfType<PhysGun>().RemoveTarget();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}

/*
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnFinish()
    {
        base.OnFinish();
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
*/
