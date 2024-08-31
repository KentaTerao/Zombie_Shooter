using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 武器を管理するクラス
public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject axe;
    [SerializeField] GameObject assault;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject pistol;

    // プレイヤー死亡時に呼ばれるメソッド
    public void OnPlayerDeath()
    {
        axe.GetComponent<Weapon>().enabled = false;
        assault.GetComponent<Weapon>().enabled = false;
        shotgun.GetComponent<Weapon>().enabled = false;
        pistol.GetComponent<Weapon>().enabled = false;
    }

    public GameObject GetAxe()
    {
        return axe;
    }

    public GameObject GetAssault()
    {
        return assault;
    }

    public GameObject GetShotgun()
    {
        return shotgun;
    }

    public GameObject GetPistol()
    {
        return pistol;
    }
}
