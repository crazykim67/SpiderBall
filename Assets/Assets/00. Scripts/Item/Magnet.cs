using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Magnet : Item
{
    [HideInInspector]
    public ItemController controller;

    private CircleCollider2D coll;
    [SerializeField]
    // 남은 지속 시간
    private float remainTime;
    // 범위
    private float range;
    // 자력
    private float magnetism;

    [SerializeField]
    private float time;

    private void Awake() => OnItem();

    private void Update() => OnCoolTimeStart();

    public override void ItemEffect()
    {

    }

    public override void OnCoolTimeStart()
    {
        if (!isAct)
        {
            time = 0f;
            return;
        }

        if(time < remainTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            OnItemEnd();
            time = 0f;
            isAct = false;
            return;
        }
    }

    // 아이템 기본 세팅
    public override void OnItem()
    {
        var magnet = ItemManager.Instance.magnet;

        isPassive = true;
        isAct = true;

        coll = GetComponent<CircleCollider2D>();
        coll.isTrigger = true;
        coll.radius = magnet.range;
        remainTime = magnet.remain;
        range = magnet.range;
        magnetism = magnet.magnetism;
    }

    public override void OnItemEnd()
    {
        controller.OnRemoveItem(this);
    }
}
