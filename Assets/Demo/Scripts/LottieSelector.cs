using Gilzoide.LottiePlayer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnLottieSelected : UnityEvent<LottieAnimationAsset>
{
};

public class LottieSelector : MonoBehaviour
{
    [SerializeField] private LottieAssetsSO assets;
    [SerializeField] private RectTransform buttonParent;
    [SerializeField] private LottieSpawnButton buttonPrefab;

    public OnLottieSelected OnSelected = new OnLottieSelected();
    void Start()
    {
        var list = assets.assets;
        foreach (var item in list)
        {
            var obj = Instantiate(buttonPrefab, buttonParent);
            obj.Initialize(item, OnButtonClick);
        }
    }

    public void OnButtonClick(LottieAnimationAsset asset)
    {
        Debug.Log($"[LottieSelector] OnButtonClick: {asset}]");
        OnSelected?.Invoke(asset);
    }
}
