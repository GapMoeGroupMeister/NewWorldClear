using System;
using System.Collections.Generic;
using EasyJson;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class RequestInventory : MonoBehaviour
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
}
