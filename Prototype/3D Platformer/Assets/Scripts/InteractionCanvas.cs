using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionCanvas : MonoBehaviour {

    private GameObject main_Panel;

    private void Start()
    {
        main_Panel = transform.Find("Main_Panel").gameObject;
        Hide();
    }

    public void Show(string keycode, string description)
    {
        main_Panel.SetActive(true);
        main_Panel.transform.Find("InteractionPrompt_Panel").Find("Keycode_Text").GetComponent<Text>().text = keycode;
        main_Panel.transform.Find("Description_Text").GetComponent<Text>().text = description;
    }

    public void Hide()
    {
        main_Panel.SetActive(false);
    }
}
