using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Butt : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float input;
    public float sensitility = 3;
    bool pressing;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressing = false;
    }


    void Update()
    {
        if (pressing)
        {
            input += Time.deltaTime * sensitility;
        }
        else
        {
            input -= Time.deltaTime * sensitility;
        }
        input = Mathf.Clamp(input, 0, 1);
    }
}
