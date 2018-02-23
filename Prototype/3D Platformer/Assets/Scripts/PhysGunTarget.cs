using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysGunTarget : MonoBehaviour {

    [SerializeField] private string _name = "";
    [SerializeField] private string interactionDescription = "";
    [SerializeField] public bool transformX, transformY, transformZ = true;
    [SerializeField] public bool allowFreeze, allowPush = true;

    protected Vector3 previousPosition = Vector3.zero;
    protected Vector3 transformVelocity = Vector3.zero;

    protected bool onFinishLateUpdate = false;

    [HideInInspector] [SerializeField] public bool useDefaultExtents = false;
    [HideInInspector] [SerializeField] public float extentsDefaultX, extentsDefaultY, extentsDefaultZ = 1.0f;
    [HideInInspector] [SerializeField] public float extentsDefaultXOffset, extentsDefaultYOffset, extentsZDefaultOffset = 0.0f;
    [SerializeField] private Vector3 worldExtentsDefaultMin = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 worldExtentsDefaultMax = new Vector3(1, 1, 1);
    private bool worldExtentsDefaultSet = false;

    [HideInInspector] [SerializeField] public bool useTargetedExtents = false;
    [HideInInspector] [SerializeField] public float extentsTargetedX, extentsTargetedY, extentsTargetedZ = 1.0f;
    [HideInInspector] [SerializeField] public float extentsTargetedXOffset, extentsTargetedYOffset, extentsZTargetedOffset = 0.0f;
    [SerializeField] private Vector3 worldExtentsTargetedMin = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 worldExtentsTargetedMax = new Vector3(1, 1, 1);
    private bool worldExtentsTargetedSet = false;

    [HideInInspector] [SerializeField] public bool useGravityStart = false;
    [HideInInspector] [SerializeField] public bool movementStartX, movementStartY, movementStartZ = false;
    [HideInInspector] [SerializeField] public bool movementFinishX, movementFinishY, movementFinishZ = false;
    
    [HideInInspector] [SerializeField] public bool useGravityFinish = true;
    [HideInInspector] [SerializeField] public bool rotationStartX, rotationStartY, rotationStartZ = false;
    [HideInInspector] [SerializeField] public bool rotationFinishX, rotationFinishY, rotationFinishZ = false;

    public enum CollidableType { Red, Green, Blue, None }
    [SerializeField] private CollidableType collidableType = CollidableType.None;
    public CollidableType GetCollidableType { get { return collidableType; } }
    public CollidableType SetCollidableType
    {
        set
        {
            switch (value)
            {
                case CollidableType.None:
                    gameObject.layer = LayerMask.NameToLayer("Default");
                    break;

                case CollidableType.Blue:
                    gameObject.layer = LayerMask.NameToLayer("ObjectBlue");
                    break;

                case CollidableType.Red:
                    gameObject.layer = LayerMask.NameToLayer("ObjectRed");
                    break;

                case CollidableType.Green:
                    gameObject.layer = LayerMask.NameToLayer("ObjectGreen");
                    break;
            }
            collidableType = value;
        }
    }

    protected RigidbodyConstraints startConstraints;
    protected RigidbodyConstraints finishConstraints;

    [HideInInspector] public bool wasPushed = false;
    [SerializeField] protected float pushForce = 45.0f;

    protected bool isTargetable = true;
    public bool IsTargetable { get { return isTargetable; } }

    protected bool isTargetted = false;    
    public bool IsTargetted { get { return isTargetted; } }

    private bool isFrozen = false;

    private Transform seeker;
    private GameObject beam;

    // Use this for initialization
    protected virtual void Start () {
        SetStartConstraints();
        SetFinishConstraints();
        SetCollidableType = collidableType;
        SetWorldExtents();
    }

    // Update is called once per frame
    protected virtual void Update () {
        if (isTargetted)
        {
            if (worldExtentsTargetedSet)
            {
                if (useTargetedExtents)
                {
                    transform.position = transform.position.Clamp(worldExtentsTargetedMin, worldExtentsTargetedMax);
                }
            }
        }
        else
        {
            if (worldExtentsDefaultSet)
            {
                if (useDefaultExtents)
                {
                    transform.position = transform.position.Clamp(worldExtentsDefaultMin, worldExtentsDefaultMax);
                }
            }
        }
    }

    public virtual void OnStart()
    {
        isFrozen = false;
        wasPushed = false;
        onFinishLateUpdate = false;
        GetComponent<Rigidbody>().constraints = startConstraints;
        GetComponent<Rigidbody>().useGravity = useGravityStart;
        wasPushed = false;
        isTargetted = true;
    }

    public virtual void OnFinish()
    {        
        onFinishLateUpdate = true;
        GetComponent<Rigidbody>().constraints = finishConstraints;
        GetComponent<Rigidbody>().useGravity = useGravityFinish;
        isTargetted = false;
    }

    public virtual void Control(Transform seeker, GameObject beam)
    {
        this.seeker = seeker;
        this.beam = beam;        
    }

    public virtual void Push(Transform beam)
    {
        if (allowPush)
        {
            GetComponent<Rigidbody>().AddForce((beam.forward).normalized * pushForce, ForceMode.Impulse);
            wasPushed = true;
        }        
    }

    public virtual void Freeze()
    {
        if(allowFreeze)
        {
            isFrozen = true;
        }
    }

    Vector3 relativePosition = Vector3.zero;

    protected virtual void FixedUpdate()
    {
        CalculateTransformVelocity();

        if(isFrozen)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            return;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        if(IsTargetted)
        {
            if (seeker == null)
                return;

            if (beam == null) return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
            relativePosition = new Vector3(mousePosition.x, mousePosition.y, beam.transform.position.z);

            relativePosition.x = (transformX) ? relativePosition.x : transform.position.x;
            relativePosition.y = (transformY) ? relativePosition.y : transform.position.y;
            relativePosition.z = (transformZ) ? relativePosition.z : transform.position.z;

            Vector3 targetedPosition = Vector3.Lerp(seeker.transform.position, relativePosition, Time.deltaTime * 5.0f / GetComponent<Rigidbody>().mass);
            float dist = Vector3.Distance(targetedPosition, transform.position);
            Vector3 newVelocity = Vector3.zero;
            Debug.Log(dist);

            if (dist > 0.075f)
                newVelocity = (targetedPosition - transform.position) * (5.0f / Vector3.Distance(targetedPosition, transform.position));
            else newVelocity = (targetedPosition - transform.position) * 35.0f;

            GetComponent<Rigidbody>().velocity = newVelocity;
            seeker.transform.position = transform.position;
        }
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

    protected virtual void OnMouseEnter()
    {
        if (!IsTargetable) return;
        FindObjectOfType<InteractionCanvas>().Show("LMB", interactionDescription);
        FindObjectOfType<InspectionCanvas>().Show(_name, transformX, transformY, transformZ, allowPush, allowFreeze);
    }

    protected virtual void OnMouseExit()
    {
        FindObjectOfType<InteractionCanvas>().Hide();
        FindObjectOfType<InspectionCanvas>().Hide();
    }

    private void OnDrawGizmosSelected()
    {
        if (useDefaultExtents)
        {
            float x = (extentsDefaultX);
            float y = (extentsDefaultY);
            float z = (extentsDefaultZ);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, 0.5f, 0.0f) - new Vector3(extentsDefaultXOffset, extentsDefaultYOffset, extentsZDefaultOffset), new Vector3(x, y, z));
        }

        if (useTargetedExtents)
        {
            float x = (extentsTargetedX);
            float y = (extentsTargetedY);
            float z = (extentsTargetedZ);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, 0.5f, 0.0f) - new Vector3(extentsTargetedXOffset, extentsTargetedYOffset, extentsZTargetedOffset), new Vector3(x, y, z));
        }
    }

    private void SetWorldExtents()
    {
        if (useDefaultExtents)
        {
            Vector3 globalExtents = transform.TransformDirection(new Vector3(extentsDefaultX, extentsDefaultY, extentsDefaultZ) - new Vector3(extentsDefaultXOffset, extentsDefaultYOffset, extentsZDefaultOffset) + new Vector3(0.0f, 0.5f, 0.05f));
            Vector3 centre = transform.position + new Vector3(0.0f, 0.5f, 0.0f) - new Vector3(extentsDefaultXOffset, extentsDefaultYOffset, extentsZDefaultOffset);
            Vector3 size = new Vector3(extentsDefaultX, extentsDefaultY, extentsDefaultZ);
            worldExtentsDefaultMin = centre - (size / 2) + new Vector3(transform.localScale.x / 2.0f, 0, transform.localScale.z / 2.0f);
            worldExtentsDefaultMax = centre + (size / 2) - new Vector3(transform.localScale.x / 2.0f, transform.localScale.y, transform.localScale.z / 2.0f);
            worldExtentsDefaultSet = true;
        }

        if(useTargetedExtents)
        {
            Vector3 globalExtents = transform.TransformDirection(new Vector3(extentsTargetedX, extentsTargetedY, extentsTargetedZ) - new Vector3(extentsTargetedXOffset, extentsTargetedYOffset, extentsZTargetedOffset) + new Vector3(0.0f, 0.5f, 0.05f));
            Vector3 centre = transform.position + new Vector3(0.0f, 0.5f, 0.0f) - new Vector3(extentsTargetedXOffset, extentsTargetedYOffset, extentsZTargetedOffset);
            Vector3 size = new Vector3(extentsTargetedX, extentsTargetedY, extentsTargetedZ);
            worldExtentsTargetedMin = centre - (size / 2) + new Vector3(transform.localScale.x / 2.0f, 0, transform.localScale.z / 2.0f);
            worldExtentsTargetedMax = centre + (size / 2) - new Vector3(transform.localScale.x / 2.0f, transform.localScale.y, transform.localScale.z / 2.0f);
            worldExtentsTargetedSet = true;
        }
    }

    public void UseDefaultExtents(bool state)
    {
        useDefaultExtents = state;
    }

    public void SetTargetable(bool state)
    {
        isTargetable = state;

        if(!state)
        {
            RemoveSelfAsTarget();
        }
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
