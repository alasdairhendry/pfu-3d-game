using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_Binary : Minigame {

    [SerializeField] private RectTransform healthRect;
    [SerializeField] private RectTransform healthFill;
    private GameObject main_Panel;

    List<Minigame_BinarySet> binarySets = new List<Minigame_BinarySet>();
    Minigame_BinarySet activeSet;

    [SerializeField] private Vector2 changeDelayRange = new Vector2(1.0f, 5.0f);
    private float currentDelay = 0.0f;
    private float delayCounter = 0.0f;

    [SerializeField] private float currentHealth = 75.0f;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 11; i++)
        {
            Minigame_BinarySet set = new Minigame_BinarySet(transform.Find("Main_Panel").Find("Screen").Find("Panel").GetChild(i).GetComponentInChildren<Text>());
            binarySets.Add(set);
        }

        main_Panel = transform.Find("Main_Panel").gameObject;
        EndMinigame();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!isActive) return;

        base.Update();
        CheckInput();
        MonitorChange();
        UpdateHealth();
    }

    private void SetNew()
    {        
        if (activeSet != null)
        {            
            activeSet.SetActive(false);            
        }

        activeSet = binarySets[Random.Range(0, binarySets.Count)];
        activeSet.SetActive(true);

        currentDelay = Random.Range(changeDelayRange.x, changeDelayRange.y);
        delayCounter = 0.0f;
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Answered(activeSet.Check(0));                       
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Answered(activeSet.Check(1));
        }
    }

    private void UpdateHealth()
    {
        currentHealth -= Time.deltaTime;
        healthFill.sizeDelta = new Vector2(Mathf.Lerp(0, 1152, currentHealth / 100.0f), healthFill.sizeDelta.y);

        if(currentHealth <= 0)
        {
            OnFailed();
        }
        else if(currentHealth >= 100.0f)
        {
            OnComplete();
        }
    }

    private void Answered(bool answeredCorrect)
    {
        if (answeredCorrect)
            currentHealth += 1.5f;
        else currentHealth -= 2.0f;
    }

    private void MonitorChange()
    {
        delayCounter += Time.deltaTime;
        if(delayCounter >= currentDelay)
        {
            SetNew();
        }
    }

    public override void OnComplete()
    {
        base.OnComplete();

        EndMinigame();
    }

    public override void OnFailed()
    {
        base.OnFailed();

        EndMinigame();
    }

    public override void StartMinigame()
    {
        base.StartMinigame();

        main_Panel.SetActive(true);
        SetNew();
    }

    public override void EndMinigame()
    {
        base.EndMinigame();

        main_Panel.SetActive(false);
    }
}

public class Minigame_BinarySet
{
    private List<int> binarySet = new List<int>();    

    private Text textTarget;
    private bool isActive = false;
    public bool IsActive { get { return isActive; } }

    public Minigame_BinarySet(Text textTarget)
    {
        this.textTarget = textTarget;
        
        for (int i = 0; i < 20; i++)
        {
            binarySet.Add(Random.Range(0, 2));
        }

        UpdateText();
    }

    public void SetActive(bool state)
    {
        isActive = state;

        if(IsActive)
        {
            textTarget.transform.parent.GetComponent<Mask>().showMaskGraphic = true;
        }
        else
        {
            textTarget.transform.parent.GetComponent<Mask>().showMaskGraphic = false;
        }
    }

    public bool Check(int input)
    {
        if (binarySet[0] == input)
        {
            Debug.Log("Correct");
            RemoveAnswer();
            AddToList();
            UpdateText();
            return true;
        }
        else
        {
            Debug.Log("False");
            RemoveAnswer();
            AddToList();
            UpdateText();
            return false;
        }
    }

    private void RemoveAnswer()
    {
        binarySet.RemoveAt(0);
    }

    private void AddToList()
    {
        binarySet.Add(Random.Range(0, 2));
    }

    private void UpdateText()
    {
        string text = "";

        for (int i = binarySet.Count - 1; i > 0; i--)
        {
            text += binarySet[i].ToString("0") + "\n";
        }

        text += binarySet[0].ToString("0");

        textTarget.text = text;
    }
}
