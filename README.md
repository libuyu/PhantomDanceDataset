# PhantomDance Dataset

The official Dataset proposed in "DanceFormer: Music Conditioned 3D Dance Generation with Parametric Motion Transformer” [AAAI 2022]. 

<table class="center">
<tr>
  <td><img src="https://raw.githubusercontent.com/libuyu/libuyu.github.io/master/files/Chinese1.gif"></td>
  <td><img src="https://raw.githubusercontent.com/libuyu/libuyu.github.io/master/files/Otaku1.gif"></td>
</tr>
<tr>
  <td><img src="https://raw.githubusercontent.com/libuyu/libuyu.github.io/master/files/Jazz1.gif"></td>
  <td><img src="https://raw.githubusercontent.com/libuyu/libuyu.github.io/master/files/Hiphop1.gif"></td>
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
  - Scenes

Animations: The folder for animation files.

Music: The folder for music files.

Plugins: Plugins used in the project, for JSON parsing.

Characters: 3D models used for the animation dataset.

Scripts: Scripts for animation playing.

Scenes: The default Unity folder of Scenes.

### Use the toolkit to visualize motion data

1. Install Unity3D and Toolkit
   - Toolkit install: 
   ```
   $ git clone https://github.com/libuyu/PhantomDanceDataset.git
   ```
   - Download and install Unity Hub and Unity Editor: https://unity.com/download
   - Open the toolkit project with Unity Hub

> We provide a SampleScene with properly configured character models (an official SMPL model and a non-SMPL model) and timeline tracks. To play the animation data on the models, you just need the following steps:

2. Convert motion data to animation clip
   - Open the "Windows" edit box of the Unity editor, find the "Bone mapping" editor and open it.
   - Set these params: 
      - input json path (e.g. "Assets/Animations/RawData/xxx.json")
      - output anim path (e.g. "Assets/Animations/AnimClips/xxx.anim")
      - model name ("Official_SMPL" or "Jean" in the sample scene).
   - Click "Create Animation Clip!" to convert the json data in the dataset to .anim file.

3. Play the animation with Timeline
   - Drag the animation clip file into the Animation Track of the Timeline.
   - Drag music files into Audio Track if needed.
   - Click the play button in the Timeline window.

Now you can playback any details with Timeline and watch the animation in Scene views.


### Use custom 3D characters
1. Configure scenes
   - Put your custom 3D model in the Characters folder, and drag it into the scene or hierarchy window.
   - **Note**: the model should be T-posed. If your model in Scene view is not in T-pose, rotate bones manually to fit T-pose.
   - Drag the model (in the hierarchy window) to the Timeline to add an Animation Track.
   - If needed, create an AudioSource in the scene and put it to the Timeline to add an Audio Track.

2. Bone Mapping
   - Since the skeletal structures and bone names of different models are usually different, you need to manually match the bones to the SMPL format when using non-SMPL rigged models.
   - In inspector, find the BoneMapping component, drag the bone object of the your model into the Bone Data List in inspector to match the corresponding SMPL bone.
   - Run the Unity project and you will have a bone map file for your model.

3. Follow Step 2-3 in the [previous section](#use-the-toolkit-to-visualize-motion-data).



### Tutorial Video

- English subtitle:  
- Chinese subtitle: https://www.bilibili.com/video/BV1VX4y1E7Pg

### Copyright


## Acknowledgement
- The 3D character models in this project are provided by:
   - [SMPL-Model](https://smpl.is.tue.mpg.de/)
   - Jean in [Genshin Impact](https://genshin.hoyoverse.com/)
- Thanks to [Yongxiang](https://github.com/Qedsama) for his contribution in the Unity3D toolkit.

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
