using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysGun : MonoBehaviour {

    [SerializeField] private GameObject beam;
    [SerializeField] private GameObject seeker;
    private bool seekerIsLocked = false;
    private bool seekerOffsetLocked = false;
    private Vector3 seekerOffset = Vector3.zero;
    [SerializeField] private float seekerHorizontalPull = 0.0f;

    private bool isLatched = false;
    private PhysGunTarget target = null;
    private LineRenderer lr;

    private bool hasForced = false;

	// Use this for initialization
	void Start () {
        lr = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {   
        
        //if(Input.GetMouseButtonDown(0))
        //{
        //    if (hasForced)
        //        return;
        //    if(!isLatched)
        //    {
        //        Fire();
        //    }
        //}

		if(Input.GetMouseButton(0))
        {
            if (hasForced)
                return;

            if (!isLatched)
                Fire();
            BeamControl();

            if(isLatched)
            {
                if(seekerIsLocked)
                {
                    if(Input.GetMouseButtonDown(1))
                    {
                        target.GetComponent<Rigidbody>().AddForce((beam.transform.forward).normalized * target.GetComponent<PhysGunTarget>().pushForce, ForceMode.Impulse);
                        target.GetComponent<PhysGunTarget>().wasPushed = true;
                        ResetBeam();
                        hasForced = true;
                    }
                }
            }
        }
        else
        {
            ResetBeam();
        }

        if (Input.GetMouseButtonUp(0))
            hasForced = false;

        Rotate();
        SeekerLatching();
	}

    private void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<PhysGunTarget>())
            {
                if (hit.collider.gameObject.GetComponent<PhysGunTarget>().IsTargetable)
                {
                    isLatched = true;
                    target = hit.collider.gameObject.GetComponent<PhysGunTarget>();
                    if (!target.IsTargetted)
                        target.OnStart();
                    return;
                }
            }
            else
            {
                if (target)
                    target.OnFinish();
                isLatched = false;
                target = null;
            }
        }

    }

    private void BeamControl()
    {       
        if(!target)
        {
            DisplayBeamNoTarget(beam.transform.position + (beam.transform.forward * 1.5f));            
        }
        else
        {
            DisplayBeamTarget(seeker.transform.position);
        }
    }

    private void DisplayBeamNoTarget(Vector3 targetPosition)
    {
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] { beam.transform.position, targetPosition });
    }

    private void DisplayBeamTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - beam.transform.position);
        float distance = Vector3.Distance(targetPosition, beam.transform.position);
        float distanceHalf = distance / 2.0f;
        Vector3 positionHalf = (beam.transform.position + (beam.transform.forward * distanceHalf));

        int numPoints = Mathf.CeilToInt(distance * 7.0f);

        if (numPoints % 2 == 0)
            numPoints++;

        Vector3[] points = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            Vector3 lerp01 = Vector3.Lerp(beam.transform.position, positionHalf, (float)i / (float)numPoints);
            Vector3 lerp02 = Vector3.Lerp(positionHalf, targetPosition, (float)i / (float)numPoints);

            Vector3 point = Vector3.Lerp(lerp01, lerp02, (float)i / (float)numPoints);
            points[i] = point;
        }

        lr.positionCount = points.Length;
        lr.SetPositions(points);
    }

    private void ResetBeam()
    {
        lr.positionCount = 0;
        isLatched = false;
        if(target)
        target.OnFinish();
        target = null;
    }

    private void Rotate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
        Vector3 direction = (mousePosition - beam.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1;

        if (Vector3.Distance(mousePosition, beam.transform.position) <= 0.5f)
        {
            return;
        }

        if (angle < -55 || angle > 40)
            return;

        angle = Mathf.Clamp(angle, -55, 40);
        seekerHorizontalPull = Mathf.Lerp(-1, 1, Mathf.InverseLerp(-55, 40, angle));
        transform.localEulerAngles = new Vector3(angle, 0, 0);
    }

    private void SeekerLatching()
    {
        if(isLatched)
        {
            if (!seekerIsLocked)
                seeker.transform.position = Vector3.Lerp(seeker.transform.position, target.transform.position, Time.deltaTime * 10.0f);
            else
            {
                if(!seekerOffsetLocked)
                seeker.transform.position = target.transform.position;

                if(!seekerOffsetLocked)
                {
                    seekerOffsetLocked = true;
                    seekerOffset = beam.transform.position - seeker.transform.position;
                }
                else
                {
                    Vector3 direction = (beam.transform.position - seeker.transform.position).normalized;

                    float scroll = Input.GetAxis("Mouse ScrollWheel");
                    seekerOffset -= scroll * (beam.transform.forward).normalized * 5.0f;
                }
            }

            if (!seekerIsLocked)
            {
                if (Vector3.Distance(seeker.transform.position, target.transform.position) < 0.5f)
                {
                    seekerIsLocked = true;
                }
                else
                {
                    seekerIsLocked = false;
                }
            }
            else
            {
                if(seekerOffsetLocked)
                {
                    // Move the seeker with the original offset relative to the beam's origin
                    target.Control(seeker.transform, beam);
                    
                }
            }
        }
        else
        {
            seeker.transform.position = beam.transform.position;
            seekerIsLocked = false;
            seekerOffsetLocked = false;
            seekerOffset = Vector3.zero;
        }
    }

    public void RemoveTarget()
    {
        seekerIsLocked = false;
        seekerOffsetLocked = false;
        seekerOffset = Vector3.zero;
        seekerHorizontalPull = 0.0f;
        isLatched = false;
        target = null;
        hasForced = false;
    }
}
