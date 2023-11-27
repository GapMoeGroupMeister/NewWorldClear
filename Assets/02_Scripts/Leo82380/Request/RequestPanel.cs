using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;
using UnityEditor;

public class RequestPanel : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private TextMeshProUGUI ingredientText;
    [SerializeField] private Ingredients[] ingredients;
    [SerializeField] private GameObject man;
    
    private int count = 0;
    private int day;

    private void OnEnable()
    {
        RequestSetup();
    }

    [ContextMenu("Test/Request Setup")]
    internal void RequestSetup()
    {
        day++;
        if(day > 3)
            day = 1;
        if (day % 3 == 0)
        {
            man.SetActive(true);
            transform.position = new Vector3(0f, -10f, 0f);
            transform.DOMoveY(-3.5f, 0.5f).SetEase(ease);
            int randomIndex = Random.Range(0, ingredients.Length);
            ingredientText.text =
                ingredients[randomIndex].ingredients + " 주세요\n" +
                "<size=70><color=#000000>목표: " + ingredients[randomIndex].ingredients + " " +
                ingredients[randomIndex].count + "개</color></size>";

            count = ingredients[randomIndex].count;
        }
        else
        {
            man.SetActive(false);
            transform.position = new Vector3(0f, -10f, 0f);
            transform.DOMoveY(-3.5f, 0.5f).SetEase(ease);
            
            ingredientText.text = "오늘은 의뢰가 없나 보네요..";
        }
        print(day);
    }
}

/// <summary>
/// 필요한 재료
/// </summary>
[Serializable]
public class Ingredients
{
    public string ingredients;
    public int count;
}

[CustomEditor(typeof(RequestPanel))]
class RequestInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20f);
        GUILayout.Label("Button");
        if (GUILayout.Button("Request Random Setup"))
        {
            var requestPanel = target as RequestPanel;
            requestPanel.RequestSetup();
        }
    }
}