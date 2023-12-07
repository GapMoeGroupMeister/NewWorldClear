using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

public class RequestPanel : MonoBehaviour
{
    [Header("ingredients")]
    [SerializeField] private Ease ease;
    [SerializeField] private TextMeshProUGUI ingredientText;
    [SerializeField] private List<RequestSO> ingredients;
    [SerializeField] private GameObject man;

    [Space]
    [Header("Request Setup")]
    [SerializeField] private GameObject acceptButton;
    [SerializeField] private GameObject requestPanel;
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private TMP_Text reservesText;
    [SerializeField] private TMP_Text resultText;

    [SerializeField] private float duration = 0.5f;
    
    private int count;
    private int day;
    private int randomIndex;
    private RequestSO nowRequest;

    private void OnEnable()
    {
        RequestSetup();
        requestPanel.transform.position = new Vector3(0, -10f);
        resultText.text = string.Empty;
    }

    [ContextMenu("Test/Request Setup")]
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
            ingredientText.text =
                ingredients[randomIndex].item.itemName + " 주세요\n" +
                "<size=70><color=#000000>목표: " + ingredients[randomIndex].item.itemName + " " +
                ingredients[randomIndex].amount + "개</color></size>";

            count = ingredients[randomIndex].amount;
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

    public void Request()
    {
        requestPanel.transform.DOMoveY(0, duration).SetEase(ease);
        
        targetText.text = ingredients[randomIndex].item.itemName + " " + ingredients[randomIndex].amount + "개";
        reservesText.text = ingredients[randomIndex].item.itemName + " " + 1 + "개";
    }

    public void Pass()
    {
        if (ItemManager.Instance.FindItem(nowRequest.item) == null) return;
        if (ItemManager.Instance)
        {
            resultText.text = "보유량이 충분하지 않습니다!";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = string.Empty;
            
            requestPanel.transform.DOMoveY(-10f, duration).SetEase(ease);
            ingredientText.text = "감사합니다!\n<color=black><size=70>버튼을 눌러 돌아가기</size></color>";
            acceptButton.SetActive(false);
        }
    }
}
