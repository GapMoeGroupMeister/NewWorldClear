using System;
using System.Collections.Generic;
using EasyJson;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class RequestInventory : MonoBehaviour, IPointerClickHandler
{
    private RequestPanel requestPanel;
    private int requestID;
    [SerializeField] private List<RequestSO> requestList;
    private RequestSO nowRequest;

    private void Awake()
    {
        requestPanel = FindObjectOfType<RequestPanel>();
    }

    private void OnEnable()
    {
        requestID = DBManager.Get_UserInfo().nowRequestId;
        nowRequest = requestList[requestID];
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (ItemManager.Instance.FindItem(nowRequest.requests[0].item) == null) return;
        if (ItemManager.Instance.FindItem(nowRequest.requests[1].item) == null) return;
        if (ItemManager.Instance.SubItem(nowRequest.requests[0].item, 0))
        {
            requestPanel.GiveAmount[0]++;
            requestPanel.RequestTextSetup();
        }
        if (ItemManager.Instance.SubItem(nowRequest.requests[1].item, 0))
        {
            requestPanel.GiveAmount[1]++;
            requestPanel.RequestTextSetup();
        }
    }
}
