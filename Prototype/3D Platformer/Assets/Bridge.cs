using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {

    Animator animator;
    [SerializeField] private bool autoCompress = false;
    [SerializeField] private float autoCompressDelay = 1.5f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Extend()
    {
        animator.SetTrigger("Extend");

        if (autoCompress)
            StartCoroutine(AutoCompress());
    }

    public void Compress()
    {
        animator.SetTrigger("Compress");
    }

    private IEnumerator AutoCompress()
    {
        yield return new WaitForSeconds(autoCompressDelay);
        Compress();
    }
}
