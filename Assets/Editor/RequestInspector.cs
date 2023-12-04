using UnityEditor;
using UnityEngine;

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