using System;
using Gilzoide.LottiePlayer;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LottieSpawnButton : MonoBehaviour
{
    [SerializeField] private Button button;
    public void Initialize(LottieData data, Action<LottieAnimationAsset> callback)
    {
        button.image.sprite = data.thumbnail;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => callback?.Invoke(data.asset));
    }
}
