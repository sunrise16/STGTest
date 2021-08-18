#region USING
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
    [HideInInspector] public RuntimeAnimatorController[] pAnimatonController;
    [HideInInspector] public IEnumerator iGameProcedure;
    [HideInInspector] public float fDefaultPadding;
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        SetPlayerInfo();

        pMainCamera = Camera.main;
        pBulletEffectSprite = Resources.LoadAll<Sprite>(GlobalData.szBulletSpritePath);
        // pItemSprite = Resources.LoadAll<Sprite>(GlobalData.szItemSpritePath);
        pEnemySprite = Resources.LoadAll<Sprite>(GlobalData.szEnemySpritePath);
        pPlayerSprite = Resources.LoadAll<Sprite>(GlobalData.szPlayerSpritePath);
        pAnimatonController = Resources.LoadAll<RuntimeAnimatorController>(GlobalData.szEnemyAnimationPath);
        fDefaultPadding = 150.0f;

        iGameProcedure = StageProcedure(GlobalData.enGameDifficulty);
        iGameProcedure.MoveNext();
    }
    public void SetPlayerInfo()
    {
        pPlayer = GameObject.Find("Player").Equals(null) ? Instantiate(Resources.Load(GlobalData.szPlayerPrefabPath)) as GameObject : GameObject.Find("Player");

        PlayerMain pPlayerMain = pPlayer.GetComponent<PlayerMain>();
        pPlayerMain.Init();

        PlayerBase pPlayerBase = pPlayerMain.GetPlayerBase();
        pPlayerBase.SetPlayerLife(GlobalData.iStartLife);
        pPlayerBase.SetPlayerLifeFragment(0);
        pPlayerBase.SetPlayerSpell(GlobalData.iStartSpell);
        pPlayerBase.SetPlayerSpellFragment(0);
        pPlayerBase.SetPlayerScoreItem(0);
        pPlayerBase.SetPlayerGrazeCount(0);
        pPlayerBase.SetPlayerPower(0.0f);
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

        yield return Timing.WaitForSeconds(2.0f);

        pEnemy = EnemyManager.Instance.GetEnemyPool().ExtractEnemy(new Vector2(0.0f, 1.0f), Vector3.one, EEnemyType.enType_TinyFairy_Type3, 10.0f, false);
        EnemyManager.Instance.GetEnemyPool().AddSinglePattern(pEnemy, 0, 2.0f, 102, 1.5f);
        // EnemyManager.Instance.GetEnemyPool().AddRepeatPattern(pEnemy, Timing.RunCoroutine(Pattern109(pEnemy)));
        EnemyManager.Instance.GetEnemyPool().SetEnemyMoveX(pEnemy, 0.0f);
        EnemyManager.Instance.GetEnemyPool().SetEnemyMoveY(pEnemy, 0.0f);
        // iTween.MoveTo(pEnemy, iTween.Hash("path", paths, "easetype", easeType, "time", moveTime));

        // for (int i = 0; i < 6; i++)
        // {
        //     pEnemy = EnemyManager.Instance.GetEnemyPool().ExtractEnemy
        //         (new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(0.5f, 2.75f)), Vector3.one, EEnemyType.enType_TinyFairy_Type3, 10.0f, 100, false, 1, 0.5f, 0, 3.5f);
        // 
        //     yield return Timing.WaitForSeconds(1.3f);
        // }

        yield return Timing.WaitForOneFrame;
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