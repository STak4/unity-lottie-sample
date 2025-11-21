using UnityEngine;

/// <summary>
/// 指定したフレームレートを Application.targetFrameRate に設定するコンポーネント。
/// インスペクタから 24 / 30 / 45 / 60 / 90 / 120 を選択できます。
/// Awake のタイミングで一度だけ適用します。
/// </summary>
public class FrameRateSetter : MonoBehaviour
{
    public enum FrameRateOption
    {
        Fps_24  = 24,
        Fps_30  = 30,
        Fps_45  = 45,
        Fps_60  = 60,
        Fps_90  = 90,
        Fps_120 = 120,
    }

    [Header("ターゲットフレームレート")]
    [Tooltip("Application.targetFrameRate に設定する値")]
    [SerializeField]
    private FrameRateOption targetFrameRate = FrameRateOption.Fps_60;

    private void Awake()
    {
        ApplyFrameRate();
    }

    /// <summary>
    /// 現在の設定を元にフレームレートを適用します。
    /// </summary>
    private void ApplyFrameRate()
    {
        // モバイルでは VSync 無効が基本（targetFrameRate を優先させるため）
        QualitySettings.vSyncCount = 0;

        int fps = (int)targetFrameRate;
        Application.targetFrameRate = fps;

        Debug.Log($"[FrameRateSetter] Target FPS set to {fps}");
    }

    /// <summary>
    /// コードから動的にフレームレートを切り替えたいとき用。
    /// </summary>
    public void SetFrameRate(FrameRateOption option)
    {
        targetFrameRate = option;
        ApplyFrameRate();
    }
}