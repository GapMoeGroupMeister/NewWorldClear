using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RequestPanel : MonoBehaviour
{
    [Header("ingredients")] [SerializeField]
    private Ease ease;

    [Tooltip("요청할 재료가 뜨는 텍스트")] [SerializeField]
    private TextMeshProUGUI ingredientText;

    [Tooltip("요청할 재료들")] [SerializeField] private List<RequestSO> ingredients;
    [Tooltip("의뢰인")] [SerializeField] private GameObject man;

    [Space] [Header("Request Setup")] [Tooltip("의뢰 수락 버튼")] [SerializeField]
    private GameObject acceptButton;

    [Tooltip("의뢰 패널")] [SerializeField] private GameObject requestPanel;

    [Tooltip("요청 아이템 이미지")] [SerializeField]
    private Image[] ingredientImage;

    // 의뢰 갯수 텍스트
    [SerializeField] private TMP_Text requestAmountText1;

    [SerializeField] private TMP_Text requestAmountText2;

    // 준 갯수 텍스트
    [SerializeField] private TMP_Text[] giveAmountText;

    // 요청 아이템 이미지
    [SerializeField] private Image[] requestImage;

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
        if (day > 3)
            day = 1;
        if (day % 3 == 0)
        {
            man.SetActive(true);
            RequestPositionSetUp();
            randomIndex = Random.Range(0, ingredients.Count);
            nowRequest = ingredients[randomIndex];
            RequestTextSetUp();
            RequestSave();
            RequestTextSetup();

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
        RequestMaterialSetup(1);
    }

    private void RequestMaterialSetup(int index)
    {
        if (giveAmount[index] >= nowRequest.requests[index].amount)
        {
            giveAmountText[index].color = Color.green;
            requestImage[index].material = null;
        }
        else
        {
            giveAmountText[index].color = Color.red;
            requestImage[index].material = lackMaterial;
        }

        giveAmountText[index].text = giveAmount[index] + "/" + nowRequest.requests[index].amount;
    }

    private void RequestPositionSetUp()
    {
        transform.position = new Vector3(0f, -10f, 0f);
        transform.DOMoveY(-3.5f, duration).SetEase(ease);
    }

    private void RequestTextSetUp()
    {
        var request = nowRequest.requests;
        ingredientText.text = nowRequest.request + "\n<size=40><color=#000000>목표: " + request[0].item.itemName + " " +
                              ingredients[randomIndex].requests[0].amount + "개, " +
                              request[1].item.itemName + ingredients[randomIndex].requests[1].amount
                              + "개</color></size>";
        requestAmountText1.text = "X" + request[0].amount;
        requestAmountText2.text = "X" + request[1].amount;
        count = request[0].amount;
    }

    private void RequestSave()
    {
        saveInfo.nowRequestId = nowRequest.requestID;
        DBManager.Save_userInfo(saveInfo);
    }

    public void Request()
    {
        #nullable enable
        int? amount = FindItemSlot(nowRequest.requests[0].item)?.amount;
        requestPanel.transform.DOMoveY(0, duration).SetEase(ease);
        IngredientImageSetup(0, 0);
        IngredientImageSetup(1, 0);
        IngredientImageSetup(2, 1);
        IngredientImageSetup(3, 1);
    }

    private void IngredientImageSetup(int imageIndex, int requestIndex)
    {
        ingredientImage[imageIndex].sprite =
            SpriteLoader.Instance.FindSprite(nowRequest.requests[requestIndex].item.itemSpriteName);
        ingredientImage[imageIndex].SetNativeSize();
        ingredientImage[imageIndex].transform.localScale *= 0.8f;
        if (imageIndex is 1 or 3)
            ingredientImage[imageIndex].transform.localScale *= 0.7f;
    }

    public void Pass()
    {
        if (FindItemSlot(nowRequest.requests[0].item) == null) return;
        if (FindItemSlot(nowRequest.requests[0].item) == null) return;
        if (ItemManager.Instance.CountItem(nowRequest.requests[0].item) > nowRequest.requests[0].amount &&
            ItemManager.Instance.CountItem(nowRequest.requests[1].item) > nowRequest.requests[1].amount)
        {
            if (ItemManager.Instance.SubItem(nowRequest.requests[0].item, nowRequest.requests[0].amount)) 
            { 
                if (ItemManager.Instance.SubItem(nowRequest.requests[1].item, nowRequest.requests[1].amount)) 
                { 
                    StartCoroutine(OnRequestSuccess());
                }
            }

        }
        else
        {
            print(false);
            print(nowRequest.requests[0].amount);
            print(nowRequest.requests[1].amount);
        }
    }

    private IEnumerator OnRequestSuccess()
    {
        giveAmount[0] += nowRequest.requests[0].amount;
        giveAmount[1] += nowRequest.requests[1].amount;
        
        RequestTextSetup();
        yield return new WaitForSeconds(0.5f);
        print(true);
        requestPanel.transform.DOMoveY(-10f, duration).SetEase(ease);
        ingredientText.text = $"감사합니다!\n<size=40>보상: {nowRequest.reward.item.itemName} {nowRequest.reward.amount}개</size>\n<color=black><size=50>버튼을 눌러 돌아가기</size></color>";
        ItemManager.Instance.AddItem(nowRequest.reward, nowRequest.reward.amount);
        acceptButton.SetActive(false);
    }
    
    private ItemSlot? FindItemSlot(Item item)
    {
        return ItemManager.Instance.FindItem(item);
    }
    
    public void RequestPanelClose()
    {
        requestPanel.transform.DOMoveY(-10f, duration).SetEase(ease);
    }
}
