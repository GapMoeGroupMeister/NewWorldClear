using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

public class RequestPanel : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private TextMeshProUGUI ingredientText;
    [SerializeField] private Ingredients[] ingredients;
    private void OnEnable()
    {
        transform.position = new Vector3(0f, -10f, 0f);
        transform.DOMoveY(-2.5f, 0.5f).SetEase(ease);
        int randomIndex = Random.Range(0, ingredients.Length);
        ingredientText.text = 
            ingredients[randomIndex].ingredients + " 주세요\n" + 
            "<size=70><color=#000000>목표: " + ingredients[randomIndex].ingredients + " " + 
            ingredients[randomIndex].count + "개</color></size>";
    }
}
[Serializable]
public class Ingredients
{
    public string ingredients;
    public int count;
}