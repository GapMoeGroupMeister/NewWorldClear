using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Start_MapManager : MonoBehaviour
{
    [SerializeField] private MapSO mapSO;
    [SerializeField] private TMP_Text mapNameText;
    [SerializeField] private TMP_Text mapDescriptionText;
    
    public void SetMapName()
    {
        mapNameText.text = mapSO.mapName;
        mapDescriptionText.text = mapSO.mapDescription;
    }
}
