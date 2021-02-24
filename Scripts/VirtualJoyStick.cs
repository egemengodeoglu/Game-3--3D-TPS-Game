using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private float startTime;
    private bool control;
    public float countDownTime;
    private static VirtualJoyStick _instance;
    public static VirtualJoyStick Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<VirtualJoyStick>();
            return _instance;
        }
    }

    [Header("Touchable")]
    [SerializeField] private float joystickVisualDistance = 100f;

    private Image joystick;
    private Image background;
    private Vector3 direction;
    public Vector3 Direction { get { return direction; } }

    private void Start()
    {
        var imgs = GetComponentsInChildren<Image>();
        background = imgs[0];
        joystick = imgs[1];
        //CountDown side
        startTime = Time.time - countDownTime;
        //image. .interactable = true;
        control = true;
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / background.rectTransform.sizeDelta.x);
            pos.y = (pos.y / background.rectTransform.sizeDelta.y);
        }

        Vector2 p = background.rectTransform.pivot;
        pos.x += p.x - 0.5f;
        pos.y += p.y - 0.5f;
        /*
        float x = Mathf.Clamp(pos.x, -1, 1);
        float y = Mathf.Clamp(pos.y, -1, 1);
        */
        //direction = new Vector3(x, 0, y);
        direction = new Vector3(pos.x, 0, pos.y)* joystickVisualDistance;
        if (direction.magnitude > 45)
        {
            direction = direction.normalized * 45;
        }
        
        
        //Debug.Log(direction);
        joystick.rectTransform.anchoredPosition = new Vector3(direction.x, direction.z);

    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction = default(Vector3);
        joystick.rectTransform.anchoredPosition = default(Vector3);
    }

    public void ShootPlayer()
    {
        startTime = Time.time;
        control = true;
    }

    private void FixedUpdate()
    {
        float tmp = Time.time - startTime;
        if (tmp <= countDownTime)
        {
            background.GetComponent<Image>().fillAmount = (tmp / countDownTime);
        }
        else
        {
            if (control)
            {
                background.GetComponent<Image>().fillAmount = 1;
                control = false;
            }
        }
    }

}
