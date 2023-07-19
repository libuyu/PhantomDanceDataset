# PhantomDance Dataset
The official Dataset proposed in "DanceFormer: Music Conditioned 3D Dance Generation with Parametric Motion Transformer” [AAAI 2022]. 

**Table of Contents**
- [PhantomDance Dataset](#phantomdance-dataset)
  - [Introduction](#introduction)
  - [Dataset Download](#dataset-download)
  - [Data Format](#data-format)
  - [Toolkit Usage](#toolkit-usage)
  - [Acknowledgement](#acknowledgement)
  - [Citation](#citation)

## Introduction
The PhantomDance dataset, as the first dance dataset crafted by professional animators, has 260 dance-music pairs with `todo`-hour length in total.

A [Unity3D Toolkit](#toolkit-usage) for data visualization (animation playing) is also provided in this repo.

## Dataset Download

Google Drive: [link]() `todo`

Baidu Netdisk: [link]() `todo`

## Data Format
For convenient usage, original animation curves in AutoDesk FBX format are converted to float arrays saved in JSON format with frame rate of 30fps. And the corresponding music sequences are saved in the WAV format.

The human skeleton follows the definition of [SMPL](https://smpl.is.tue.mpg.de/) with 24 joints. So a human pose is represented as root position and 24 joint rotations. The 3D position uses the unit of meter, and the rotations use quaternion representation. That is, a T-frame-length motion sequence have T * (3 + 4 * 24) float values. All the positions and rotations use world coordinates in Unity3D (x-right, y-up, z-forward). You can read [Unity official docs](https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html) for details.

If you are not familiar with the mathematics of 3D space 3D transformation and quaternion, we recommend you to read this [tutorial](http://web.mit.edu/2.998/www/QuaternionReport1.pdf). Since many 3D animation softwares describe animation data in local coordinates, we also provide script to convert the joint rotations from world coordinates to local coordinates in the toolkit.



## Toolkit Usage

### Structure

This is an Asset project folder for Unity 2022.3.2f1c1.

Structure for:

- Asset
  - Animations
  - Plugins
  - Resources
  - Scenes
  - Scripts

Animations:Demonstrate the standard model used for action datasets.

Plugins:Plugins used in the project, related to json parsing.

Resources:Some sample action data.

Scenes:A sample scene for playing actions and music in Unity.

Scripts:Data visualisation scripts

### Usage

1. Install Unity and Toolkit
   - Unity Download: https://unity.com/download
   - Toolkit install: 
      - Create a new Unity project.
      - Download Toolkit:
      ```
      $ git clone https://github.com/libuyu/PhantomDanceDataset.git
      ```
      - Replace the Unity project's Asset folder with toolkit's Asset folder. 

2. Configure scenes
   - Drag and drop the model under Animations into the scene or or import your own models.
   - Add Timeline to the model and create a Animation Track.
   - If need, create a Audio Track for music.

3. Bone Mapping
   - Since the skeletal structure of the model is different from the naming, you need to manually match the bones when using it.
   - In Inspector, find the BoneMapping component, following the SMPL model, drag the bone Gameobject of the model you are using into the Bone Gameobject field of the bone element in the Bone Data List.

4. Setting the input and output paths
   - Open the "Windows" edit box of the Unity editor, find the "Bone mapping" editor and open it.
   - Fill in the input and output paths as directed
   - Click "Switch" to convert the json data in the dataset to .anim file.

5. Playing an animation using the generated .anim file
   - Drag the animation file into the Animation Track of the Timeline.
   - Drag music files into Audio Track if needed.

Now you can visualise the animation playback details in the Timeline and Scene views.


## Acknowledgement
Thanks to [Yongxiang](https://github.com/Qedsama) for his contribution in the Unity3D toolkit.

## Citation
```
@inproceedings{li2022danceformer,
  title={Danceformer: Music conditioned 3d dance generation with parametric motion transformer},
  author={Li, Buyu and Zhao, Yongchi and Zhelun, Shi and Sheng, Lu},
  booktitle={Proceedings of the AAAI Conference on Artificial Intelligence},
  volume={36},
  number={2},
  pages={1272--1279},
  year={2022}
}
```