using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// プレイヤーがダメージを受けた際に表示するUIを管理するクラス
public class DamageUI : MonoBehaviour
{
    [SerializeField] Image img; // ダメージを受けた際に表示する画像

    void Start()
    {
        img.color = Color.clear;
    }

    // 赤くなった画面を徐々に戻す
    void Update()
    {
        img.color = Color.Lerp(img.color, Color.clear, Time.deltaTime);
    }

    // ダメージを受けた際に呼び出される
    // 画面を赤くする
    public void OnTakeDamage()
    {
        img.color = new Color(0.6f, 0, 0, 0.6f);
    }
}
