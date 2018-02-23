using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTriggerGroup : MonoBehaviour {

    [SerializeField] UnityEvent OnChildrenComplete;
    Dictionary<InteractionTrigger, bool> children = new Dictionary<InteractionTrigger, bool>();

    private void Start()
    {
        SetChildren();
    }

    private void SetChildren()
    {
        InteractionTrigger[] _children = GetComponentsInChildren<InteractionTrigger>();

        foreach (InteractionTrigger item in _children)
        {
            children.Add(item, false);
        }
        Debug.Log(children.Count);
    }

    public void SetChildComplete(GameObject child)
    {
        children[child.GetComponent<InteractionTrigger>()] = true;
        CheckComplete();
    }

    private void CheckComplete()
    {
        foreach (KeyValuePair<InteractionTrigger, bool> item in children)
        {
            if(!item.Value)
            {
                return;
            }
        }

        OnChildrenComplete.Invoke();
    }
}
