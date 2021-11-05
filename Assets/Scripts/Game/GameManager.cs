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
        pPlayerBase.SetPlayerPower(1.98f);

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

        pEnemy = EnemyManager.Instance.GetEnemyPool().ExtractEnemy(new Vector2(0.0f, 1.0f), Vector3.one, EEnemyType.enType_TinyFairy_Type1, 100.0f, true, false);
        EnemyManager.Instance.GetEnemyPool().AddSinglePattern(pEnemy, 110, 0, 4.5f, 1.0f);
        // EnemyManager.Instance.GetEnemyPool().AddRepeatPattern(pEnemy, new KeyValuePair<int, float>(117, 1.0f));
        EnemyManager.Instance.GetEnemyPool().AddItemDictionary(pEnemy, new KeyValuePair<EItemType, int>(EItemType.enType_PowerS, 3), new KeyValuePair<EItemType, int>(EItemType.enType_ScoreS, 1));
        // EnemyManager.Instance.GetEnemyPool().SetEnemyMoveRepeat(pEnemy, 3.0f, EnemyMoveTest);
        // EnemyManager.Instance.GetEnemyPool().SetEnemyMoveOnce(pEnemy, 1.0f,
        //     new KeyValuePair<DelegateGameObject, float>((GameObject pObject) => { iTween.MoveTo(pObject, iTween.Hash("position", new Vector3(2.0f, 2.0f), "easetype", iTween.EaseType.easeOutQuad, "time", 1.0f)); }, 1.5f),
        //     new KeyValuePair<DelegateGameObject, float>((GameObject pObject) => { iTween.MoveTo(pObject, iTween.Hash("position", new Vector3(-2.0f, 2.0f), "easetype", iTween.EaseType.easeOutQuad, "time", 1.5f)); }, 2.0f));
        // EnemyManager.Instance.GetEnemyPool().AddRepeatPattern(pEnemy, 101, 1.0f);
        // EnemyManager.Instance.GetEnemyPool().AddCounterPattern(pEnemy, 102);

        yield return Timing.WaitForOneFrame;

        // UNDER CONSTRUCTION
    }
    public IEnumerator<float> EnemyMoveTest(GameObject pEnemy)
    {
        Vector3 vPosition = Vector3.zero;

        while (pEnemy.activeSelf.Equals(true))
        {
            vPosition = new Vector2(UnityEngine.Random.Range(-2.0f, 2.0f), UnityEngine.Random.Range(1.0f, 2.5f));
            iTween.MoveTo(pEnemy, iTween.Hash("position", vPosition, "easetype", iTween.EaseType.easeOutQuad, "time", 1.0f));

            yield return Timing.WaitForSeconds(3.0f);
        }
        yield break;
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