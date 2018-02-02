using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField] private bool activeOnStart = false;
    private GameObject objects;
    private MeshRenderer[] renderers;

    private void Start()
    {
        objects = transform.Find("Objects").gameObject;
        MeshRenderer[] r = GetComponentsInChildren<MeshRenderer>();
        renderers = r;

        if (!activeOnStart)
        {
            //objects.SetActive(false);            
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }
        }
    }

    public void DisplayRoom()
    {
        StartCoroutine(DisplayIterate());
    }

    private IEnumerator DisplayIterate()
    {
        int i = 0;

        while(i< renderers.Length)
        {
            renderers[i].enabled = true;

            i++;
            yield return null;
        }
    }
}
