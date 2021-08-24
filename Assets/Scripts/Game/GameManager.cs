#region USING
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public partial class GameManager : UnitySingleton<GameManager>
{
    #region VARIABLE
    [HideInInspector] public GameObject pPlayer;
    [HideInInspector] public Camera pMainCamera;
    [HideInInspector] public Sprite[] pBulletEffectSprite;
    [HideInInspector] public Sprite[] pItemSprite;
    [HideInInspector] public Sprite[] pPlayerSprite;
    [HideInInspector] public Sprite[] pEnemySprite;
    [HideInInspector] public RuntimeAnimatorController[] pBulletAnimator;
    [HideInInspector] public RuntimeAnimatorController[] pEffectAnimator;
    [HideInInspector] public RuntimeAnimatorController[] pPlayerAnimator;
    [HideInInspector] public RuntimeAnimatorController[] pEnemyAnimator;
    [HideInInspector] public RuntimeAnimatorController[] pCommonAnimator;
    [HideInInspector] public float fDefaultPadding;

    public PlayerBase pPlayerBase;
    public IEnumerator iGameProcedure;
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        SetPlayerInfo();

        pMainCamera = Camera.main;
        pBulletEffectSprite = Resources.LoadAll<Sprite>(GlobalData.szBulletSpritePath);
        pItemSprite = Resources.LoadAll<Sprite>(GlobalData.szItemSpritePath);
        pEnemySprite = Resources.LoadAll<Sprite>(GlobalData.szEnemySpritePath);
        pPlayerSprite = Resources.LoadAll<Sprite>(GlobalData.szPlayerSpritePath);
        pBulletAnimator = Resources.LoadAll<RuntimeAnimatorController>(GlobalData.szBulletAnimationPath);
        pEffectAnimator = Resources.LoadAll<RuntimeAnimatorController>(GlobalData.szEffectAnimationPath);
        pPlayerAnimator = Resources.LoadAll<RuntimeAnimatorController>(GlobalData.szPlayerAnimationPath);
        pEnemyAnimator = Resources.LoadAll<RuntimeAnimatorController>(GlobalData.szEnemyAnimationPath);
        pCommonAnimator = Resources.LoadAll<RuntimeAnimatorController>(GlobalData.szCommonAnimationPath);
        fDefaultPadding = 150.0f;

        // 자동 회수 존 (프리팹으로 사용할 경우)
        // GameObject pAutoCollectZone = Instantiate(Resources.Load(GlobalData.szMiscellaneousPrefabPath + "AutoCollectZone")) as GameObject;
        // pAutoCollectZone.transform.parent = transform;
        // pAutoCollectZone.SetActive(true);

        iGameProcedure = StageProcedure(GlobalData.enGameDifficulty);
        iGameProcedure.MoveNext();
    }
    public void SetPlayerInfo()
    {
        pPlayer = GameObject.Find("Player").Equals(null) ? Instantiate(Resources.Load(GlobalData.szPlayerPrefabPath)) as GameObject : GameObject.Find("Player");

        PlayerMain pPlayerMain = pPlayer.GetComponent<PlayerMain>();
        pPlayerMain.Init();

        pPlayerBase = pPlayerMain.GetPlayerBase();
        pPlayerBase.SetPlayerType(GlobalData.enSelectPlayerType);
        pPlayerBase.SetPlayerWeaponType(GlobalData.enSelectPlayerWeaponType);
        pPlayerBase.SetPlayerLife(GlobalData.iStartLife);
        pPlayerBase.SetPlayerLifeFragment(0);
        pPlayerBase.SetPlayerSpell(GlobalData.iStartSpell);
        pPlayerBase.SetPlayerSpellFragment(0);
        pPlayerBase.SetPlayerScoreItem(0);
        pPlayerBase.SetPlayerGrazeCount(0);
        pPlayerBase.SetPlayerMissCount(0);
        pPlayerBase.SetPlayerMaxScore(0);
        pPlayerBase.SetPlayerCurrentScore(0);
        pPlayerBase.SetPlayerPower(2.0f);

        for (int i = 1; i <= 4; i++)
        {
            pPlayerBase.GetChildTransform(2).GetChild(i - 1).gameObject.SetActive((int)pPlayerBase.GetPlayerPower() >= i ? true : false);
        }
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator StageProcedure(EGameDifficulty enGameDifficulty)
    {
        if (enGameDifficulty.Equals(EGameDifficulty.enDifficulty_Extra))
        {
            // STAGE EXTRA
            yield return Timing.RunCoroutine(StageExtra());
        }
        else
        {
            // STAGE 1
            yield return Timing.RunCoroutine(Stage1(enGameDifficulty));

            // STAGE 2
            yield return Timing.RunCoroutine(Stage2(enGameDifficulty));

            // STAGE 3
            yield return Timing.RunCoroutine(Stage3(enGameDifficulty));

            // STAGE 4
            yield return Timing.RunCoroutine(Stage4(enGameDifficulty));

            // STAGE 5
            yield return Timing.RunCoroutine(Stage5(enGameDifficulty));

            // STAGE 6
            yield return Timing.RunCoroutine(Stage6(enGameDifficulty));
        }
    }

    #region STAGE CLASSIFICATION
    #region STAGE 1
    public IEnumerator<float> Stage1(EGameDifficulty enGameDifficulty)
    {
        GameObject pEnemy = null;

        yield return Timing.WaitForOneFrame;

        SoundManager.Instance.PlayBGM(EBGM.enBGM_Stage1_Field, 1.0f, true);

        yield return Timing.WaitForSeconds(2.0f);

        pEnemy = EnemyManager.Instance.GetEnemyPool().ExtractEnemy(new Vector2(0.0f, 1.0f), Vector3.one, EEnemyType.enType_TinyFairy_Type3, 100.0f, true, false);
        EnemyManager.Instance.GetEnemyPool().AddSinglePattern(pEnemy, 114, 0, 4.0f, 1.0f);
        // EnemyManager.Instance.GetEnemyPool().AddRepeatPattern(pEnemy, 101, 1.0f);
        // EnemyManager.Instance.GetEnemyPool().AddCounterPattern(pEnemy, 102);
        EnemyManager.Instance.GetEnemyPool().SetEnemyMoveX(pEnemy, 0.0f);
        EnemyManager.Instance.GetEnemyPool().SetEnemyMoveY(pEnemy, 0.0f);

        pEnemy = EnemyManager.Instance.GetEnemyPool().ExtractEnemy(new Vector2(1.5f, -1.0f), Vector3.one, EEnemyType.enType_TinyFairy_Type3, 100.0f, true, false);
        pEnemy = EnemyManager.Instance.GetEnemyPool().ExtractEnemy(new Vector2(-1.5f, 2.0f), Vector3.one, EEnemyType.enType_TinyFairy_Type3, 100.0f, true, false);

        yield return Timing.WaitForOneFrame;

        // UNDER CONSTRUCTION
    }
    #endregion

    #region STAGE 2
    public IEnumerator<float> Stage2(EGameDifficulty enGameDifficulty)
    {
        yield return Timing.WaitForOneFrame;
    }
    #endregion

    #region STAGE 3
    public IEnumerator<float> Stage3(EGameDifficulty enGameDifficulty)
    {
        yield return Timing.WaitForOneFrame;
    }
    #endregion

    #region STAGE 4
    public IEnumerator<float> Stage4(EGameDifficulty enGameDifficulty)
    {
        yield return Timing.WaitForOneFrame;
    }
    #endregion

    #region STAGE 5
    public IEnumerator<float> Stage5(EGameDifficulty enGameDifficulty)
    {
        yield return Timing.WaitForOneFrame;
    }
    #endregion

    #region STAGE 6
    public IEnumerator<float> Stage6(EGameDifficulty enGameDifficulty)
    {
        yield return Timing.WaitForOneFrame;
    }
    #endregion

    #region STAGE EXTRA
    public IEnumerator<float> StageExtra()
    {
        yield return Timing.WaitForOneFrame;
    }
    #endregion
    #endregion
    #endregion
}