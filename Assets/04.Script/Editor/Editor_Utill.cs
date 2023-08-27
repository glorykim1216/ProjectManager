#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Editor_Utill
{
    static public void Folder(ref bool _folder, string _name, Color _color)
    {
        GUIStyle style = new GUIStyle(EditorStyles.foldout);
        style.fontSize = 12;
        style.fontStyle = FontStyle.Bold;

        GUI.color = _color;
        _folder = EditorGUILayout.Foldout(_folder, _name, style);
        GUI.color = Color.white;
    }

    static public void IndentUp(int indent = 1)
    {
        EditorGUI.indentLevel += indent;
    }
    static public void IndentDown(int indent = 1)
    {
        EditorGUI.indentLevel -= indent;
    }

    static private float originLabelwidth = -1;
    static public void SetLabelWidth(float width)
    {
        if (originLabelwidth == -1)
            originLabelwidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = width;
    }
    static public void ResetLabelWidth()
    {
        EditorGUIUtility.labelWidth = originLabelwidth;
        originLabelwidth = -1;
    }
    static public void ColorLabel(string label, Color color)
    {
        var prev = GUI.color;
        GUI.color = color;
        GUILayout.Label(label);
        GUI.color = prev;
    }
    static public bool ColorLabelButton(string label, Color color, float width = 0)
    {
        var prev = GUI.color;
        GUI.color = color;
        bool button;
        if (width == 0)
            button = GUILayout.Button(label, "Label");
        else
            button = GUILayout.Button(label, "Label", GUILayout.Width(width));
        GUI.color = prev;
        return button;
    }
    static public bool ColorToggle(string label, bool Toggle, Color color)
    {
        var prev = GUI.color;
        GUI.color = color;
        Toggle = EditorGUILayout.Toggle(label, Toggle);
        GUI.color = prev;
        return Toggle;
    }
}

#endif
