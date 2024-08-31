using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーのSEを管理するクラス
public class PlayerSoundManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] float pitchVariation = 0.2f; // ピッチの変動幅

    [Header("Audio Clip")]
    [SerializeField] AudioClip damageSound; // ダメージ時のSE

    [Header("Sound Volume")]
    [SerializeField] float damageSoundVolume = 1f; // ダメージ時のSE音量

    OptionManager optionManager;

    void Start()
    {
        optionManager = FindObjectOfType<OptionManager>();
    }

    // ダメージ時のSEを鳴らすメソッド
    public void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation); // ランダムにピッチを変える
            audioSource.PlayOneShot(damageSound, damageSoundVolume * optionManager.GetMasterVolumeMultiplier());
            audioSource.pitch = 1f; // ピッチを元に戻す
        }
    }
}
