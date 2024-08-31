using UnityEngine;

// 武器のSEを管理するクラス
public class WeaponSoundManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] float pitchVariation = 0.2f; // ピッチの変動幅

    [Header("Audio Clip")]
    [SerializeField] AudioClip axeSound; // 斧投げ時のSE
    [SerializeField] AudioClip pistolSound; // ピストル射撃時のSE
    [SerializeField] AudioClip assaultSound; // アサルトライフル射撃時のSE
    [SerializeField] AudioClip shotgunSound; // ショットガン射撃時のSE
    [SerializeField] AudioClip reloadSound; // リロード時のSE

    [Header("Sound Volume")]
    [SerializeField] float axeSoundVolume = 1f; // 斧投げ時のSE音量
    [SerializeField] float pistolSoundVolume = 1f; // ピストル射撃時のSE音量
    [SerializeField] float assaultSoundVolume = 1f; // アサルトライフル射撃時のSE音量
    [SerializeField] float shotgunSoundVolume = 1f; // ショットガン射撃時のSE音量
    [SerializeField] float reloadSoundVolume = 1f; // リロード時のSE音量

    OptionManager optionManager;

    void Start()
    {
        optionManager = FindObjectOfType<OptionManager>();
    }



    // 斧投げ時のSEを再生するメソッド
    public void PlayAxeSound()
    {
        PlaySoundSEVolume(axeSound, axeSoundVolume);
    }

    // ピストル射撃時のSEを再生するメソッド
    public void PlayPistolSound()
    {
        PlaySoundSEVolume(pistolSound, pistolSoundVolume);
    }

    // アサルトライフル射撃時のSEを再生するメソッド
    public void PlayAssaultSound()
    {
        PlaySoundSEVolume(assaultSound, assaultSoundVolume);
    }

    // ショットガン射撃時のSEを再生するメソッド
    public void PlayShotgunSound()
    {
        PlaySoundSEVolume(shotgunSound, shotgunSoundVolume);
    }

    // リロード時のSEを再生するメソッド
    public void PlayReloadSound()
    {
        PlaySoundSEVolume(reloadSound, reloadSoundVolume);
    }

    // SEを再生するメソッド
    void PlaySoundSEVolume(AudioClip clip, float volume)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation); // ランダムにピッチを変える

            // マスターボリュームとSEボリュームを適用する
            audioSource.PlayOneShot(clip, volume * optionManager.GetMasterVolumeMultiplier() * optionManager.GetSEVolumeMultiplier());
            audioSource.pitch = 1f; // ピッチを元に戻す
        }
    }

    // SEを止めるメソッド
    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}