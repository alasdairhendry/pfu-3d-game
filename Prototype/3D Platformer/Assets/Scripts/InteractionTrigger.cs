using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour {

    [SerializeField] private UnityEvent OnPlayerEnter;
    [SerializeField] private UnityEvent OnPlayerExit;

    [SerializeField] private UnityEvent OnPlayerInteract;
    [SerializeField] private KeyCode interactKeycode;

    [SerializeField] private bool isActive = true;

    [SerializeField] public bool singleInteraction = true;
    private bool hasInteracted = false;

    [SerializeField] private string description = "";

    private void OnTriggerStay(Collider other)
    {
        if (!isActive) return;
        if (hasInteracted && singleInteraction) return;

        if (other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(interactKeycode))
            {
                if (OnPlayerInteract != null)
                    OnPlayerInteract.Invoke();

                hasInteracted = true;
                //FindObjectOfType<InteractionCanvas>().Hide();
            }            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (other.gameObject.tag == "Player")
        {
            if (OnPlayerEnter != null)
                OnPlayerEnter.Invoke();

            if(!string.IsNullOrEmpty(description))
            {
                if(!hasInteracted)
                {
                    FindObjectOfType<InteractionCanvas>().Show(interactKeycode.ToString(), description);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<InteractionCanvas>().Hide();
        if (!isActive) return;
        if (other.gameObject.tag == "Player")
        {
            if (OnPlayerExit != null)
                OnPlayerExit.Invoke();

            if (!singleInteraction)
                hasInteracted = false;
        }
    }

    public void SetActive(bool state)
    {
        isActive = state;
    }
}
