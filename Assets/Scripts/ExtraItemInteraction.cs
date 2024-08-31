using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ウェーブ終了時に出現するアイテムとのインタラクションを管理するクラス
public class ExtraItemInteraction : MonoBehaviour
{
    ExtraItemManager extraItemManager;
    GameObject interactionUI; // アイテムに触れた際に表示するUI
    TextMeshProUGUI descriptionText; // UI上の説明テキスト
    bool isPlayerInRange = false; // プレイヤーがアイテムに触れているかどうか
    ExtraItem targetItem;

    void Start()
    {
        extraItemManager = FindObjectOfType<ExtraItemManager>();
        interactionUI = extraItemManager.GetInteractUI();

        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // 最初はUIを非表示にする
            descriptionText = interactionUI.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (targetItem != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                targetItem.Apply(); // アイテム効果を適用
                extraItemManager.ClearItems(); // 他のアイテムも削除
                isPlayerInRange = false;
                targetItem = null;
                HideItemInteractionUI(); // UIを非表示にする
            }
        }
        else
            HideItemInteractionUI(); // アイテムが存在しない場合はUIを非表示にする
    }

    // アイテムに触れた際に呼ばれるメソッド
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExtraItem"))
        {
            isPlayerInRange = true;
            targetItem = other.GetComponent<ExtraItem>();
            ShowItemInteractionUI(other.GetComponent<ExtraItem>().GetDescription()); // UIを表示する
        }
    }

    // アイテムから離れた際に呼ばれるメソッド
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ExtraItem"))
        {
            isPlayerInRange = false;
            targetItem = null;
            HideItemInteractionUI(); // UIを非表示にする
        }
    }

    // UIを表示するメソッド
    void ShowItemInteractionUI(string description)
    {
        if (interactionUI != null)
        {
            descriptionText = interactionUI.GetComponentInChildren<TextMeshProUGUI>();
            descriptionText.text = description; // 説明テキストを設定
            interactionUI.SetActive(true); // UIを表示
        }
    }

    // UIを非表示にするメソッド
    void HideItemInteractionUI()
    {
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // UIを非表示
        }
    }
}
