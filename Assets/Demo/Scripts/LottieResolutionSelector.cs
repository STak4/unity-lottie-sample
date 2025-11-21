using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LottieResolutionSelector : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _dropdown;

    public UnityEvent<int> OnResolutionChanged = new UnityEvent<int>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int value)
    {
        var resolution = int.Parse(_dropdown.options[value].text);
        Debug.Log($"[ResolutionSelector] Resolution: {resolution}");
        OnResolutionChanged.Invoke(resolution);
    }
}
