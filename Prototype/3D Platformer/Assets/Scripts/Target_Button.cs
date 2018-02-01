using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target_Button : PhysGunTarget {

    public enum TriggerType { OneShot, Auto, AutoDelay }
    [Header("Button")] [SerializeField] private TriggerType triggerType = TriggerType.OneShot;
    [SerializeField] private float delay = 0.25f;

    [SerializeField] private bool multipleTriggers = false; // Can the user keep letting go and attaching to this to trigger?
    [SerializeField] UnityEvent onTrigger;

    [SerializeField] private bool hasTriggered = false;
    
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

        StartCoroutine(ActionAuto());
    }  

    private IEnumerator ActionAuto()
    {
        float _delay = delay;

        while(_delay > 0)
        {
            _delay -= Time.deltaTime;

            if(_delay <= 0)
            {
                if (onTrigger != null)
                {
                    hasTriggered = true;
                    onTrigger.Invoke();
                }

                if(triggerType == TriggerType.OneShot)
                {
                    yield break;
                }

                _delay = delay;
            }

            yield return null;
        }
    }

    public override void OnFinish()
    {
        base.OnFinish();

        if (multipleTriggers)
            hasTriggered = false;
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
