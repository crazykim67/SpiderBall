using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HookCount : MonoBehaviour
{
    [SerializeField]
    private HookController hookController;

    [SerializeField]
    private TMP_Text countText;

    // 8 이 기본값

    private void Update() => this.transform.position = hookController.transform.position;

    public void SubCount()
    {
        if (hookController.hookCount <= 0)
            return;

        hookController.hookCount--;

        countText.text = $"Hook : {hookController.hookCount.ToString()}";
    }

    public void AddCount(int _num)
    {
        hookController.hookCount += _num;
        countText.text = $"Hook : {hookController.hookCount.ToString()}";
    }

    public bool GetCount()
    {
        if (hookController.hookCount <= 0)
            return false;
        else
            return true;
    }
}
