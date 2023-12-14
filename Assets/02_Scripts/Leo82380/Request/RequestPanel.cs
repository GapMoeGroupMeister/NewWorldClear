using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using EasyJson;

public class RequestPanel : MonoBehaviour
{
    [Header("ingredients")]
    [SerializeField] private Ease ease;
    [Tooltip("요청할 재료가 뜨는 텍스트")]
    [SerializeField] private TextMeshProUGUI ingredientText;
    [Tooltip("요청할 재료들")]
    [SerializeField] private List<RequestSO> ingredients;
    [Tooltip("의뢰인")]
    [SerializeField] private GameObject man;

    [Space]
    [Header("Request Setup")]
    [Tooltip("의뢰 수락 버튼")]
    [SerializeField] private GameObject acceptButton;
    [Tooltip("의뢰 패널")]
    [SerializeField] private GameObject requestPanel;
    [Tooltip("요청 아이템 이미지")]
    [SerializeField] private Image[] ingredientImage;
    // 의뢰 갯수 텍스트
    [SerializeField] private TMP_Text requestAmountText1;
    [SerializeField] private TMP_Text requestAmountText2;
    // 준 갯수 텍스트
    [SerializeField] private TMP_Text giveAmountText1;
    [SerializeField] private TMP_Text giveAmountText2;
    // 요청 아이템 이미지
    [SerializeField] private Image requestImage1;
    [SerializeField] private Image requestImage2;
    // 보유 갯수 부족 매터리얼
    [SerializeField] private Material lackMaterial;

    [SerializeField] private float duration = 0.5f;
    
    private int count;
    private int day;
    private int randomIndex;
    private RequestSO nowRequest;
    private int[] giveAmount = new int[2];
    private int ID;
    private SaveInfo saveInfo;

    public int[] GiveAmount
    {
        get => giveAmount;
        set => giveAmount = value;
    }
    
    private void Start()
    {
        saveInfo = DBManager.Get_UserInfo();
    }

    private void OnEnable()
    {
        requestPanel.transform.position = new Vector3(0, -10f);
        RequestSetup();
    }

    public void RequestSetup()
    {
        
        day++;
        if(day > 3)
            day = 1;
        if (day % 3 == 0)
        {
            man.SetActive(true);
            transform.position = new Vector3(0f, -10f, 0f);
            transform.DOMoveY(-3.5f, duration).SetEase(ease);
            randomIndex = Random.Range(0, ingredients.Count);
            nowRequest = ingredients[randomIndex];
            var request = nowRequest.requests;
            ingredientText.text = nowRequest.request + "\n<size=40><color=#000000>목표: " + request[0].item.itemName + " " +
                                  ingredients[randomIndex].requests[0].amount + "개, "  +
                                  request[1].item.itemName + ingredients[randomIndex].requests[1].amount
                                  + "개</color></size>";
            saveInfo.nowRequestId = nowRequest.requestID;
            DBManager.Save_userInfo(saveInfo);
            requestAmountText1.text = "X" + request[0].amount;
            requestAmountText2.text = "X" + request[1].amount;
            RequestTextSetup();
            count = request[0].amount;
            acceptButton.SetActive(true);
        }
        else
        {
            man.SetActive(false);
            transform.position = new Vector3(0f, -10f, 0f);
            transform.DOMoveY(-3.5f, duration).SetEase(ease);
            acceptButton.SetActive(false);
            
            ingredientText.text = "오늘은 의뢰가 없나 보네요..";
        }
        print(day);
    }

    public void RequestTextSetup()
    {
        RequestMaterialSetup(0);
        giveAmountText1.text = giveAmount[0] + "/" + nowRequest.requests[0].amount;
        RequestMaterialSetup(1);
        giveAmountText2.text = giveAmount[1] + "/" + nowRequest.requests[1].amount;
    }
    
    private void RequestMaterialSetup(int index)
    {
        if (giveAmount[index] >= nowRequest.requests[index].amount)
        {
            giveAmountText1.color = Color.green;
            requestImage1.material = null;
        }
        else
        {
            giveAmountText1.color = Color.red;
            requestImage1.material = lackMaterial;
        }
    }

    public void Request()
    {
        #nullable enable
        ItemSlot? slot = ItemManager.Instance.FindItem(nowRequest.requests[0].item) 
                         ?? ItemManager.Instance.FindItem(nowRequest.requests[0].item);
        int? amount = slot.amount;
        requestPanel.transform.DOMoveY(0, duration).SetEase(ease);
        print(slot.item);
    }

    public void Pass()
    {
        if (ItemManager.Instance.FindItem(nowRequest.requests[0].item) == null) return;
        if (ItemManager.Instance.SubItem(nowRequest.requests[0].item, nowRequest.requests[0].amount))
        {
            requestPanel.transform.DOMoveY(-10f, duration).SetEase(ease);
            ingredientText.text = "감사합니다!\n<color=black><size=50>버튼을 눌러 돌아가기</size></color>";
            acceptButton.SetActive(false);
        }
        else
        {
            ingredientText.text = "재료가 부족합니다!\n<color=black><size=50>버튼을 눌러 돌아가기</size></color>";
            acceptButton.SetActive(false);
        }
    }
    
    public void RequestPanelClose()
    {
        requestPanel.transform.DOMoveY(-10f, duration).SetEase(ease);
    }
}
