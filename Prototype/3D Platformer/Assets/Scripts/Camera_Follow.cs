using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : BaseCamera {

    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField] private GameObject player;

    protected override void Start()
    {

    }

    protected override void Update()
    {
        if (!isActive) return;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime);
    }

    public override void SetActive(bool state)
    {
        base.SetActive(state);

        if (isActive)
        {
            GetComponent<Camera>().enabled = true;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            GetComponent<Camera>().enabled = false;
        }
    }

    public override void Switch(BaseCamera target)
    {
        base.Switch(target);
    }

    [ContextMenu("SetOffset")]
    private void SetOffset()
    {
        offset = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
