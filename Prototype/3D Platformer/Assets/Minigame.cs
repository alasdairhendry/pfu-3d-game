using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame : MonoBehaviour {

    protected bool isActive = false;
    [SerializeField] protected UnityEvent onComplete;
    [SerializeField] protected UnityEvent onFailed;

    protected virtual void Update()
    {
        if (isActive)
            if (Input.GetKeyDown(KeyCode.Escape))
                OnFailed();
    }

    public virtual void StartMinigame()
    {
        Debug.Log("Minigame Started");
        isActive = true;
    }

    public virtual void EndMinigame()
    {
        Debug.Log("Minigame Ended");
        isActive = false;
    }

    public virtual void OnComplete()
    {
        Debug.Log("You Won");

        if (onComplete != null)
            onComplete.Invoke();
    }

    public virtual void OnFailed()
    {
        Debug.Log("You Lost");

        if (onFailed != null)
            onFailed.Invoke();
    }
}
