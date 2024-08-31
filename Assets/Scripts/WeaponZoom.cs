using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

// 武器のぞき込み（ADS)を管理するクラス
public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] OptionManager optionManager;
    [SerializeField] float zoomOutFOV = 60f; // ズームアウト時(通常時)の視野角
    [SerializeField] float zoomInFOV = 25f; // ズームイン時(覗き込み時)の視野角

    bool zoomInToggle = false; // ズームインをしているかどうかを示すフラグ

    void Update()
    {
        // 右クリックでズームイン・ズームアウトを切り替える
        if (Input.GetMouseButtonDown(1))
        {
            if (!zoomInToggle)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    // ズームイン時の視野角・感度に変更するメソッド
    void ZoomIn()
    {
        zoomInToggle = true;
        fpsCamera.fieldOfView = zoomInFOV;
        UpdateSensitivity();
    }

    // ズームアウト時の視野角・感度に変更するメソッド
    public void ZoomOut()
    {
        zoomInToggle = false;
        fpsCamera.fieldOfView = zoomOutFOV;
        UpdateSensitivity();
    }

    // 感度を更新するメソッド
    public void UpdateSensitivity()
    {
        if (zoomInToggle)
        {
            fpsController.mouseLook.XSensitivity = optionManager.GetADSSensitivity();
            fpsController.mouseLook.YSensitivity = optionManager.GetADSSensitivity();
        }
        else
        {
            fpsController.mouseLook.XSensitivity = optionManager.GetSensitivity();
            fpsController.mouseLook.YSensitivity = optionManager.GetSensitivity();
        }
    }
}
