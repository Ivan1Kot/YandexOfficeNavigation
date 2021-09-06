using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PanelSmothSlide))]
public class PanelSmoothSlideEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PanelSmothSlide myScript = (PanelSmothSlide)target;
        if (GUILayout.Button("Slide"))
        {
            myScript.CallSlide();
        } 
        if (GUILayout.Button("Extra Slide"))
        {
            myScript.CallExtraSlide();
        }
        if (GUILayout.Button("Hide"))
        {
            myScript.CallHide();
        }
    }
}