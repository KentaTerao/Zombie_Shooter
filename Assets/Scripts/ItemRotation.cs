using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アイテムの回転を制御するクラス
public class ItemRotation : MonoBehaviour
{
    // 回転スピード
    [SerializeField] private float rotationSpeed = 60.0f;

    void Update()
    {
        // 回転処理
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
