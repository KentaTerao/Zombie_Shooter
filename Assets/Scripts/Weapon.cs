using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 武器の挙動を管理するクラス
public class Weapon : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Camera FPCamera; // Main Camera

    [Header("Sound")]
    [SerializeField] WeaponSoundManager weaponSoundManager;

    [Header("General")]
    [SerializeField] float range = 100f; // 弾の届く距離
    [SerializeField] GameObject hitEffect; // 弾ヒット時のエフェクト
    [SerializeField] float reloadTime = 2f; // リロードにかかる時間

    [Header("Gun")]
    [SerializeField] ParticleSystem muzzleFlash; // 射撃時のエフェクト

    [Header("Ammo")]
    [SerializeField] Ammo ammoSlot; // 弾管理用スクリプト
    [SerializeField] AmmoType ammoType; // 弾の種類を管理するenum型

    [Header("Cool Time")]
    [SerializeField] float timeBetweenShots = 0.5f; // 射撃の間隔

    [Header("Axe")]
    [SerializeField] GameObject axePrefab; // 投げ斧のプレハブ
    [SerializeField] float throwForce = 20f; // 斧を投げる力
    [SerializeField] float spinSpeed = 1000f; // 斧の回転速度

    Pause pauseScript;
    Coroutine reloadCoroutine; // リロードコルーチンを保持する変数
    bool canShoot = true; // 射撃できる状態であるかを示すフラグ
    bool isReloading = false; // リロード中かどうかを示すフラグ
    bool isShooting = false; // 射撃ボタンが押されているかを示すフラグ

    void OnEnable()
    {
        canShoot = true;
        isShooting = false;
    }

    void Start()
    {
        pauseScript = FindObjectOfType<Pause>();
    }

    void Update()
    {
        // ポーズ中は射撃を無効化
        if (pauseScript != null && pauseScript.GetIsPaused())
            return;

        // アサルトライフルの場合は連射
        if (ammoType == AmmoType.AssaultAmmo)
        {
            if (Input.GetMouseButtonDown(0) && canShoot && !isReloading)
            {
                isShooting = true;
                StartCoroutine(AutoShoot());
            }

            if (Input.GetMouseButtonUp(0))
            {
                isShooting = false;
            }
        }
        else // 単発武器の射撃
        {
            if (Input.GetMouseButtonDown(0) && canShoot && !isReloading)
                StartCoroutine(Shoot());
        }

        // リロード
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            reloadCoroutine = StartCoroutine(ReloadWeapon());
    }

    // 連射を行うコルーチン
    IEnumerator AutoShoot()
    {
        while (isShooting && canShoot)
        {
            yield return StartCoroutine(Shoot());
        }
    }

    // 射撃の一連の挙動を実行するコルーチン
    IEnumerator Shoot()
    {
        canShoot = false;

        // マガジン内弾数が0より大きい場合に射撃可能
        if (ammoSlot.GetMagazineAmmo(ammoType) > 0)
        {
            if (ammoType != AmmoType.Axe) // 斧以外の武器はレイキャストによる射撃を行う
            {
                if (muzzleFlash != null)
                    PlayMuzzleFlash(); // マズルフラッシュエフェクトをプレイ

                PlayShootSound(); // 射撃SEを鳴らす
                ShootByRaycast(ammoSlot.GetDamage(ammoType)); // 即着弾の判定をレイキャストで行う
                ammoSlot.ReduceMagazineAmmo(ammoType); // マガジン内弾数を減らす
            }
            else // 斧はオブジェクトを飛ばし、衝突時にダメージを与える
            {
                ThrowAxe(ammoSlot.GetDamage(ammoType));
                PlayShootSound(); // 射撃SEを鳴らす
            }
        }

        // 次の射撃まで間隔をあける
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    // リロード処理を行うコルーチン
    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        weaponSoundManager.PlayReloadSound();

        // リロードのコルーチンを呼び出し
        yield return StartCoroutine(ammoSlot.Reload(ammoType, reloadTime));

        isReloading = false;
        weaponSoundManager.StopSound();
    }

    // リロードをキャンセルするメソッド
    public void CancelReload()
    {
        if (isReloading)
        {
            if (reloadCoroutine != null)
            {
                StopCoroutine(reloadCoroutine); // リロードコルーチンを停止
            }
            isReloading = false;
            weaponSoundManager.StopSound(); // リロード音を停止
        }
    }

    // マズルフラッシュエフェクトを開始するメソッド
    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    // レイキャストが衝突したポイントに火花が散るエフェクトを生成するメソッド
    void CreateHitEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    // 武器種に応じた射撃SEを鳴らすメソッド
    void PlayShootSound()
    {
        switch (ammoType)
        {
            case AmmoType.Axe:
                weaponSoundManager.PlayAxeSound();
                break;

            case AmmoType.PistolAmmo:
                weaponSoundManager.PlayPistolSound();
                break;

            case AmmoType.AssaultAmmo:
                weaponSoundManager.PlayAssaultSound();
                break;

            case AmmoType.ShotgunAmmo:
                weaponSoundManager.PlayShotgunSound();
                break;
        }
    }

    // レイキャストによる射撃を行うメソッド
    void ShootByRaycast(float damage)
    {
        RaycastHit hit; // レイキャストが衝突した対象を格納する変数
        int layerMask = ~LayerMask.GetMask("DeadEnemy"); // DeadEnemyレイヤーを無視するマスク

        // カメラの位置から前方にレイキャストを行う
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, layerMask))
        {
            CreateHitEffect(hit); // レイキャストが衝突したポイントにエフェクトを生成

            // ヒットした対象が敵である場合はダメージ処理を行う
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
                return;

            target.TakeDamage(damage);
        }
    }

    // 斧を投げて攻撃を行うメソッド
    void ThrowAxe(float damage)
    {
        // 斧の角度を設定
        Quaternion axeRotation = Quaternion.LookRotation(FPCamera.transform.forward) * Quaternion.Euler(0, 90, 0);

        // 投げ斧を生成
        GameObject axe = Instantiate(axePrefab, FPCamera.transform.position, axeRotation);
        Rigidbody rb = axe.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // 投げ斧にダメージを設定
            axe.GetComponent<ThrowedAxe>().SetDamage(damage);

            // 投げ斧に力を加えて飛ばす
            rb.AddForce(FPCamera.transform.forward * throwForce, ForceMode.VelocityChange);

            // 斧に回転力を加えて回転させる
            rb.AddTorque(FPCamera.transform.right * spinSpeed);
        }
    }

    // 弾薬の種類を返すメソッド
    public AmmoType GetAmmoType()
    {
        return ammoType;
    }
}