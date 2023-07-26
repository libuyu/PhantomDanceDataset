using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using LitJson;

public class Json2yaml : EditorWindow
{
    [System.Serializable]
    public class BoneData
    {
        public string boneName;
        public string bonePath;
        public Quaternion initialRotation;
    }
    public static List<BoneData> boneDataList = new List<BoneData>();

    MotionData motion_data;
    public string[] boneName;
    private static Windowseditor windowsEditor;
    public class MotionDataToAnimConverter
    {
        public static int FPS = 30;

        public static AnimationClip ConvertToAnimationClip(MotionData motionData)
        {
            AnimationClip clip = new AnimationClip();
            for (int i = 0; i < motionData.bone_name.Length; i++)
            {
                string bonePath = boneDataList[i].bonePath;
                AnimationCurve xCurve = CreateCurveFromData(motionData.rotations, i, 0);
                AnimationCurve yCurve = CreateCurveFromData(motionData.rotations, i, 1);
                AnimationCurve zCurve = CreateCurveFromData(motionData.rotations, i, 2);
                AnimationCurve wCurve = CreateCurveFromData(motionData.rotations, i, 3);

                clip.SetCurve(bonePath, typeof(Transform), "localRotation.x", xCurve);
                clip.SetCurve(bonePath, typeof(Transform), "localRotation.y", yCurve);
                clip.SetCurve(bonePath, typeof(Transform), "localRotation.z", zCurve);
                clip.SetCurve(bonePath, typeof(Transform), "localRotation.w", wCurve);

            }

            AnimationCurve rootXCurve = CreateCurveFromData(motionData.root_positions, 0);
            AnimationCurve rootYCurve = CreateCurveFromData(motionData.root_positions, 1);
            AnimationCurve rootZCurve = CreateCurveFromData(motionData.root_positions, 2);

            clip.SetCurve(boneDataList[0].bonePath, typeof(Transform), "localPosition.x", rootXCurve);
            clip.SetCurve(boneDataList[0].bonePath, typeof(Transform), "localPosition.y", rootYCurve);
            clip.SetCurve(boneDataList[0].bonePath, typeof(Transform), "localPosition.z", rootZCurve);

            return clip;
        }

        private static AnimationCurve CreateCurveFromData(double[][][] data, int boneIndex, int componentIndex)
        {
            AnimationCurve curve = new AnimationCurve();
            for (int i = 0; i < data.Length; i++)
            {
                Keyframe keyframe = new Keyframe((float)i / FPS, (float)data[i][boneIndex][componentIndex], 0, 0, 1f / 3, 1f / 3);
                curve.AddKey(keyframe);
            }
            return curve;
        }

