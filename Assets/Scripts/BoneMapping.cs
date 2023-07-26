using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEditor;

public class BoneMapping : MonoBehaviour
{
    public List<string> SMPLBoneNames = new List<string> {
        "Pelvis",
        "L_Hip", "R_Hip", "Spine1",
        "L_Knee", "R_Knee", "Spine2",
        "L_Ankle", "R_Ankle", "Spine3",
        "L_Foot", "R_Foot", "Neck",
        "L_Collar", "R_Collar", "Head",
        "L_Shoulder", "R_Shoulder",
        "L_Elbow", "R_Elbow",
        "L_Wrist", "R_Wrist",
        "L_Hand", "R_Hand"
    };

    [System.Serializable]
    public class BoneData
    {
        public string boneName;
        public GameObject boneGameObject;
    }

    public List<BoneData> boneDataList = new List<BoneData>();

    private Dictionary<string, GameObject> boneGameObjects = new Dictionary<string, GameObject>();

    private Dictionary<string, string> bonePaths = new Dictionary<string, string>();
    private Dictionary<string, Quaternion> initialRotations = new Dictionary<string, Quaternion>();

    private void Reset()
    {
        // 初始化 boneDataList 为长度为24的列表，并使用 SMPLBoneNames 来初始化 boneName 字段
        boneDataList.Clear();
        for (int i = 0; i < SMPLBoneNames.Count; i++)
        {
            BoneData boneData = new BoneData();
            boneData.boneName = SMPLBoneNames[i];
            boneDataList.Add(boneData);
        }
    }
    void Start()
    {
        // 设置骨骼对应的Gameobject
        foreach (BoneData boneData in boneDataList)
        {
            string boneName = boneData.boneName;
            GameObject boneGameObject = boneData.boneGameObject;

            if (boneGameObject != null)
            {
                boneGameObjects[boneName] = boneGameObject;
                bonePaths[boneName] = GetGameObjectPath(boneGameObject);
                initialRotations[boneName] = boneGameObject.transform.rotation;
            }
        }

        SaveBoneDataToJson();
    }
    private string GetGameObjectPath(GameObject obj)
    {
        string objectName = gameObject.name;
        string path = "/" + obj.name;
        while (obj.transform.parent.name != objectName)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        path = path.TrimStart('/');
        return path;
    }

    private void SaveBoneDataToJson()
    {
        JsonData jsonData = new JsonData();

        foreach (BoneData boneData in boneDataList)
        {
            string boneName = boneData.boneName;
            GameObject boneGameObject = boneData.boneGameObject;

            if (boneGameObject != null)
            {
                boneGameObjects[boneName] = boneGameObject;
                bonePaths[boneName] = GetGameObjectPath(boneGameObject);
                initialRotations[boneName] = boneGameObject.transform.rotation;
            }

            JsonData boneJson = new JsonData();
            boneJson["boneName"] = boneName;
            boneJson["bonePath"] = bonePaths[boneName];
            Quaternion initialRotation = initialRotations[boneName];
            JsonData initialRotationJson = new JsonData();
            initialRotationJson["x"] = initialRotation.x;
            initialRotationJson["y"] = initialRotation.y;
            initialRotationJson["z"] = initialRotation.z;
            initialRotationJson["w"] = initialRotation.w;
            boneJson["initialRotation"] = initialRotationJson;

            jsonData.Add(boneJson);
        }

        string json = jsonData.ToJson();
        string filePath = Application.dataPath + "/Characters/"+this.name+"_bone_map.json";
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("Bone data saved to JSON file: " + filePath);
    }
}

[CustomEditor(typeof(BoneMapping))]
public class BoneMappingEditor : Editor
{
    private SerializedProperty boneDataListProperty;

    private void OnEnable()
    {
        boneDataListProperty = serializedObject.FindProperty("boneDataList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(boneDataListProperty, true);

        serializedObject.ApplyModifiedProperties();
    }
}
