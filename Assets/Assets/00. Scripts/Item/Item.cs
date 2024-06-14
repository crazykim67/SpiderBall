using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public bool isPassive = false;
    public bool isAct = false;

    public abstract void OnItem();

    // 쿨타임 시작
    public abstract void OnCoolTimeStart();

    // 아이템 사용 종료 및 비활성화
    public abstract void OnItemEnd();

    public abstract void ItemEffect();
}
