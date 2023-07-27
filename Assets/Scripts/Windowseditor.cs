using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Windowseditor : EditorWindow
{
    private Rect Panel;
    private static Json2yaml json2yaml;

    public string inputPath, outputPath,boneMap;


    private static string jsonPathKey = "Assets/Animations/RawData/example.json";
    private static string outputPathKey = "Assets/Animations/AnimClips/example.anim";
    private static string boneMapKey = "example";


    public static Windowseditor window;
    [MenuItem("Window/CreateAnimationClip")]
    private static void OpenWindow()
    {
        window = GetWindow<Windowseditor>();
        LoadFieldValues();
    }
    private void OnGUI()
    {
        Panel = new Rect(0, 0, position.width, position.height * 0.9f);
        GUILayout.BeginArea(Panel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("JSON Path:");
        inputPath = GUILayout.TextField(inputPath);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Output ANIM Path:");
        outputPath = GUILayout.TextField(outputPath);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Model Name:");
        boneMap = GUILayout.TextField(boneMap);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create Animation Clip!"))
        {
            SaveFieldValues();
            LoadFieldValues();
            json2yaml = new Json2yaml();
            json2yaml.json2anim();
        }

        GUILayout.EndArea();
    }

    private void OnDestroy()
    {
        SaveFieldValues();
    }

    private void SaveFieldValues()
    {
        EditorPrefs.SetString(jsonPathKey, inputPath);
        EditorPrefs.SetString(outputPathKey, outputPath);
        EditorPrefs.SetString(boneMapKey, boneMap);
    }

    public void LoadFieldValues()
    {
        inputPath = EditorPrefs.GetString(jsonPathKey, "");
        outputPath = EditorPrefs.GetString(outputPathKey, "");
        boneMap = EditorPrefs.GetString(boneMapKey, "");
    }
}