        private static AnimationCurve CreateCurveFromData(double[][] data, int componentIndex)
        {
            AnimationCurve curve = new AnimationCurve();
            for (int i = 0; i < data.Length; i++)
            {
                Keyframe keyframe = new Keyframe((float)i / FPS, (float)data[i][componentIndex], 0, 0, 1f / 3, 1f / 3);
                curve.AddKey(keyframe);
            }
            return curve;
        }
    }
    public class MotionData
    {
        public string[] bone_name; // bone names
        public double[][][] rotations; // frame * bone_num * 4, in order of bone names
        public double[][] root_positions; // frame * 3
    }
    string BodyAnimFile;
    List<string> SMPLBoneNames = new List<string> {
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
    List<string> SMPLFatherNode = new List<string>{
            "NULL",
            "Pelvis", "Pelvis", "Pelvis",
            "L_Hip", "R_Hip", "Spine1",
            "L_Knee", "R_Knee", "Spine2",
            "L_Ankle", "R_Ankle", "Spine3",
            "Spine3", "Spine3", "Neck",
            "L_Collar", "R_Collar",
            "L_Shoulder", "R_Shoulder",
            "L_Elbow", "R_Elbow",
            "L_Wrist", "R_Wrist"
    };
    void globalRotation2localRotation()
    {
        for (int j = motion_data.bone_name.Length - 1; j >= 0; j--)
        {
            for (int i = 0; i < motion_data.rotations.Length; i++)
            {
                Quaternion rot = new Quaternion(
                    (float)(motion_data.rotations[i][j][0]),
                    (float)(motion_data.rotations[i][j][1]),
                    (float)(motion_data.rotations[i][j][2]),
                    (float)(motion_data.rotations[i][j][3])
                );
                rot = boneDataList[j].initialRotation * rot;
                motion_data.rotations[i][j][0] = rot.x;
                motion_data.rotations[i][j][1] = rot.y;
                motion_data.rotations[i][j][2] = rot.z;
                motion_data.rotations[i][j][3] = rot.w;
            }
        }
        for (int j = motion_data.bone_name.Length - 1; j >= 0; j--)
        {
            for (int i = 0; i < motion_data.rotations.Length; i++)
            {
                Quaternion rot = new Quaternion(
                    (float)(motion_data.rotations[i][j][0]),
                    (float)(motion_data.rotations[i][j][1]),
                    (float)(motion_data.rotations[i][j][2]),
                    (float)(motion_data.rotations[i][j][3])
                );
                if (SMPLFatherNode[j] != "NULL")
                {
                    for (int t = 0; t < 24; t++)
                    {
                        if (SMPLBoneNames[t] == SMPLFatherNode[j])
                        {
                            Quaternion last_rot = new Quaternion(
                                (float)(motion_data.rotations[i][t][0]),
                                (float)(motion_data.rotations[i][t][1]),
                                (float)(motion_data.rotations[i][t][2]),
                                (float)(motion_data.rotations[i][t][3])
                            );
                            rot = Quaternion.Inverse(last_rot) * rot;
                            break;
                        }
                    }
                }
                motion_data.rotations[i][j][0] = rot.x;
                motion_data.rotations[i][j][1] = rot.y;
                motion_data.rotations[i][j][2] = rot.z;
                motion_data.rotations[i][j][3] = rot.w;
            }
        }
    }
    List<Quaternion> init_rots = new List<Quaternion>();
    // Start is called before the first frame update
    public void json2anim()
    {
        windowsEditor = EditorWindow.GetWindow<Windowseditor>();
        windowsEditor.LoadFieldValues();
        BodyAnimFile = windowsEditor.inputPath;
        string filePathx = Application.dataPath + "/Animations/AnimClips/" + windowsEditor.boneMap + "_bone_map.json"; ;
        string jsonx = System.IO.File.ReadAllText(filePathx);
        JsonData jsonData = JsonMapper.ToObject(jsonx);
        for (int i = 0; i < jsonData.Count; i++)
        {
            JsonData boneJson = jsonData[i];

            BoneData boneData = new BoneData();
            boneData.boneName = (string)boneJson["boneName"];
            boneData.bonePath = (string)boneJson["bonePath"];
            boneData.initialRotation = new Quaternion(
                (float)(double)boneJson["initialRotation"]["x"],
                (float)(double)boneJson["initialRotation"]["y"],
                (float)(double)boneJson["initialRotation"]["z"],
                (float)(double)boneJson["initialRotation"]["w"]
            );

            boneDataList.Add(boneData);
        }
        StreamReader sr = new StreamReader(BodyAnimFile);
        string json = sr.ReadToEnd();
        sr.Close();
        motion_data = JsonMapper.ToObject<MotionData>(json);
        Debug.Log(motion_data.rotations.Length);
        globalRotation2localRotation();
        AnimationClip animationClip = MotionDataToAnimConverter.ConvertToAnimationClip(motion_data);
        string filePath = windowsEditor.outputPath;
        AssetDatabase.CreateAsset(animationClip, filePath);
        AssetDatabase.SaveAssets();
    }

}