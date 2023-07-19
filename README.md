# PhantomDance Dataset

### Languages / 语言
- [English](#english)
- [中文](#chinese)

## English
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
  - Characters
  - Scripts

Animations: Animation files.

Plugins: Plugins used in the project, for JSON parsing.

Characters: Standard models used for the animation dataset.

Scripts: Scripts for data visualization.

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

## Chinese

官方数据集，收录在“DanceFormer: Music Conditioned 3D Dance Generation with Parametric Motion Transformer” [AAAI 2022]中。

**目录**
- [幻影舞蹈数据集](#幻影舞蹈数据集)
  - [介绍](#介绍)
  - [数据集下载](#数据集下载)
  - [数据格式](#数据格式)
  - [工具包使用](#工具包使用)
  - [致谢](#致谢)
  - [引用](#引用)

## 介绍
幻影舞蹈数据集是由专业动画师创建的第一个舞蹈数据集，总共包含260对舞蹈-音乐配对，总时长为`todo`小时。

本存储库还提供了用于数据可视化（动画播放）的Unity3D工具包。

## 数据集下载

Google Drive: [链接]() `todo`

百度网盘: [链接]() `todo`

## 数据格式
为了方便使用，原始的AutoDesk FBX格式的动画曲线被转换为以30fps帧率保存的浮点数数组，并以JSON格式进行存储。相应的音乐序列以WAV格式保存。

人体骨骼遵循[SMPL](https://smpl.is.tue.mpg.de/)的定义，包含24个关节。因此，一个人的姿势由根位置和24个关节旋转表示。3D位置使用米作为单位，旋转使用四元数表示。也就是说，一个长度为T的动作序列有T * (3 + 4 * 24)个浮点数值。所有的位置和旋转都使用Unity3D中的世界坐标系（x-右，y-上，z-前）表示。您可以阅读[Unity官方文档](https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html)了解详细信息。

如果您对3D空间的数学知识和四元数不熟悉，我们建议您阅读这篇[教程](http://web.mit.edu/2.998/www/QuaternionReport1.pdf)。由于许多3D动画软件使用局部坐标系描述动画数据，我们还提供了将关节旋转从世界坐标系转换为局部坐标系的脚本。

## 工具包使用

### 结构

这是一个Unity 2022.3.2f1c1的资源项目文件夹。

结构如下:

- Asset
  - Animations
  - Characters
  - Plugins
  - Scripts

Animations: 动作文件。

Plugins: 项目中使用的插件，用于JSON解析。

Characters: 演示用于动作数据集的标准模型。

Scripts: 数据可视化脚本。

### 使用方法

1. 安装Unity和工具包
   - Unity下载：https://unity.com/download
   - 工具包安装：
      - 创建一个新的Unity项目。
      - 下载工具包：
      ```
      $ git clone https://github.com/libuyu/PhantomDanceDataset.git
      ```
      - 用工具包的Asset文件夹替换Unity项目的Asset文件夹。

2. 配置场景
   - 将模型拖放到Animations文件夹下的场景中，或者导入自己的模型。
   - 为模型添加Timeline组件，并创建一个Animation Track。
   - 如有需要，可以为音乐创建一个Audio Track。

3. 骨骼映射
   - 由于模型的骨骼结构与命名不同，使用时需要手动匹配骨骼。
   - 在Inspector中找到BoneMapping组件，按照SMPL模型的方式，将所使用的模型的骨骼GameObject拖放到Bone Data List中各个骨骼元素的Bone GameObject字段中。

4. 设置输入和输出路径
   - 在Unity编辑器的"Windows"菜单中，找到"Bone mapping"编辑器并打开它。
   - 按照指示填写输入和输出路径。
   - 点击"Switch"将数据集中的JSON数据转换为.anim文件。

5. 使用生成的.anim文件播放动画
   - 将.anim文件拖放到Timeline的Animation Track中。
   - 如有需要，将音乐文件拖放到Audio Track中。

现在，您可以在Timeline和Scene视图中查看动画播放的详细信息。

## 致谢
感谢[Yongxiang](https://github.com/Qedsama)对Unity3D工具包的贡献。

## 引用

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