using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillTreeNode : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerUpHandler, IPointerExitHandler
{
    public bool nodeSelected;
    public SkillTreeNode[] nextSkillTreeNode;

    private Image connecter = null;
    private Transform selectedTrm = null;
    private Transform frameTrm = null;

    private bool isLoading = false;

    private void Awake()
    {
        connecter = transform.Find("connecter/connecterFill").GetComponent<Image>();
        selectedTrm = transform.Find("Selected");
        frameTrm = transform.Find("Frame");

        selectedTrm.gameObject.SetActive(false);
        frameTrm.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(isLoading == true)
        {

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        frameTrm.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        frameTrm.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isLoading = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isLoading = false;
    }

    
}
