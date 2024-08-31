using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// プレイヤー死亡時の挙動を管理するクラス
public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    WeaponSwitcher weaponSwitcher;
    WeaponManager weaponManager;

    void Awake()
    {
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    void Start()
    {
        // ゲームオーバー時のUIを隠す
        gameOverCanvas.enabled = false;
    }

    // プレイヤー死亡時に呼び出されるメソッド
    public void OnPlayerDeath()
    {
        gameOverCanvas.enabled = true; // ゲームオーバーを知らせるUIを表示
        Time.timeScale = 0f; // ゲーム内の時間を止める
        weaponSwitcher.enabled = false; // 武器切り替えを不可能にする
        weaponManager.OnPlayerDeath(); // 射撃、リロードを不可能にする

        // カーソルを表示、カーソル操作を可能にする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
