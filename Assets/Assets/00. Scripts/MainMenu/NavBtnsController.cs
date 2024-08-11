using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class NavBtnsController : MonoBehaviour
{
    [SerializeField]
    private HorizontalScrollSnap scrollSnap;

    [SerializeField]
    private int currentPage = 1;

    [SerializeField]
    private int maxPage = 0;

    [SerializeField]
    private Button leftBtn;
    [SerializeField]
    private Button rightBtn;
    [SerializeField]
    private List<GameObject> panelList = new List<GameObject>();

    private void Awake()
    {
        currentPage = scrollSnap.StartingScreen;
        maxPage = panelList.Count;
    }

    public void OnValueChanged()
    {
        currentPage = scrollSnap.CurrentPage;
        leftBtn.gameObject.SetActive(currentPage <= 0 ? false : true);
        rightBtn.gameObject.SetActive(currentPage < maxPage - 1 ? true : false);
    }
}
