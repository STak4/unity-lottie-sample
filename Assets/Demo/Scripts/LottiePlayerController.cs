using System.Collections.Generic;
using Gilzoide.LottiePlayer;
using TMPro;
using UnityEngine;

public struct LottieSpawnData
{
    public int Id;
    public Vector3 Position;
    public LottieAnimationAsset AnimationAsset;
    public ImageLottiePlayer Player;
}

public class SpawnLogic
{
    private readonly float _width, _height;
    public SpawnLogic(Rect area)
    {
        _width = area.width;
        _height = area.height;
    }

    public Vector3 GetPosition()
    {
        var x = Random.Range(-_width/2f, _width/2f);
        var y = Random.Range(-_height/2f, _height/2f);
        
        return new Vector3(x, y, 0f);
    }
}
public class LottiePlayerController : MonoBehaviour
{
    // TODO: ループ制御を外からする方法が...
    //public bool Loop = true;
    
    [SerializeField] private RectTransform spawnParent;
    [SerializeField] private ImageLottiePlayer lottiePlayerPrefab;
    [SerializeField] private LottieResolutionSelector resolutionSelector;
    [SerializeField] private LottieSelector lottieSelector;
    [SerializeField] private TMP_Text debugText;

    private SpawnLogic _logic;
    private Dictionary<int, LottieSpawnData> _players;
    private Dictionary<string, LottieAnimationAsset> _assets;

    private int _resolution;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _logic = new SpawnLogic(spawnParent.rect);
        _players = new Dictionary<int, LottieSpawnData>();
        _assets = new Dictionary<string, LottieAnimationAsset>();
        resolutionSelector.OnResolutionChanged.AddListener(OnResolutionChanged);
        lottieSelector.OnSelected.AddListener(OnLottieSelected);

        UpdateDebugText();
    }

    void OnDestroy()
    {
        resolutionSelector.OnResolutionChanged.RemoveListener(OnResolutionChanged);
        lottieSelector.OnSelected.RemoveListener(OnLottieSelected);
    }

    public LottieSpawnData SpawnLottie(LottieAnimationAsset animationAsset)
    {
        var player = Instantiate(lottiePlayerPrefab, spawnParent);
        var spawnData = new LottieSpawnData()
        {
            Id = _players.Count,
            Position = _logic.GetPosition(),
            AnimationAsset = animationAsset,
            Player = player
        };
        
        player.transform.SetLocalPositionAndRotation(spawnData.Position, Quaternion.identity);
        // TODO: 解像度設定
        player.SetAnimationAsset(animationAsset);
        player.Play();
        _players.Add(spawnData.Id, spawnData);

        _assets.TryAdd(animationAsset.name, animationAsset);
        
        UpdateDebugText();
        return spawnData;
    }

    public void DestroyLottie(int id)
    {
        if (_players.Remove(id, out var data))
        {
            Destroy(data.Player.gameObject);
        }
        
        UpdateDebugText();
    }

    private void OnResolutionChanged(int resolution)
    {
        _resolution = resolution;
    }

    private void OnLottieSelected(LottieAnimationAsset asset)
    {
        var spawned = SpawnLottie(asset);
        Debug.Log($"[LottieController] Lottie spawn[{spawned.Id}]. Asset:{spawned.AnimationAsset.name}");
    }

    private void UpdateDebugText()
    {
        if (debugText == null) return;

        var count = _players.Count;
        var lotties = _assets.Count;
        debugText.text = $"Count: {count}\nLotties:{lotties}";
    }
}
