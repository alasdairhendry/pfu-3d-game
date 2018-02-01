using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour {

    [SerializeField] private UnityEvent OnPlayerEnter;
    [SerializeField] private UnityEvent OnPlayerExit;

    [SerializeField] private UnityEvent OnPlayerInteract;
    [SerializeField] private KeyCode interactKeycode;

    [SerializeField] public bool singleInteraction = true;
    private bool hasInteracted = false;

    private void OnTriggerStay(Collider other)
    {
        if (hasInteracted) return;

        if (other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(interactKeycode))
            {
                if (OnPlayerInteract != null)
                    OnPlayerInteract.Invoke();

                hasInteracted = true;
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (OnPlayerEnter != null)
                OnPlayerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (OnPlayerExit != null)
                OnPlayerExit.Invoke();

            hasInteracted = false;
        }
    }
}
