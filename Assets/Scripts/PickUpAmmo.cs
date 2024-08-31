using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  弾数回復アイテムを取得した際の挙動を管理するクラス
public class PickUpAmmo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5; // 回復させる弾数
    [SerializeField] AmmoType ammoType; // 回復させる弾の種類

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<Ammo>().IncreaseTotalAmmo(ammoType, ammoAmount);
        }
    }
}

