using System;
using System.Collections.Generic;
using Gilzoide.LottiePlayer;
using UnityEngine;

[Serializable]
public class LottieData
{
    public string name;
    public Sprite thumbnail;
    public LottieAnimationAsset asset;
}
[CreateAssetMenu(fileName = "LottieAssetsSO", menuName = "Scriptable Objects/LottieAssetsSO")]
public class LottieAssetsSO : ScriptableObject
{
    public List<LottieData> assets;
}
