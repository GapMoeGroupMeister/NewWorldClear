using TMPro;
using UnityEngine;

public class Start_MapManager : MonoBehaviour
{
    [SerializeField] private MapSO mapSO;
    [SerializeField] private TMP_Text mapText;
    [SerializeField] private TMP_Text mapNameText;
    [SerializeField] private TMP_Text mapDescriptionText;

    private void Start()
    {
        mapText.text = mapSO.mapName;
    }

    public void SetMapName()
    {
        mapNameText.text = mapSO.mapName;
        mapDescriptionText.text = mapSO.mapDescription;
    }
}
