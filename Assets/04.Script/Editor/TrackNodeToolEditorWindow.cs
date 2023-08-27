#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
using System.IO;

public class TrackNodeToolEditorWindow : EditorWindow
{
    public static TrackNodeToolEditorWindow editor;
    public static string _nodePath = $"{Application.dataPath}/Resources/Node";
    public static string _mapPath = $"Assets/Resources/Map";

    public static GameObject _map;
    public static NodeEditor _nodeEditor;

    [MenuItem("Tools/Track Node Editor %#t")]
    public static void OpenTool()
    {
        SceneView.CameraMode cameraMode;
        cameraMode.drawMode = DrawCameraMode.Wireframe;
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/TrackNodeTool.unity");
        editor = EditorWindow.GetWindow<TrackNodeToolEditorWindow>();
        editor.minSize = new Vector2(530, 450);

        SceneView.lastActiveSceneView.sceneViewState.showSkybox = false;
        _nodeEditor = GameObject.Find("NodeEditor").GetComponent<NodeEditor>();
    }

    public void OnGUI()
    {
        DrawLoadGUI();
        DrawCreateGUI();
    }

    private bool _uiFolder = true;
    private int _selecteIndex = 0;
    private int _loadIndex = 0;
    void DrawLoadGUI()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        Editor_Utill.Folder(ref _uiFolder, "Load Node", Color.cyan);
        if (_uiFolder == false) return;

        Editor_Utill.IndentUp(2);
        Editor_Utill.SetLabelWidth(200);

        List<string> strId = new List<string>();
        List<int> strIndex = new List<int>();

        DirectoryInfo directoryInfo = new DirectoryInfo(_nodePath);
        DirectoryInfo[] array = directoryInfo.GetDirectories();
        for (int i = 0; i < array.Length; i++)
        {
            strId.Add($"{array[i].Name}");
            strIndex.Add(i);
        }

        _selecteIndex = EditorGUILayout.IntPopup("Node Name", _selecteIndex, strId.ToArray(), strIndex.ToArray());

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Node", GUILayout.Width(250)) == true)
        {
            _loadIndex = _selecteIndex;
            var strSplit = strId[_loadIndex].Split('_');
            string mapName = $"Map_{strSplit[^1]}.prefab";
            GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>($"{_mapPath}/{mapName}");
            if (go != null && _nodeEditor.Load(strId[_loadIndex]))
            {
                _map = Instantiate(go);
            }
            else
            {
                EditorUtility.DisplayDialog("Warning", "Node 로드에 실패하였습니다.", "확인");
            }
        }

        if (GUILayout.Button("Clear Node", GUILayout.Width(250)) == true)
        {
            if (EditorUtility.DisplayDialog("Warning", "Node를 모두 제거하시겠습니까?", "Ok", "Cancel") == true)
            {
                // 초기화
                OpenTool();
            }
        }
        GUILayout.EndHorizontal();

        if (_map != null)
        {
            GUIStyle TextFieldStyles = new GUIStyle(EditorStyles.label);
            //Value Color
            TextFieldStyles.normal.textColor = Color.yellow;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField($"Load Node : {strId[_loadIndex]}", TextFieldStyles);
            EditorGUILayout.Space();
            if (GUILayout.Button("Save Node", GUILayout.Width(250)) == true)
            {
                if (EditorUtility.DisplayDialog("Warning", $"{strId[_loadIndex]}을 저장 하시겠습니까?", "Ok", "Cancel") == true)
                {
                    _nodeEditor.Save(strId[_loadIndex]);
                }
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        Editor_Utill.ResetLabelWidth();

        //DrawUsingGUI();

        Editor_Utill.IndentDown(2);

    }


    bool _newUIFolder = false;
    string _uiName = string.Empty;

    void DrawCreateGUI()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        Editor_Utill.Folder(ref _newUIFolder, "Create New Node", Color.cyan);

        if (_newUIFolder == false) return;

        Editor_Utill.IndentUp(2);


        string nodeName = EditorGUILayout.TextField("Node Name", _uiName);
        if (string.IsNullOrEmpty(nodeName) == false)
        {
            if (GUILayout.Button("Create Node", GUILayout.Width(250)))
            {
                if (Directory.Exists($"{_nodePath}/{nodeName}"))
                {
                    EditorUtility.DisplayDialog("Warning", $"{nodeName} 폴더가 이미 존재합니다.", "Ok");
                }
                else
                {
                    Directory.CreateDirectory($"{_nodePath}/{nodeName}");

                    _nodeEditor.Save(nodeName);

                    AssetDatabase.Refresh();
                }
            }

            _uiName = nodeName;
        }
        else
        {
            _uiName = string.Empty;
        }

        Editor_Utill.IndentDown(2);
    }
}

#endif