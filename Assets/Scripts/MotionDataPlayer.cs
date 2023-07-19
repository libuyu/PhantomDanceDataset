using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class MotionDataPlayer : MonoBehaviour
{
    public class MotionData
    {
        public string[] name; // bone names
        public double[][][] rotations; // frame * bone_num * 4, in order of bone names
        public double[][] root_positions; // frame * 3
    }

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

    public string BodyAnimFile;
    public GameObject Root;
    public float FPS = 60;
    // public bool use_SMPL = true;

    // int BoneNum;
    MotionData motion_data;
    List<string> bone_names;
    List<Transform> bones;
    Vector3 init_root_pos;
    List<Quaternion> init_rots;
    int root_idx;
    float play_time;
    int e_frame;

    void Start()
    {
        // read json file
        StreamReader sr = new StreamReader(BodyAnimFile);
        string json = sr.ReadToEnd();
        sr.Close();
        motion_data = JsonMapper.ToObject<MotionData>(json);
        bone_names = new List<string>(motion_data.name);
        Debug.Log("FrameNum: " + motion_data.rotations.Length);

        // mapping bones
        int bone_num = bone_names.Count;
        bones = new List<Transform>();
        init_rots = new List<Quaternion>();
        Transform[] character_bones = Root.GetComponentsInChildren<Transform>();
        for (int i=0; i<bone_num; i++) {
            string cur_bone = motion_data.name[i];
            if (SMPLBoneNames.Contains(cur_bone) == false) {
                Debug.LogError("Invalid Bone Name: " + cur_bone);
                return;
            }

            for (int j=0; j<character_bones.Length; j++) {
                if (character_bones[j].name == cur_bone) {
                    bones.Add(character_bones[j]);
                    // record the initial rotation
                    init_rots.Add(character_bones[j].rotation);
                    break;
                }
            }
        }

        // find the root and its initial position
        string root_name = Root.name;
        Debug.Log("Root Name: " + root_name);
        root_idx = bone_names.IndexOf(root_name);
        if (root_idx == -1) {
            Debug.LogError("root name not found: " + root_name);
            return;
        }
        init_root_pos = bones[root_idx].position - new Vector3((float)motion_data.root_positions[0][0], 
                                                               (float)motion_data.root_positions[0][1], 
                                                               (float)motion_data.root_positions[0][2]);

        // initialize play time
        e_frame = motion_data.rotations.Length - 1;
        play_time = 0;
        Debug.Log("begin playing");
    }
 

    void SetMotionFromRot(int frame) 
    {
        Vector3 root_pos = new Vector3(
            (float)(motion_data.root_positions[frame][0]),
            (float)(motion_data.root_positions[frame][1]),
            (float)(motion_data.root_positions[frame][2])
        );
        bones[root_idx].position = init_root_pos + root_pos;

        for (int j=0; j<bones.Count; j++) {
			Quaternion rot = new Quaternion(
                (float)(motion_data.rotations[frame][j][0]),
                (float)(motion_data.rotations[frame][j][1]),
                (float)(motion_data.rotations[frame][j][2]),
                (float)(motion_data.rotations[frame][j][3])
            );
            bones[j].rotation = rot;
            // bones[j].localRotation = rot;
		}
    }

    void Update()
    {
        float dt = Time.deltaTime;
        play_time += dt;
        int frame = (int)(play_time * FPS);
        // Debug.Log("Frame: " + frame);
		if (frame >= e_frame) {
			play_time = 0; 
			frame = 0;
		}
        SetMotionFromRot(frame);
    }
}
