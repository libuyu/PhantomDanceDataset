# PhantomDance Dataset

The official Dataset proposed in "DanceFormer: Music Conditioned 3D Dance Generation with Parametric Motion Transformer” [AAAI 2022]. 

<table class="center">
<tr>
  
  <td><img src="https://huiye-tech.github.io/files/ChineseClassic.gif"></td>
  <td><img src="https://raw.githubusercontent.com/libuyu/libuyu.github.io/master/files/Otaku.gif"></td>
</tr>
<tr>
  <td><img src="https://raw.githubusercontent.com/libuyu/libuyu.github.io/master/files/Jazz.gif"></td>
  <td><img src="https://huiye-tech.github.io/files/HipHop.gif"></td>
</tr>
</table>

**Table of Contents**
- [Introduction](#introduction)
- [Dataset Download](#dataset-download)
- [Data Format](#data-format)
- [Toolkit](#toolkit)
- [Acknowledgement](#acknowledgement)
- [Citation](#citation)

## Introduction
The PhantomDance dataset is the first dance dataset crafted by professional animators. The released version (v1.2) has 260 dance-music pairs with 9.5-hour length in total.

A [Unity3D Toolkit](#toolkit) for data visualization (animation playing) is also provided in this repo.

## Dataset Download

- Google Drive: [Download Link](https://drive.google.com/file/d/1cDLsniPSXDSkuXPXosf6A8ybglz6adH8/view?usp=sharing)

- Baidu NetDisk: [Download Link](https://pan.baidu.com/s/1eXRlvSQkJn7-fhLHnzEVPQ?pwd=44d2)


## Data Format
For convenient usage, original animation curves in AutoDesk FBX format are converted to float arrays saved in JSON format with frame rate of 30fps. And the corresponding music sequences are saved in the WAV format with the sample rate of 16 kHz. The structure of a motion JSON file is:
- **bone_name** [list of string]: the bone names of the skeletal rig; the dimension is N, the number of bones.
- **root_positions** [2d array of float]: the position (x, y, z) of the root bone at each frame; the dimension is T x 3 where T is frame number; the unit is meter.
- **rotations** [3d array of float]: the rotation (in quaternion: X, Y, Z, W) of each bone at each frame; the dimension is T x N x 4.

The human skeleton in PhantomDance follows the definition of [SMPL](https://smpl.is.tue.mpg.de/) with 24 joints. So a human pose is represented as root position and 24 joint rotations. The 3D position uses the unit of meter, and the rotations use quaternion representation. That is, a T-frame-length motion sequence have T * (3 + 4 * 24) motion parameters. All the positions and rotations use world coordinates in Unity3D (x-right, y-up, z-forward). You can read [Unity official docs](https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html) for details.

If you are not familiar with the mathematics of 3D space 3D transformation and quaternion, we recommend you to read this [tutorial](http://web.mit.edu/2.998/www/QuaternionReport1.pdf). Since many 3D animation softwares describe animation data in local coordinates, we also provide script to convert the joint rotations from world coordinates to local coordinates in the toolkit.


## Toolkit

We provide a Unity3D toolkit as well as a [tutorial video](#tutorial-video) for data visualization. In fact, our toolkit can be used as a general motion data player. That is, this toolkit works for most human or human-like 3D models, and the skeletal rigs are not limited to SMPL format. Moreover, we provide a script for animation retargeting and your custom motion data can be applied to different rigged models with simple configuration.

### Requirements
- Unity 2022.3.x

### Directory Structure

This is the Asset folder with following structure:

- Assets
  - Animations
  - Music
  - Plugins
  - Characters
  - Scripts

Animations: The folder for animation files.

Music: The folder for music files.

Plugins: Plugins used in the project, for JSON parsing.

Characters: 3D models used for the animation dataset.

Scripts: Scripts for animation playing.

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
   - If you don't know anything about Timeline, you can check [here](https://docs.unity3d.com/2021.3/Documentation/Manual/com.unity.timeline.html)
   - Add Timeline to the model and create a Animation Track.
   - If need, create a Audio Track for music.

3. Bone Mapping
   - Since the skeletal structure of the model is different from the naming, you need to manually match the bones when using it.
   - In Inspector, find the BoneMapping component, following the SMPL model, drag the bone GameObject of the model you are using into the Bone GameObject field of the bone element in the Bone Data List.

4. Setting the input and output paths
   - Open the "Windows" edit box of the Unity editor, find the "Bone mapping" editor and open it.
   - Fill in the input and output paths as directed
   - Click "Switch" to convert the json data in the dataset to .anim file.

5. Playing an animation using the generated .anim file
   - Drag the animation file into the Animation Track of the Timeline.
   - Drag music files into Audio Track if needed.

Now you can visualize the animation playback details in the Timeline and Scene views.

### Tutorial Video

- YouTube (English): [Video Link]()
- BiliBili (Chinese): [Video Link]()


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
