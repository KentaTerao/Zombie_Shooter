using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 武器切り替えを管理するクラス
public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] AmmoUI ammoUI; // 現在武器のアイコンと残弾数を表示するUI

    // アクティブにする武器のインデックス
    // ヒエラルキー上のWeapons配下、上から連番
    int currentWeaponIdx = 0;

    void Start()
    {
        ActivateWeapon();
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        int previousWeaponIdx = currentWeaponIdx;

        SwitchByKey(); // 数字キー入力による武器切り替え
        SwitchByWheel(); // ホイール入力による武器切り替え

        if (previousWeaponIdx != currentWeaponIdx)
        {
            ActivateWeapon();
        }

    }

    // 数字キー入力による武器切り替えメソッド
    void SwitchByKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 0
            currentWeaponIdx = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 1
            currentWeaponIdx = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // 2
            currentWeaponIdx = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) // 3
            currentWeaponIdx = 3;
    }

    // ホイール入力による武器切り替えメソッド
    void SwitchByWheel()
    {
        // ホイールを上にスクロールした際の武器切り替え処理
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // 最後の武器をアクティブ化している際、次の武器への切り替えは最初の武器への切り替えとする
            if (currentWeaponIdx >= transform.childCount - 1)
                currentWeaponIdx = 0;
            else //そうでない場合は次の武器に切り替える
                currentWeaponIdx++;
        }

        // ホイールを下にスクロールした際の武器切り替え処理
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // 最初の武器をアクティブ化している際、次の武器への切り替えは最後の武器への切り替えとする
            if (currentWeaponIdx <= 0)
                currentWeaponIdx = transform.childCount - 1;
            else //そうでない場合は次の武器に切り替える
                currentWeaponIdx--;
        }
    }

    // currentWeaponIdxに対応した武器に切り替えるメソッド
    void ActivateWeapon()
    {
        int weaponIdx = 0;

        // 武器を切り替える前に現在の武器のリロードをキャンセル
        Weapon currentWeapon = GetCurrentWeapon();
        if (currentWeapon != null)
        {
            currentWeapon.CancelReload();
        }

        // Weapons配下の武器を確認し、アクティブ化対象の武器のみSetActive(true)する
        foreach (Transform weapon in transform)
        {
            if (weaponIdx == currentWeaponIdx)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            weaponIdx++;
        }

        // 武器切り替え後、武器アイコンを変更する
        ammoUI.ChangeWeaponIcon(GetCurrentWeapon().GetAmmoType());
    }

    // 現在アクティブ状態である武器を返すメソッド
    public Weapon GetCurrentWeapon()
    {
        foreach (Transform weapon in transform)
        {
            if (weapon.gameObject.activeInHierarchy)
                return weapon.GetComponent<Weapon>();
        }

        return null;
    }
}
