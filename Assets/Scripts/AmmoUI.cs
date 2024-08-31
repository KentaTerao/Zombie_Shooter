using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 残弾数を表示するUIを管理するクラス
public class AmmoUI : MonoBehaviour
{
    [SerializeField] Ammo ammoSlot;
    [SerializeField] WeaponSwitcher weaponSwitcher;
    [SerializeField] TextMeshProUGUI magazineAmmoText; // マガジン内の弾数を表示するテキスト
    [SerializeField] TextMeshProUGUI totalAmmoText; // 総弾数を表示するテキスト

    void Update()
    {
        UpdateMagazineAmmoText();
        UpdateTotalAmmoText();
    }

    // マガジン内の弾数を表示するテキストを更新するメソッド
    void UpdateMagazineAmmoText()
    {
        AmmoType currentAmmoType = weaponSwitcher.GetCurrentWeapon().GetAmmoType();
        int currentAmmoInSlot = ammoSlot.GetMagazineAmmo(currentAmmoType);
        magazineAmmoText.text = currentAmmoInSlot.ToString("000");
    }

    // 総弾数を表示するテキストを更新するメソッド
    void UpdateTotalAmmoText()
    {
        AmmoType currentAmmoType = weaponSwitcher.GetCurrentWeapon().GetAmmoType();
        int currentAmmoInSlot = ammoSlot.GetTotalAmmo(currentAmmoType);
        totalAmmoText.text = currentAmmoInSlot.ToString("000");
    }

    // 武器アイコンを切り替えるメソッド
    public void ChangeWeaponIcon(AmmoType currentAmmoType)
    {
        DeactivateAllIcon();

        switch (currentAmmoType)
        {
            case AmmoType.Axe:
                transform.Find("Panel/AxeIcon").gameObject.SetActive(true);
                break;
            case AmmoType.AssaultAmmo:
                transform.Find("Panel/Assault1Icon").gameObject.SetActive(true);
                break;
            case AmmoType.ShotgunAmmo:
                transform.Find("Panel/Shotgun1Icon").gameObject.SetActive(true);
                break;
            case AmmoType.PistolAmmo:
                transform.Find("Panel/Pistol1Icon").gameObject.SetActive(true);
                break;
        }
    }

    // 武器アイコンをすべて非表示にするメソッド
    void DeactivateAllIcon()
    {
        transform.Find("Panel/AxeIcon").gameObject.SetActive(false);
        transform.Find("Panel/Assault1Icon").gameObject.SetActive(false);
        transform.Find("Panel/Shotgun1Icon").gameObject.SetActive(false);
        transform.Find("Panel/Pistol1Icon").gameObject.SetActive(false);
    }
}
