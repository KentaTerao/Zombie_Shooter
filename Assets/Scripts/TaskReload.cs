using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアルのタスク（リロード）
public class TaskReload : TutorialTask
{
    public void OnTaskSetting()
    {
        Ammo Ammo = GameObject.Find("Player").GetComponent<Ammo>();
        Ammo.ReduceMagazineAmmo(AmmoType.AssaultAmmo);
        Ammo.ReduceMagazineAmmo(AmmoType.ShotgunAmmo);
        Ammo.ReduceMagazineAmmo(AmmoType.PistolAmmo);
    }

    public bool CheckTask()
    {
        bool reload = Input.GetKey(KeyCode.R);

        if (reload)
            return true;

        return false;
    }

    public string GetTitle()
    {
        return "リロード";
    }

    public string GetText()
    {
        return "rキーでリロードします。（斧以外）";
    }


    public float GetTransitionTime()
    {
        return 2f;
    }
}
