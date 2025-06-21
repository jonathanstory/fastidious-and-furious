using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GraphicsUI : MonoBehaviour
{
    private UIDocument ui;
    private Button cancel;
    private Button apply;
    private Slider volume;

    private DropdownField displayResolution;
    private DropdownField quality;

    public AudioSource playerAudio;

    private void OnEnable()
    {
        ui = GetComponent<UIDocument>();

        cancel = ui.rootVisualElement.Q<Button>("cancel");
        apply = ui.rootVisualElement.Q<Button>("apply");
        volume = ui.rootVisualElement.Q<Slider>("volume");

        cancel.clicked += OnCancel;
        apply.clicked += OnApply;

        InitDisplayResolutions();
        InitQualitySettings();
    }

    void OnDisable()
    {
        cancel.clicked -= OnCancel;
        apply.clicked -= OnApply;
    }

    void OnCancel()
    {
        GameController.paused = false;
    }

    void OnApply()
    {
        var resolution = Screen.resolutions[displayResolution.index];
        Screen.SetResolution(resolution.width, resolution.height, true);
        QualitySettings.SetQualityLevel(quality.index, true);
        playerAudio.volume = volume.value;
    }

    void InitDisplayResolutions()
    {
        displayResolution = ui.rootVisualElement.Q<DropdownField>("displayResolution");
        displayResolution.choices = Screen.resolutions.Select(resolution => $"{resolution.width}x{resolution.height}").ToList();
        displayResolution.index = Screen.resolutions
            .Select((resolution, index) => (resolution, index))
            .First((value) => value.resolution.width == Screen.currentResolution.width && value.resolution.height == Screen.currentResolution.height).index;
    }

    void InitQualitySettings()
    {
        quality = ui.rootVisualElement.Q<DropdownField>("quality");
        quality.choices = QualitySettings.names.ToList();
        quality.index = QualitySettings.GetQualityLevel();
    }
}
