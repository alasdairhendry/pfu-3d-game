using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    Animator animator;
    [SerializeField] private bool autoClose = false;
    [SerializeField] private float autoCloseDelay = 1.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetTrigger("Open");

        if (autoClose)
            StartCoroutine(AutoClose());
    }

    public void Close()
    {
        animator.SetTrigger("Close");
    }

    private IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(autoCloseDelay);
        Close();
    }
}
