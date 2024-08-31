using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Pause pauseScript;
    [SerializeField] GameObject pauseButtonGroup;
    [SerializeField] GameObject optionUI;

    public void OnClickResumeButton()
    {
        pauseScript.ResumeGame();
    }

    public void OnClickOptionButton()
    {
        DeactivatePauseButtons();
        ActivateOptionUI();
    }

    public void OnClickBackButton()
    {
        DeactivateOptionUI();
        ActivatePauseButtons();
    }

    // ポーズ画面のボタンをアクティブにするメソッド
    void ActivatePauseButtons()
    {
        pauseButtonGroup.SetActive(true);
    }

    // ポーズ画面のボタンを非アクティブにするメソッド
    void DeactivatePauseButtons()
    {
        pauseButtonGroup.SetActive(false);
    }

    // オプションのUIをアクティブにするメソッド
    void ActivateOptionUI()
    {
        optionUI.SetActive(true);
    }

    // オプションのUIをアクティブにするメソッド
    void DeactivateOptionUI()
    {
        optionUI.SetActive(false);
    }
}
