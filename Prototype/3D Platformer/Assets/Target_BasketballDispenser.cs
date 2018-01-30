using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_BasketballDispenser : PhysGunTarget {

    [SerializeField] private GameObject basketballPrefab;

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
        if (!isTargetted)
        {
            StartCoroutine(Spawn());
        }

        base.OnStart();
    }

    [SerializeField] private SphereCollider spherecol;
    private IEnumerator Spawn()
    {
        yield return null;
        bool spawned = false;

        while(!spawned)
        {
            GameObject go = Instantiate(basketballPrefab);
            go.transform.position = new Vector3(3.0f, 2.0f, 0.0f);
            go.transform.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
            go.name = "Basketball";
            ClothSphereColliderPair[] a = new ClothSphereColliderPair[1];
            a[0] = new ClothSphereColliderPair(go.GetComponentInChildren<SphereCollider>());
            GameObject.FindObjectOfType<Cloth>().sphereColliders = a;            
            spawned = true;

            yield return null;
        }
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
}
