using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Passive Items List")]
    public List<Item> passiveItemList = new List<Item>();

    [Header("Active Item List")]
    public List<Item> activeItemList = new List<Item>();

    #region Add & Remove

    public void OnAddItem(Item _item)
    {
        if (_item.isPassive)
            passiveItemList.Add(_item);
        else
            activeItemList.Add(_item);
    }

    public void OnRemoveItem(Item _item)
    {
        if (_item.isPassive)
            passiveItemList.Remove(_item);
        else
            activeItemList.Remove(_item);

        Destroy(_item.gameObject);
    }

    #endregion
}
