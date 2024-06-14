using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 기본 정보, 데이터를 담는 스크립트
public class ItemManager : MonoBehaviour
{
    #region Instance

    private static ItemManager instance;

    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject ItemManager = new GameObject("ItemManager");
                instance = ItemManager.AddComponent<ItemManager>();
                DontDestroyOnLoad(ItemManager);
            }

            return instance;
        }
    }

    #endregion

    #region Item Datas

    [System.Serializable]
    public class Magnet
    {
        public string name;
        public float remain;
        public float range;
        public float magnetism;
    }

    #endregion

    public Magnet magnet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
