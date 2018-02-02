using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectionCanvas : MonoBehaviour {

    [SerializeField] private GameObject inspection_Panel;
    [SerializeField] private Text header_Text;
    [SerializeField] private GameObject leftright_Toggle;
    [SerializeField] private GameObject updown_Toggle;
    [SerializeField] private GameObject frontback_Toggle;
    [SerializeField] private GameObject pushable_Toggle;
    [SerializeField] private GameObject freezable_Toggle;

    private void Start()
    {
        Hide();
    }

    public void Show(string name, bool leftright, bool updown, bool frontback, bool pushable, bool freezable)
    {
        if (!leftright && !updown && !frontback && !pushable && !freezable)
            return;

        inspection_Panel.SetActive(true);
        header_Text.text = name;
        leftright_Toggle.SetActive(leftright);
        updown_Toggle.SetActive(updown);
        frontback_Toggle.SetActive(frontback);
        pushable_Toggle.SetActive(pushable);
        freezable_Toggle.SetActive(freezable);
    }

    public void Hide()
    {
        inspection_Panel.SetActive(false);
    }

}
