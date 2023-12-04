using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponMarketManager))]
class WeaponMarketManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WeaponMarketManager weaponMarketManager = target as WeaponMarketManager;
        GUILayout.Space(20f);
        if (GUILayout.Button("무기 변경"))
        {
            weaponMarketManager.WeaponChange();
        }
    }
}