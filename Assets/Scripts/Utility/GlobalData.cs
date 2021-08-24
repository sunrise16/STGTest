#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public sealed class GlobalData
{
    #region VARIABLE
    public static EGameDifficulty enGameDifficulty = EGameDifficulty.enDifficulty_Lunatic;
    public static EPlayerType enSelectPlayerType = EPlayerType.enType_Reimu;
    public static EPlayerWeaponType enSelectPlayerWeaponType = EPlayerWeaponType.enType_A;

    public static int iStartLife = 2;
    public static int iStartSpell = 3;

    public static string szBulletPrefabPath = "Prefabs/Bullet/Bullet";
    public static string szEffectPrefabPath = "Prefabs/Effect/Effect";
    public static string szItemPrefabPath = "Prefabs/Item/Item";
    public static string szEnemyPrefabPath = "Prefabs/Enemy/Enemy";
    public static string szPlayerPrefabPath = "Prefabs/Player/Player";
    public static string szSoundPrefabPath = "Prefabs/Sound/Sound";
    public static string szMiscellaneousPrefabPath = "Prefabs/Miscellaneous/";

    public static string szBulletSpritePath = "Sprites/Bullet/Bullets";
    public static string szItemSpritePath = "Sprites/Item/Items";
    public static string szEnemySpritePath = "Sprites/Enemy/";
    public static string szPlayerSpritePath = "Sprites/Player/";

    public static string szBulletAnimationPath = "Animations/Bullet/";
    public static string szEffectAnimationPath = "Animations/Effect/";
    public static string szPlayerAnimationPath = "Animations/Player/";
    public static string szEnemyAnimationPath = "Animations/Enemy/";
    public static string szCommonAnimationPath = "Animations/Common/";

    public static string szBGMClipPath = "Sounds/BGM/";
    public static string szSEClipPath = "Sounds/SE/";
    #endregion
}