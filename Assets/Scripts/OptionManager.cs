using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OptionManager : MonoBehaviour
{
    [SerializeField] RigidbodyFirstPersonController fpsController;

    [SerializeField] Slider masterVolumeSlider; // マスターボリュームのスライダー
    [SerializeField] Slider seVolumeSlider; // SEボリュームのスライダー
    [SerializeField] Slider sensitivitySlider; // マウス感度のスライダー
    [SerializeField] Slider adsSensitivitySlider; // マウス感度（ADS時）のスライダー

    float masterVolumeMultiplier = 1f; // マスターボリュームマルチプライヤー
    float seVolumeMultiplier = 1f; // SEボリュームマルチプライヤー
    float sensitivity = 10f; // マウス感度
    float adsSensitivity = 10f; // マウス感度（ADS時）

    void Start()
    {
        // スライダーの初期値を反映
        sensitivitySlider.value = sensitivity;
        adsSensitivitySlider.value = adsSensitivity;

        // スライダーの値が変更されたときにメソッドを呼び出す
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterVolumeChanged(); });
        seVolumeSlider.onValueChanged.AddListener(delegate { OnSEVolumeChanged(); });
        sensitivitySlider.onValueChanged.AddListener(delegate { OnSensitivityChanged(); });
        adsSensitivitySlider.onValueChanged.AddListener(delegate { OnADSSensitivityChanged(); });
    }

    // マスターボリュームスライダーの値が変化した際にスライダーの値を変数に反映するメソッド
    public void OnMasterVolumeChanged()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeMultiplier = masterVolumeSlider.value;
            BgmManager.instance.SetVolume(BgmManager.instance.GetBaseVolume() * masterVolumeMultiplier);
        }
    }

    // SEボリュームスライダーの値が変化した際にスライダーの値を変数に反映するメソッド
    public void OnSEVolumeChanged()
    {
        if (seVolumeSlider != null)
            seVolumeMultiplier = seVolumeSlider.value;
    }

    // マウス感度スライダーの値が変化した際にスライダーの値を変数に反映し、WeaponZoomの感度を更新するメソッド
    public void OnSensitivityChanged()
    {
        if (sensitivitySlider != null)
        {
            sensitivity = sensitivitySlider.value;
            UpdateAllWeaponSensitivities();
        }
    }

    // マウス感度（ADS時）スライダーの値が変化した際にスライダーの値を変数に反映し、WeaponZoomの感度を更新するメソッド
    public void OnADSSensitivityChanged()
    {
        if (adsSensitivitySlider != null)
        {
            adsSensitivity = adsSensitivitySlider.value;
            UpdateAllWeaponSensitivities();
        }
    }

    // すべてのWeaponZoomの感度を更新するメソッド
    private void UpdateAllWeaponSensitivities()
    {
        WeaponZoom[] weaponZooms = FindObjectsOfType<WeaponZoom>();
        foreach (WeaponZoom weaponZoom in weaponZooms)
        {
            weaponZoom.UpdateSensitivity();
        }
    }

    public float GetMasterVolumeMultiplier()
    {
        return masterVolumeMultiplier;
    }

    public float GetSEVolumeMultiplier()
    {
        return seVolumeMultiplier;
    }

    public float GetSensitivity()
    {
        return sensitivity;
    }

    public float GetADSSensitivity()
    {
        return adsSensitivity;
    }
}
