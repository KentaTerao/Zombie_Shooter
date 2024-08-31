using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 弾薬を管理するクラス
public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    public class AmmoSlot
    {
        public AmmoType ammoType;
        public int totalAmmoAmount; // 総弾数
        public int magazineAmmoAmount; // マガジン内弾数
        public int maxMagazineSize = 30; // マガジンの最大サイズ
        public float damage;

        public bool CompareAmmoType(AmmoType ammoType)
        {
            return this.ammoType == ammoType;
        }
    }

    // 弾薬の種類に応じて総弾数を返すメソッド
    public int GetTotalAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).totalAmmoAmount;
    }

    // 弾薬の種類に応じてマガジン内の弾数を返すメソッド
    public int GetMagazineAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).magazineAmmoAmount;
    }

    // 弾薬の種類に応じてダメージを返すメソッド
    public float GetDamage(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).damage;
    }

    // 弾薬の種類に応じてマガジン内の弾数を減らすメソッド
    public void ReduceMagazineAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).magazineAmmoAmount--;
    }

    // 弾薬の種類に応じて総弾数を増やすメソッド
    public void IncreaseTotalAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).totalAmmoAmount += ammoAmount;
    }

    // 弾薬の種類に応じてマガジンの最大弾数を増やすメソッド
    public void IncreaseMagazineAmmo(AmmoType ammoType, int ammoAmount)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        slot.maxMagazineSize += ammoAmount; // マガジンの最大弾数を増やす

        // マガジン内の弾を補充する
        int neededAmmo = slot.maxMagazineSize - slot.magazineAmmoAmount;
        if (slot.totalAmmoAmount > 0)
        {
            int ammoToReload = Mathf.Min(neededAmmo, slot.totalAmmoAmount);
            slot.magazineAmmoAmount += ammoToReload;
        }
    }

    // 弾薬の種類に応じてダメージを増やすメソッド
    public void IncreaseDamage(AmmoType ammoType, float damageAmount)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        slot.damage += damageAmount; // ダメージを増やす
    }

    // リロードを行うコルーチン
    public IEnumerator Reload(AmmoType ammoType, float reloadTime)
    {
        // リロード処理中のUIやエフェクトを追加

        // リロード時間の遅延
        yield return new WaitForSeconds(reloadTime);

        // リロードに必要な弾数
        AmmoSlot slot = GetAmmoSlot(ammoType);
        int neededAmmo = slot.maxMagazineSize - slot.magazineAmmoAmount;

        // 総弾数が足りなければその分だけリロード
        if (slot.totalAmmoAmount > 0)
        {
            int ammoToReload = Mathf.Min(neededAmmo, slot.totalAmmoAmount);
            slot.magazineAmmoAmount += ammoToReload;
            slot.totalAmmoAmount -= ammoToReload;
        }

        // リロード完了後の処理を追加
    }

    // 弾薬の種類に応じたスロットを返すメソッド
    public AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot ammoSlot in ammoSlots)
        {
            if (ammoSlot.CompareAmmoType(ammoType))
                return ammoSlot;
        }
        return null;
    }
}