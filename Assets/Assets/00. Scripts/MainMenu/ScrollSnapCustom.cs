using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ScrollSnapCustom : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private List<Button> buttons = new List<Button>();

    private void Update()
    {
        if (buttons.Count <= 0)
            return;

        foreach(var btn in buttons)
        {
            if(EventSystem.current.currentSelectedGameObject == btn.gameObject)
            {
                scrollRect.enabled = false;
                break;
            }
            else
                scrollRect.enabled = true;
        }
    }
}
