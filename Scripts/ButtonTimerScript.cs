 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTimerScript : MonoBehaviour
{
    private float startTime;
    private bool control;
    public Button button;
    public float countDownTime;

    void Start()
    {
        startTime = Time.time- countDownTime;
        button.interactable = true;
        control = true;
    }
    void Update()
    {
        float tmp = Time.time - startTime;
        if (tmp <= countDownTime)
        {
            button.GetComponent<Image>().fillAmount = (tmp / countDownTime);
        }
        else
        {
            if (control)
            {
                button.GetComponent<Image>().fillAmount = 1;
                button.interactable = true;
                control = false;
            }
        }
    }

    public void clicledButton()
    {
        startTime = Time.time;
        button.interactable = false;
        control = true;
    }
}
