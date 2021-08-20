#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public partial class GameManager : UnitySingleton<GameManager> // a.k.a PatternManager
{
    #region VARIABLE
    private CoroutineHandle pTempHandle;
    #endregion

    #region SHOOT PATTERN METHOD
    #region PATTERN 1
    public IEnumerator<float> Pattern1(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float fAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        switch (GlobalData.enGameDifficulty)
        {
            #region EASY / NORMAL
            case EGameDifficulty.enDifficulty_Easy:
            case EGameDifficulty.enDifficulty_Normal:
                for (int i = 0; i < (GlobalData.enGameDifficulty.Equals(EGameDifficulty.enDifficulty_Easy) ? 1 : 3); i++)
                {
                    fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y,Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;

                    GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                        (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
                    EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
                    EffectBase pEffectBase = pEffectMain.GetEffectBase();

                    pEffectBase.SetUniqueNumber(0);
                    pEffectMain.GetTimer().InitTimer(0.15f);
                    pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
                    pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
                    pEffectBase.SetEffect(fAngle, 0.0f, 0.0f, 0.0f, 0.2f, 0.15f, 0.0f);
                    pEffectBase.SetCondition(false);
                    pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 3];
                    pEffectMain.pStartDelegate = null;
                    pEffectMain.pCommonDelegate = () =>
                    {
                        GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                            EEnemyBulletType.enType_Circle, EPlayerBulletType.None, 1.0f, GameManager.Instance.fDefaultPadding);
                        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                        BulletBase pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                        pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                        pBulletBase.SetBulletSpeed(GlobalData.enGameDifficulty.Equals(EGameDifficulty.enDifficulty_Easy) ? 3.0f : 5.0f, 0.0f, 0.0f, 0.1f,
                            GlobalData.enGameDifficulty.Equals(EGameDifficulty.enDifficulty_Easy) ? 2.0f : 2.5f);
                        pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Circle) + 6];
                        pBulletMain.pCommonDelegate = null;
                        pBulletMain.pConditionDelegate = null;
                        pBulletMain.pChangeDelegate = null;
                        pBulletMain.pSplitDelegate = null;
                        pBulletMain.pAttachDelegate = null;
                    };
                    pEffectMain.pConditionDelegate = null;

                    yield return Timing.WaitForSeconds(1.0f);
                }
                break;
            #endregion
            #region HARD / LUNATIC
            case EGameDifficulty.enDifficulty_Hard:
            case EGameDifficulty.enDifficulty_Lunatic:
                for (int i = 0; i < 30; i++)
                {
                    fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y, Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;

                    GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                        (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
                    EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
                    EffectBase pEffectBase = pEffectMain.GetEffectBase();

                    pEffectBase.SetUniqueNumber(0);
                    pEffectMain.GetTimer().InitTimer(0.15f);
                    pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
                    pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
                    pEffectBase.SetEffect(fAngle, 0.0f, 0.0f, 0.0f, 0.2f, 0.15f, 0.0f);
                    pEffectBase.SetCondition(false);
                    pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 3];
                    pEffectMain.pStartDelegate = null;
                    pEffectMain.pCommonDelegate = () =>
                    {
                        GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                            EEnemyBulletType.enType_Circle, EPlayerBulletType.None, 1.0f, GameManager.Instance.fDefaultPadding);
                        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                        BulletBase pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                        pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                        pBulletBase.SetBulletSpeed(GlobalData.enGameDifficulty.Equals(EGameDifficulty.enDifficulty_Hard) ? 1.5f : 2.0f, 0.1f,
                            GlobalData.enGameDifficulty.Equals(EGameDifficulty.enDifficulty_Hard) ? 4.5f : 6.0f, 0.0f, 0.0f);
                        pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Circle) + 6];
                        pBulletMain.pCommonDelegate = null;
                        pBulletMain.pConditionDelegate = null;
                        pBulletMain.pChangeDelegate = null;
                        pBulletMain.pSplitDelegate = null;
                        pBulletMain.pAttachDelegate = null;
                    };
                    pEffectMain.pConditionDelegate = null;

                    yield return Timing.WaitForSeconds(GlobalData.enGameDifficulty.Equals(EGameDifficulty.enDifficulty_Hard) ? 0.25f : 0.15f);
                }
                break;
            #endregion
            default:
                break;
        }
        pEnemyMain.GetSinglePatternList().Remove(1);
        yield break;
    }
    #endregion

    #region PATTERN 100
    public IEnumerator<float> Pattern100(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vTempPosition = Vector3.zero;
        Vector3 vScale = new Vector3(2.0f, 2.0f, 1.0f);
        int iSpriteIndex = UnityEngine.Random.Range(2, 7);
        float fAngle = 0.0f;
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        for (int i = 0; i < 4; i++)
        {
            fAngle = UnityEngine.Random.Range(-8.0f, 8.0f);
            SoundManager.Instance.PlaySE(ESE.enSE_Tan00, 0.8f);

            for (int j = 0; j < 54; j++)
            {
                fTempAngle = fAngle + (7.5f * j);
                vTempPosition = Utility.Instance.GetBulletPosition(vPosition, 0.25f, fTempAngle);

                GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                    (vTempPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
                EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
                EffectBase pEffectBase = pEffectMain.GetEffectBase();

                pEffectBase.SetUniqueNumber(i);
                pEffectMain.GetTimer().InitTimer(0.15f);
                pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
                pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
                pEffectBase.SetEffect(fTempAngle, 0.0f, 0.0f, 0.0f, 0.15f, 0.1f, 0.0f);
                pEffectBase.SetCondition(false);
                pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect)];
                pEffectMain.pStartDelegate = null;
                pEffectMain.pCommonDelegate = () =>
                {
                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Butterfly, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletMain.GetPatternTimer().InitTimer(1.0f);
                    pBulletMain.GetRotateTimer().InitTimer(2.0f);
                    pBulletBase.SetBulletSpeed(0.1f, 0.01f, 2.0f);
                    pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f, (pEffectBase.GetUniqueNumber() % 2).Equals(0) ? 40.0f : -40.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Butterfly) + iSpriteIndex];
                    pBulletMain.pCommonDelegate = () =>
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            SoundManager.Instance.PlaySE(ESE.enSE_Kira00, 0.8f);

                            GameObject pBulletObjectSub = BulletManager.Instance.GetBulletPool().ExtractBullet
                                (pBulletBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                                EEnemyBulletType.enType_Circle, EPlayerBulletType.None, 1.0f, 10.0f);
                            BulletMain pBulletMainSub = pBulletObjectSub.GetComponent<BulletMain>();
                            BulletBase pBulletBaseSub = pBulletMainSub.GetBulletBase();

                            pBulletBaseSub.SetUniqueNumber(0);
                            pBulletMainSub.GetPatternTimer().InitTimer(0, 0.0f, false);
                            pBulletMainSub.GetRotateTimer().InitTimer(1.0f);
                            pBulletBaseSub.SetBulletSpeed(pBulletBase.GetBulletMoveSpeed(), 0.015f + (0.005f * k), 2.25f + (0.25f * k));
                            pBulletBaseSub.SetBulletRotate(pBulletBase.GetRotationZ(), pBulletBase.GetBulletRotateSpeed());
                            pBulletBaseSub.SetBulletOption();
                            pBulletBaseSub.SetCondition(false);
                            pBulletBaseSub.SetCollisionDestroy(true);
                            pBulletBaseSub.SetColliderTrigger(true);
                            pBulletBaseSub.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Butterfly) + iSpriteIndex];
                            pBulletMainSub.pCommonDelegate = null;
                            pBulletMainSub.pConditionDelegate = null;
                            pBulletMainSub.pChangeDelegate = null;
                            pBulletMainSub.pSplitDelegate = null;
                            pBulletMainSub.pAttachDelegate = null;
                        }
                    };
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                };
                pEffectMain.pConditionDelegate = null;
            }
            yield return Timing.WaitForSeconds(0.5f);
        }
        pEnemyMain.GetSinglePatternList().Remove(100);
        yield break;
    }
    #endregion

    #region PATTERN 101
    public IEnumerator<float> Pattern101(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vTempPosition = Vector3.zero;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float[] fAngle = new float[8];
        float fDestinationAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        while (true)
        {
            fDestinationAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y, Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;

            for (int i = 0; i < fAngle.Length; i++)
            {
                fAngle[i] = UnityEngine.Random.Range(fDestinationAngle - 90.0f, fDestinationAngle + 90.0f);
            }
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    vTempPosition = Utility.Instance.GetBulletPosition(vPosition, 0.15f, fAngle[j]);

                    GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                        (vTempPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
                    EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
                    EffectBase pEffectBase = pEffectMain.GetEffectBase();

                    pEffectBase.SetUniqueNumber(j);
                    pEffectMain.GetTimer().InitTimer(0.15f);
                    pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
                    pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
                    pEffectBase.SetEffect(fAngle[j], 0.0f, 0.0f, 0.0f, 0.15f, 0.12f, 0.0f);
                    pEffectBase.SetCondition(false);
                    pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 4];
                    pEffectMain.pStartDelegate = null;
                    pEffectMain.pCommonDelegate = () =>
                    {
                        GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy,
                            EEnemyBulletType.enType_Capsule, EPlayerBulletType.None, 1.0f, 10.0f);
                        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                        BulletBase pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                        pBulletMain.GetRotateTimer().InitTimer(2.0f);
                        pBulletBase.SetBulletSpeed(2.0f);
                        pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f, (pEffectBase.GetUniqueNumber() % 2).Equals(0) ? 30.0f : -30.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Arrowhead) + 8];
                        pBulletMain.pCommonDelegate = null;
                        pBulletMain.pConditionDelegate = null;
                        pBulletMain.pChangeDelegate = null;
                        pBulletMain.pSplitDelegate = null;
                        pBulletMain.pAttachDelegate = null;
                    };
                    pEffectMain.pConditionDelegate = null;
                }
                yield return Timing.WaitForSeconds(0.03f);
            }
            yield return Timing.WaitForSeconds(0.02f);
        }
    }
    #endregion

    #region PATTERN 102
    public IEnumerator<float> Pattern102(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false, int iFireCount = 0)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vTempPosition = Vector3.zero;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float fAngle = UnityEngine.Random.Range(0.0f, 360.0f);
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        for (int i = 0; i < 12; i++)
        {
            SoundManager.Instance.PlaySE(ESE.enSE_Tan00, 0.8f);

            for (int j = 0; j < 5; j++)
            {
                fTempAngle = (iFireCount % 2).Equals(0) ? fAngle + (72.0f * j) + (6.0f * i) : fAngle + (72.0f * j) - (6.0f * i);
                vTempPosition = Utility.Instance.GetBulletPosition(vPosition, 0.3f, fTempAngle);

                GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                    (vTempPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
                EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
                EffectBase pEffectBase = pEffectMain.GetEffectBase();

                pEffectBase.SetUniqueNumber(i + 1);
                pEffectMain.GetTimer().InitTimer(0.2f);
                pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
                pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
                pEffectBase.SetEffect(fTempAngle, 0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.0f);
                pEffectBase.SetCondition(false);
                pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + ((iFireCount % 2).Equals(0) ? 1 : 3)];
                pEffectMain.pStartDelegate = null;
                pEffectMain.pCommonDelegate = () =>
                {
                    for (int k = 0; k < 2; k++)
                    {
                        GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy,
                            EEnemyBulletType.enType_Knife, EPlayerBulletType.None, 1.0f, 15.0f);
                        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                        BulletBase pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetUniqueNumber(0);
                        pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                        pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                        pBulletBase.SetBulletSpeed((3.0f - (0.08f * pEffectBase.GetUniqueNumber())) * (1.0f - (0.5f * k)));
                        pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f);
                        pBulletBase.SetBulletOption(true);
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Knife) + ((iFireCount % 2).Equals(0) ? 1 : 3)];
                        pBulletMain.pCommonDelegate = null;
                        pBulletMain.pConditionDelegate = () =>
                        {
                            if (pBulletBase.GetBulletReflect().Equals(false))
                            {
                                SoundManager.Instance.PlaySE(ESE.enSE_Kira00, 0.8f);
                                pBulletMain.pConditionDelegate = null;
                            }
                        };
                        pBulletMain.pChangeDelegate = null;
                        pBulletMain.pSplitDelegate = null;
                        pBulletMain.pAttachDelegate = null;
                    }
                };
                pEffectMain.pConditionDelegate = null;
            }
            yield return Timing.WaitForSeconds(0.06f);
        }
        pEnemyMain.GetSinglePatternList().Remove(102);
        yield break;
    }
    #endregion

    #region PATTERN 103
    public IEnumerator<float> Pattern103(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y, Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
            (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
        EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
        EffectBase pEffectBase = pEffectMain.GetEffectBase();

        pEffectBase.SetUniqueNumber(0);
        pEffectMain.GetTimer().InitTimer(0.25f);
        pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
        pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
        pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.08f, 0.0f);
        pEffectBase.SetCondition(false);
        pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect)];
        pEffectMain.pStartDelegate = null;
        pEffectMain.pCommonDelegate = () =>
        {
            for (int i = 0; i < 72; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    fTempAngle = fAngle + (5.0f * i) + ((j % 2).Equals(1) ? 2.5f : 0.0f);

                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Circle, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                    pBulletBase.SetBulletSpeed(3.6f - (0.9f * j));
                    pBulletBase.SetBulletRotate(fTempAngle - 90.0f);
                    pBulletBase.SetBulletOption(false, false, true);
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Circle)];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = () =>
                    {
                        if (pBulletBase.GetPositionY() < -3.75f)
                        {
                            return;
                        }

                        GameObject pEffectObjectSub = EffectManager.Instance.GetEffectPool().ExtractEffect
                            (pBulletBase.GetPosition(), vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
                        EffectMain pEffectMainSub = pEffectObjectSub.GetComponent<EffectMain>();
                        EffectBase pEffectBaseSub = pEffectMainSub.GetEffectBase();

                        pEffectBaseSub.SetUniqueNumber(0);
                        pEffectMainSub.GetTimer().InitTimer(0.1f);
                        pEffectBaseSub.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.12f, 0.15f, 0.0f);
                        pEffectBaseSub.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect)];
                        pEffectMainSub.pCommonDelegate = null;

                        fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(pBulletBase.GetPosition(), pPlayer).y, Utility.Instance.GetAimedDestination(pBulletBase.GetPosition(), pPlayer).x) * Mathf.Rad2Deg;
                        
                        GameObject pBulletObjectSub = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pBulletBase.GetPosition(), Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy,
                            EEnemyBulletType.enType_Knife, EPlayerBulletType.None, 1.0f, 15.0f);
                        BulletMain pBulletMainSub = pBulletObjectSub.GetComponent<BulletMain>();
                        BulletBase pBulletBaseSub = pBulletMainSub.GetBulletBase();

                        pBulletBaseSub.SetUniqueNumber(0);
                        pBulletMainSub.GetPatternTimer().InitTimer(0, 0.0f, false);
                        pBulletMainSub.GetRotateTimer().InitTimer(0, 0.0f, false);
                        pBulletBaseSub.SetBulletSpeed(0.0f, 0.02f, 3.0f);
                        pBulletBaseSub.SetBulletRotate(fAngle - 90.0f);
                        pBulletBaseSub.SetBulletOption();
                        pBulletBaseSub.SetCondition(false);
                        pBulletBaseSub.SetCollisionDestroy(true);
                        pBulletBaseSub.SetColliderTrigger(true);
                        pBulletBaseSub.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Knife) + UnityEngine.Random.Range(1, 7)];
                        pBulletMainSub.pCommonDelegate = null;
                        pBulletMainSub.pConditionDelegate = null;
                        pBulletMainSub.pChangeDelegate = null;
                        pBulletMainSub.pSplitDelegate = null;
                        pBulletMainSub.pAttachDelegate = null;

                        pBulletMain.GetRotateTimer().ResetTimer(pBulletMain.GetRotateTimer().GetResetTime());
                        BulletManager.Instance.GetBulletPool().ReturnPool(pBulletObject);
                    };
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            }
            pEffectMain.pConditionDelegate = null;
        };
        pEnemyMain.GetSinglePatternList().Remove(103);
        yield break;
    }
    #endregion

    #region PATTERN 104
    public IEnumerator<float> Pattern104(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y, Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 36; j++)
            {
                fTempAngle = fAngle + (10.0f * j);

                GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                    (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
                EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
                EffectBase pEffectBase = pEffectMain.GetEffectBase();

                pEffectBase.SetUniqueNumber(j);
                pEffectMain.GetTimer().InitTimer(0.15f);
                pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
                pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
                pEffectBase.SetEffect(fTempAngle, 0.0f, 0.0f, 0.0f, 0.15f, 0.12f, 0.0f);
                pEffectBase.SetCondition(false);
                pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + ((pEffectBase.GetUniqueNumber() % 2).Equals(0) ? 3 : 5)];
                pEffectMain.pStartDelegate = null;
                pEffectMain.pCommonDelegate = () =>
                {
                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Capsule, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(pEffectBase.GetUniqueNumber());
                    pBulletMain.GetPatternTimer().InitTimer(1.7f);
                    pBulletMain.GetRotateTimer().InitTimer(1.55f);
                    pBulletBase.SetBulletSpeed(6.0f);
                    pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f, (pBulletBase.GetUniqueNumber() % 2).Equals(0) ? 360.0f : -360.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Capsule) + ((pBulletBase.GetUniqueNumber() % 2).Equals(0) ? 6 : 10)];
                    pBulletMain.pCommonDelegate = () =>
                    {
                        pBulletBase.SetBulletSpeed(pBulletBase.GetBulletMoveSpeed(), 0.0f, 0.0f, 0.2f, 1.5f);
                        pBulletMain.GetRotateTimer().InitTimer(UnityEngine.Random.Range(0.0f, 0.4f));
                        if ((pBulletBase.GetUniqueNumber() % 2).Equals(0))
                        {
                            pBulletBase.SetBulletRotateSpeed(UnityEngine.Random.Range(0.0f, 60.0f));
                        }
                        else
                        {
                            pBulletBase.SetBulletRotateSpeed(UnityEngine.Random.Range(-60.0f, 0.0f));
                        }
                    };
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                };
                pEffectMain.pConditionDelegate = null;
            }
            yield return Timing.WaitForSeconds(0.07f);
        }
        pEnemyMain.GetSinglePatternList().Remove(104);
        yield break;
    }
    #endregion

    #region PATTERN 105
    public IEnumerator<float> Pattern105(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vTempPosition = Vector3.zero;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float fAngle = fAngle = UnityEngine.Random.Range(0.0f, 360.0f);
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        for (int i = 0; i < 32; i++)
        {
            fTempAngle = fAngle + (11.25f * i);
            vTempPosition = Utility.Instance.GetBulletPosition(vPosition, 0.2f, fTempAngle);

            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vTempPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(i);
            pEffectMain.GetTimer().InitTimer(0.1f);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(fTempAngle, 0.0f, 0.0f, 0.0f, 0.18f, 0.15f, 0.0f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 5];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                for (int j = 0; j < 2; j++)
                {
                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                        (pEffectBase.GetUniqueNumber() % 2).Equals(0) ? EEnemyBulletType.enType_Circle : EEnemyBulletType.enType_TinyCircle, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                    pBulletBase.SetBulletSpeed(j.Equals(0) ? 3.0f : 2.0f, 0.0f, 0.0f, j.Equals(0) ? 0.03f : 0.02f, j.Equals(0) ? 2.0f : 1.0f);
                    pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32((pEffectBase.GetUniqueNumber() % 2).Equals(0) ? EEnemyBulletType.enType_Circle : EEnemyBulletType.enType_TinyCircle) + 11];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            };
            pEffectMain.pConditionDelegate = null;
        }
        pEnemyMain.GetSinglePatternList().Remove(105);
        yield break;
    }

    #endregion

    #region PATTERN 106
    public IEnumerator<float> Pattern106(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float fAngle = 0.0f;
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        for (int i = 0; i < 8; i++)
        {
            fAngle = UnityEngine.Random.Range(0.0f, 5.0f);

            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(0);
            pEffectMain.GetTimer().InitTimer(0.2f);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.12f, 0.1f, 0.0f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 2 + ((i + 1) / 2)];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                for (int j = 0; j < 72; j++)
                {
                    fTempAngle = fAngle + (5 * j);

                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Circle, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                    pBulletBase.SetBulletSpeed(2.0f);
                    pBulletBase.SetBulletRotate(fTempAngle - 90.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Circle) + (4 + i)];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            };
            pEffectMain.pConditionDelegate = null;

            yield return Timing.WaitForSeconds(0.1f);
        }
        pEnemyMain.GetSinglePatternList().Remove(106);
        yield break;
    }
    #endregion

    #region PATTERN 107
    public IEnumerator<float> Pattern107(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(3.0f, 3.0f, 1.0f);
        float fAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        for (int i = 0; i < 20; i++)
        {
            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(0);
            pEffectMain.GetTimer().InitTimer(0.15f);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.15f, 0.15f, 0.0f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 3];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                for (int j = 0; j < 24; j++)
                {
                    fAngle = Mathf.Atan2(Utility.Instance.GetRandomDestination(pEffectObject).y, Utility.Instance.GetRandomDestination(pEffectObject).x) * Mathf.Rad2Deg;

                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Circle, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                    pBulletBase.SetBulletSpeed(UnityEngine.Random.Range(2.0f, 7.0f));
                    pBulletBase.SetBulletRotate(fAngle - 90.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Circle) + 6];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            };
            pEffectMain.pConditionDelegate = null;

            yield return Timing.WaitForSeconds(0.1f);
        }
        pEnemyMain.GetSinglePatternList().Remove(107);
        yield break;
    }
    #endregion

    #region PATTERN 108
    public IEnumerator<float> Pattern108(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0.25f);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vTempPosition = Vector3.zero;
        Vector3 vScale = new Vector3(6.0f, 6.0f, 1.0f);
        int iFireCount = 0;
        float fAngle = 0.0f;
        float fFirstAngle = 0.0f;
        float fSecondAngle = 0.0f;
        float fTempAngle = UnityEngine.Random.Range(0.125f, 0.25f);

        yield return Timing.WaitForOneFrame;

        while (true)
        {
            iFireCount++;
            if (iFireCount.Equals(20))
            {
                iFireCount = 0;
                if (fSecondAngle > 20.0f)
                {
                    fTempAngle = UnityEngine.Random.Range(-4.0f, -2.0f);
                }
                else if (fSecondAngle > 5.0f && fSecondAngle <= 20.0f)
                {
                    fTempAngle = UnityEngine.Random.Range(-1.0f, -0.1f);
                }
                else if (fSecondAngle >= -20.0f && fSecondAngle < -5.0f)
                {
                    fTempAngle = UnityEngine.Random.Range(0.1f, 1.0f);
                }
                else if (fSecondAngle < -20.0f)
                {
                    fTempAngle = UnityEngine.Random.Range(2.0f, 4.0f);
                }
                else
                {
                    fTempAngle = UnityEngine.Random.Range(-0.5f, 0.5f);
                }
            }
            fSecondAngle += fTempAngle;
            fFirstAngle += fSecondAngle;

            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(0);
            pEffectMain.GetTimer().InitTimer(0.2f);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.15f, 0.12f, 0.0f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 3];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                for (int i = 0; i < 24; i++)
                {
                    fAngle = fFirstAngle + (15.0f * i);
                    vTempPosition = Utility.Instance.GetBulletPosition(vPosition, 0.35f, fAngle);

                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (vTempPosition, Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy, 
                        EEnemyBulletType.enType_Capsule, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                    pBulletBase.SetBulletSpeed(0.5f, 0.05f, 4.0f);
                    pBulletBase.SetBulletRotate(fAngle - 90.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Capsule) + 6];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            };
            pEffectMain.pConditionDelegate = null;

            yield return Timing.WaitForSeconds(0.07f);
        }
    }
    #endregion

    #region PATTERN 109
    public IEnumerator<float> Pattern109(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0.25f);
        Vector3 vPosition = Vector3.one;
        Vector3 vScale = new Vector3(3.0f, 3.0f, 1.0f);
        int iFireCount = 0;
        float fAngle = 0.0f;
        float fTempAngle = 0.0f;

        yield return Timing.WaitForSeconds(1.0f);

        while (true)
        {
            fAngle = 3.5f * iFireCount;
            vPosition = pEnemyObject.GetComponent<Transform>().position;

            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(0);
            pEffectMain.GetTimer().InitTimer(0.15f);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(fAngle, 0.0f, 0.0f, 0.0f, 0.2f, 0.15f, 0.0f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 3];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                for (int j = 0; j < 12; j++)
                {
                    fTempAngle = pEffectBase.GetEffectRotateAngle() + (30.0f * j);

                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (vPosition, Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Capsule, EPlayerBulletType.None, 1.0f, 10.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(0);
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                    pBulletBase.SetBulletSpeed(0.3f, 0.03f, 3.0f);
                    pBulletBase.SetBulletRotate(fTempAngle - 90.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Capsule) + 6];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = null;
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            };
            pEffectMain.pConditionDelegate = null;

            yield return Timing.WaitForSeconds(0.05f);

            iFireCount++;
            if (iFireCount >= 120)
            {
                iFireCount = 0;
            }
        }
    }
    #endregion

    #region PATTERN 110
    public IEnumerator<float> Pattern110(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        EnemyBase pEnemyBase = pEnemyObject.GetComponent<EnemyMain>().GetEnemyBase();
        Color pColor = new Color(1, 1, 1, 0.25f);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(3.0f, 3.0f, 1.0f);
        float fAngle = 0.0f;
        float fTempSpeed = 0.0f;

        yield return Timing.WaitForOneFrame;

        SoundManager.Instance.PlaySE(ESE.enSE_Tan00, 0.8f);

        for (int i = 0; i < 2; i++)
        {
            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(i);
            pEffectMain.GetTimer().InitTimer(0.2f);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.15f, 0.12f, 0.0f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + (i.Equals(0) ? 1 : 3)];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                for (int j = 0; j < 160; j++)
                {
                    fAngle = Mathf.Atan2(Utility.Instance.GetRandomDestination(vPosition).y, Utility.Instance.GetRandomDestination(vPosition).x) * Mathf.Rad2Deg;
                    fTempSpeed = UnityEngine.Random.Range(2.0f, 8.0f);

                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (vPosition, Vector3.one, EBulletType.enType_Circle, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Butterfly, EPlayerBulletType.None, 1.0f, GameManager.Instance.fDefaultPadding);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(pEffectBase.GetUniqueNumber());
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                    pBulletBase.SetBulletSpeed(fTempSpeed, 0.0f, 0.0f, fTempSpeed * 0.007f, 0.0f);
                    pBulletBase.SetBulletRotate(fAngle - 90.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Butterfly) + (pBulletBase.GetUniqueNumber().Equals(0) ? 1 : 2)];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = () =>
                    {
                        if (pBulletBase.GetBulletMoveSpeed() <= 0.0f && pBulletBase.GetCondition().Equals(false))
                        {
                            SoundManager.Instance.PlaySE(ESE.enSE_Kira00, 0.8f);

                            pBulletMain.GetRotateTimer().InitTimer(2.5f);
                            pBulletBase.SetBulletSpeed(0.0f, 0.01f, 3.0f, 0.0f, 0.0f);
                            pBulletBase.SetBulletRotate(pBulletBase.GetRotationZ(), (pBulletBase.GetUniqueNumber() % 2).Equals(0) ? 75.0f : -75.0f);
                            pBulletBase.SetCondition(true);
                        }
                    };
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            };
            pEffectMain.pConditionDelegate = null;
        }
        pEnemyMain.GetSinglePatternList().Remove(110);
        yield break;
    }
    #endregion

    #region PATTERN 111
    public IEnumerator<float> Pattern111(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0.25f);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(3.0f, 3.0f, 1.0f);
        float fAngle = 0.0f;
        float fTempAngle = Mathf.Atan2(Utility.Instance.GetRandomDestination(vPosition).y, Utility.Instance.GetRandomDestination(vPosition).x) * Mathf.Rad2Deg;

        yield return Timing.WaitForOneFrame;

        for (int i = 0; i < 8; i++)
        {
            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(0);
            pEffectMain.GetTimer().InitTimer(0.15f);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.15f, 0.12f, 0.0f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 3];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                for (int j = 0; j < 36; j++)
                {
                    fAngle = fTempAngle + (10.0f * j);

                    GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                        (vPosition, Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy,
                        EEnemyBulletType.enType_Capsule, EPlayerBulletType.None, 1.0f, 50.0f);
                    BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                    BulletBase pBulletBase = pBulletMain.GetBulletBase();

                    pBulletBase.SetUniqueNumber(pEffectBase.GetUniqueNumber());
                    pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                    pBulletMain.GetRotateTimer().InitTimer(0, 2.0f, 0);
                    pBulletBase.SetBulletSpeed(2.0f);
                    pBulletBase.SetBulletRotate(fAngle - 90.0f, 180.0f);
                    pBulletBase.SetBulletOption();
                    pBulletBase.SetCondition(false);
                    pBulletBase.SetCollisionDestroy(true);
                    pBulletBase.SetColliderTrigger(true);
                    pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Capsule) + 6];
                    pBulletMain.pCommonDelegate = null;
                    pBulletMain.pConditionDelegate = () =>
                    {
                        if (pBulletBase.GetBulletRotateAngle() >= 360.0f && pBulletBase.GetCondition().Equals(false))
                        {
                            pBulletMain.GetRotateTimer().InitTimer(0, 10.0f, 0);
                            pBulletBase.SetBulletRotateAngle(0.0f);
                            pBulletBase.SetBulletRotate(pBulletBase.GetRotationZ(), 360.0f);
                            pBulletBase.SetCondition(true);
                            pBulletMain.pConditionDelegate = () =>
                            {
                                if (pBulletBase.GetBulletRotateAngle() >= 180.0f)
                                {
                                    pBulletBase.SetBulletRotateAngle(0.0f);
                                    pBulletBase.SetBulletRotate(pBulletBase.GetRotationZ(), -pBulletBase.GetBulletRotateSpeed());
                                }
                            };
                        }
                    };
                    pBulletMain.pChangeDelegate = null;
                    pBulletMain.pSplitDelegate = null;
                    pBulletMain.pAttachDelegate = null;
                }
            };
            pEffectMain.pConditionDelegate = null;

            yield return Timing.WaitForSeconds(0.04f);
        }
        pEnemyMain.GetSinglePatternList().Remove(111);
        yield break;
    }
    #endregion

    #region PATTERN 112
    public IEnumerator<float> Pattern112(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false, int iFireCount = 0)
    {
        Texture2D pTexture = Resources.Load("Sprites/Bullet/Bullets") as Texture2D;
        Color pColor = new Color(1, 1, 1, 1.0f);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vTempPosition = Vector3.zero;
        Vector3 vScale = new Vector3(2.0f, 2.0f, 1.0f);
        float fAngle = Mathf.Atan2(Utility.Instance.GetRandomDestination(vPosition).y, Utility.Instance.GetRandomDestination(vPosition).x) * Mathf.Rad2Deg;
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        SoundManager.Instance.PlaySE(ESE.enSE_Laser00, 1.0f);

        for (int i = 0; i < 36; i++)
        {
            fTempAngle = fAngle + (10.0f * i);
            vTempPosition = Utility.Instance.GetBulletPosition(vPosition, 0.2f, fTempAngle);

            GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
                (vTempPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_MovingCurvedLaserShot);
            EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
            EffectBase pEffectBase = pEffectMain.GetEffectBase();

            pEffectBase.SetUniqueNumber(0);
            pEffectMain.GetTimer().InitTimer(16, 0.03f, 0);
            pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
            pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
            pEffectBase.SetEffect(fTempAngle, 0.0f, 120.0f, 0.4f, 0.4f, 0.0f, 0.0f, 1.6f);
            pEffectBase.SetCondition(false);
            pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 4];
            pEffectMain.pStartDelegate = null;
            pEffectMain.pCommonDelegate = () =>
            {
                var vRect = new Rect(new Vector2(276.0f + (15.5f * (pEffectMain.GetTimer().GetRepeatCount() - 1)), 876.0f), new Vector2(15.5f, 8.0f));
                var vSprite = Sprite.Create(pTexture, vRect, Vector2.one * 0.5f);

                GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                    (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Box, EBulletShooter.enShooter_Enemy,
                    EEnemyBulletType.enType_CurvedLaser, EPlayerBulletType.None, 1.0f, 10.0f);
                BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                BulletBase pBulletBase = pBulletMain.GetBulletBase();

                pBulletBase.SetUniqueNumber(0);
                pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                pBulletMain.GetRotateTimer().InitTimer(0, 2.0f, 0);
                pBulletBase.SetBulletSpeed(3.5f);
                pBulletBase.SetBulletRotate(pEffectBase.GetEffectRotateAngle() - 90.0f, (iFireCount % 2).Equals(0) ? 180.0f : -180.0f);
                pBulletBase.SetBulletOption();
                pBulletBase.SetCondition(false);
                pBulletBase.SetCollisionDestroy(true);
                pBulletBase.SetColliderTrigger(true);
                pBulletBase.GetSpriteRenderer().sprite = vSprite;
                pBulletBase.GetTransform().GetChild(0).localRotation = Quaternion.Euler(0, 0, -90.0f);
                pBulletMain.pCommonDelegate = null;
                pBulletMain.pConditionDelegate = () =>
                {
                    if (pBulletBase.GetBulletRotateAngle() >= 360.0f && pBulletBase.GetCondition().Equals(false))
                    {
                        SoundManager.Instance.PlaySE(ESE.enSE_Boon01, 1.0f);

                        pBulletMain.GetRotateTimer().InitTimer(0, 10.0f, 0);
                        pBulletBase.SetBulletRotateAngle(0.0f);
                        pBulletBase.SetBulletRotate(pBulletBase.GetRotationZ(), 360.0f);
                        pBulletBase.SetCondition(true);
                        pBulletMain.pConditionDelegate = () =>
                        {
                            if (pBulletBase.GetBulletRotateAngle() >= 180.0f)
                            {
                                pBulletBase.SetBulletRotateAngle(0.0f);
                                pBulletBase.SetBulletRotate(pBulletBase.GetRotationZ(), -pBulletBase.GetBulletRotateSpeed());
                            }
                        };
                    }
                };
                pBulletMain.pChangeDelegate = null;
                pBulletMain.pSplitDelegate = null;
                pBulletMain.pAttachDelegate = null;
            };
            pEffectMain.pConditionDelegate = null;
        }
        pEnemyMain.GetSinglePatternList().Remove(112);
        yield break;
    }
    #endregion

    #region PATTERN 113
    public IEnumerator<float> Pattern113(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 1.0f);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(2.0f, 2.0f, 1.0f);
        float fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y, Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;

        yield return Timing.WaitForOneFrame;

        GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
            (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_HoldingLaserShot);
        EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
        EffectBase pEffectBase = pEffectMain.GetEffectBase();

        pEffectBase.SetUniqueNumber(0);
        pEffectMain.GetTimer().InitTimer(0, 0.03f, 0);
        pEffectMain.GetLaserDelayTimer().InitTimer(0.75f);
        pEffectMain.GetLaserActiveTimer().InitTimer(2.5f, 0, 0.0f, false);
        pEffectBase.SetEffect(0.0f, 0.0f, 120.0f, 0.1f, 0.1f, 0.0f, 0.0f, 1.8f);
        pEffectBase.SetCondition(false);
        pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 4];
        pEffectMain.pStartDelegate = () =>
        {
            for (int i = 0; i < 40; i++)
            {
                GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                    (new Vector2(pEffectBase.GetPositionX(), pEffectBase.GetPositionY() + (0.13f * i)), new Vector3(0.1f, 2.0f, 1.0f), EBulletType.enType_Box,
                    EBulletShooter.enShooter_Enemy, EEnemyBulletType.enType_BoxLaser, EPlayerBulletType.None, 1.0f, 50.0f);
                BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                BulletBase pBulletBase = pBulletMain.GetBulletBase();

                pBulletBase.GetTransform().parent = pEffectBase.GetTransform();
                pBulletBase.SetUniqueNumber(0);
                pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                pBulletBase.SetBulletSpeed(5.0f);
                pBulletBase.SetBulletRotate(180.0f);
                pBulletBase.SetBulletOption();
                pBulletBase.SetCondition(false);
                pBulletBase.SetCollisionDestroy(true);
                pBulletBase.SetColliderTrigger(false);
                pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_BoxLaser) + 7];
                pBulletMain.pCommonDelegate = null;
                pBulletMain.pConditionDelegate = null;
                pBulletMain.pChangeDelegate = null;
                pBulletMain.pSplitDelegate = null;
                pBulletMain.pAttachDelegate = null;
            }
            pEffectBase.SetRotationZ(fAngle - 90.0f);
        };
        pEffectMain.pCommonDelegate = () =>
        {
            GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                (pEffectBase.GetPosition(), new Vector3(0.1f, 2.0f, 1.0f), EBulletType.enType_Box, EBulletShooter.enShooter_Enemy,
                EEnemyBulletType.enType_BoxLaser, EPlayerBulletType.None, 1.0f, 50.0f);
            BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
            BulletBase pBulletBase = pBulletMain.GetBulletBase();

            pBulletBase.GetTransform().parent = pEffectBase.GetTransform();
            pBulletBase.SetUniqueNumber(0);
            pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
            pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
            pBulletBase.SetBulletSpeed(5.0f);
            pBulletBase.SetBulletRotate(pEffectBase.GetRotationZ());
            pBulletBase.SetBulletOption();
            pBulletBase.SetCondition(false);
            pBulletBase.SetColliderTrigger(false);
            pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_BoxLaser) + 7];
            pBulletMain.pCommonDelegate = null;
            pBulletMain.pConditionDelegate = null;
            pBulletMain.pChangeDelegate = null;
            pBulletMain.pSplitDelegate = null;
            pBulletMain.pAttachDelegate = null;
        };
        pEffectMain.pConditionDelegate = () =>
        {
            if (pEffectMain.GetScale() >= 1.0f && pEffectBase.GetCondition().Equals(false))
            {
                pEffectBase.SetCondition(true);
                pEffectBase.SetEffectParentRotateSpeed(60.0f);
            }
        };
        pEnemyMain.GetSinglePatternList().Remove(113);
        yield break;
    }
    #endregion

    #region PATTERN 114
    public IEnumerator<float> Pattern114(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 0);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(2.5f, 2.5f, 1.0f);
        float fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y, Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;
        float fTempAngle = 0.0f;

        yield return Timing.WaitForOneFrame;

        GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
            (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_BulletShot);
        EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
        EffectBase pEffectBase = pEffectMain.GetEffectBase();

        pEffectBase.SetUniqueNumber(0);
        pEffectMain.GetTimer().InitTimer(0.25f);
        pEffectMain.GetLaserDelayTimer().InitTimer(0, 0.0f, false);
        pEffectMain.GetLaserActiveTimer().InitTimer(0, 0.0f, false);
        pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.1f, 0.08f, 0.0f);
        pEffectBase.SetCondition(false);
        pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 3];
        pEffectMain.pStartDelegate = null;
        pEffectMain.pCommonDelegate = () =>
        {
            for (int i = 0; i < 72; i++)
            {
                fTempAngle = fAngle + (5.0f * i);

                GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                    (pEffectBase.GetPosition(), Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Enemy,
                    EEnemyBulletType.enType_Capsule, EPlayerBulletType.None, 1.0f, 10.0f);
                BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                BulletBase pBulletBase = pBulletMain.GetBulletBase();

                pBulletBase.SetUniqueNumber(0);
                pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
                pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
                pBulletBase.SetBulletSpeed(3.0f);
                pBulletBase.SetBulletRotate(fTempAngle - 90.0f);
                pBulletBase.SetBulletOption();
                pBulletBase.SetCondition(false);
                pBulletBase.SetCollisionDestroy(true);
                pBulletBase.SetColliderTrigger(true);
                pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_Capsule) + 7];
                pBulletMain.pCommonDelegate = null;
                pBulletMain.pConditionDelegate = null;
                pBulletMain.pChangeDelegate = null;
                pBulletMain.pSplitDelegate = null;
                pBulletMain.pAttachDelegate = null;
            }
        };
        pEffectMain.pConditionDelegate = null;
        pEnemyMain.GetSinglePatternList().Remove(114);
        yield break;
    }
    #endregion

    #region PATTERN 115
    public IEnumerator<float> Pattern115(GameObject pEnemyObject, EnemyMain pEnemyMain, bool bCounter = false)
    {
        Color pColor = new Color(1, 1, 1, 1.0f);
        Vector3 vPosition = pEnemyObject.GetComponent<Transform>().position;
        Vector3 vScale = new Vector3(2.0f, 2.0f, 1.0f);
        float fAngle = Mathf.Atan2(Utility.Instance.GetAimedDestination(vPosition, pPlayer).y, Utility.Instance.GetAimedDestination(vPosition, pPlayer).x) * Mathf.Rad2Deg;

        yield return Timing.WaitForOneFrame;

        GameObject pEffectObject = EffectManager.Instance.GetEffectPool().ExtractEffect
            (vPosition, vScale, pColor, EEffectType.enType_CommonEffect, EEffectAnimationType.enType_HoldingLaserShot);
        EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
        EffectBase pEffectBase = pEffectMain.GetEffectBase();

        pEffectBase.SetUniqueNumber(0);
        pEffectMain.GetTimer().InitTimer(0, 0.03f, 0);
        pEffectMain.GetLaserDelayTimer().InitTimer(0.75f);
        pEffectMain.GetLaserActiveTimer().InitTimer(2.5f, 0, 0.0f, false);
        pEffectBase.SetEffect(0.0f, 0.0f, 0.0f, 0.1f, 0.1f, 0.0f, 0.0f, 1.8f);
        pEffectBase.SetCondition(false);
        pEffectBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEffectType.enType_CommonEffect) + 4];
        pEffectMain.pStartDelegate = () =>
        {
            GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                (new Vector2(pEffectBase.GetPositionX(), pEffectBase.GetPositionY() + 2.8f), new Vector3(0.1f, 40.0f, 1.0f), EBulletType.enType_Box,
                EBulletShooter.enShooter_Enemy, EEnemyBulletType.enType_BoxLaser, EPlayerBulletType.None, 1.0f, 300.0f);
            BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
            BulletBase pBulletBase = pBulletMain.GetBulletBase();

            pBulletBase.GetTransform().parent = pEffectBase.GetTransform();
            pBulletBase.SetUniqueNumber(0);
            pBulletMain.GetPatternTimer().InitTimer(0, 0.0f, false);
            pBulletMain.GetRotateTimer().InitTimer(0, 0.0f, false);
            pBulletBase.SetBulletSpeed(0.0f);
            pBulletBase.SetBulletRotate(180.0f);
            pBulletBase.SetBulletOption();
            pBulletBase.SetCondition(false);
            pBulletBase.SetCollisionDestroy(false);
            pBulletBase.SetColliderTrigger(false);
            pBulletBase.GetSpriteRenderer().sprite = pBulletEffectSprite[Convert.ToInt32(EEnemyBulletType.enType_BoxLaser) + 7];
            pBulletMain.pCommonDelegate = null;
            pBulletMain.pConditionDelegate = null;
            pBulletMain.pChangeDelegate = null;
            pBulletMain.pSplitDelegate = null;
            pBulletMain.pAttachDelegate = null;
            pEffectBase.SetRotationZ(fAngle - 90.0f);
        };
        pEffectMain.pCommonDelegate = null;
        pEffectMain.pConditionDelegate = null;
        pEnemyMain.GetSinglePatternList().Remove(115);
        yield break;
    }
    #endregion
    #endregion

    #region COMMON METHOD
    public CoroutineHandle PatternCall(GameObject pEnemyObject, EnemyMain pEnemyMain, int iPatternIndex, bool bCounter = false, int iFireCount = 0)
    {
        switch (iPatternIndex)
        {
            case 1:
                return Timing.RunCoroutine(Pattern1(pEnemyObject, pEnemyMain, bCounter));
            case 100:
                return Timing.RunCoroutine(Pattern100(pEnemyObject, pEnemyMain, bCounter));
            case 101:
                return Timing.RunCoroutine(Pattern101(pEnemyObject, pEnemyMain, bCounter));
             case 102:
                return Timing.RunCoroutine(Pattern102(pEnemyObject, pEnemyMain, bCounter, iFireCount));
            case 103:
                return Timing.RunCoroutine(Pattern103(pEnemyObject, pEnemyMain, bCounter));
            case 104:
                return Timing.RunCoroutine(Pattern104(pEnemyObject, pEnemyMain, bCounter));
            case 105:
                return Timing.RunCoroutine(Pattern105(pEnemyObject, pEnemyMain, bCounter));
            case 106:
                return Timing.RunCoroutine(Pattern106(pEnemyObject, pEnemyMain, bCounter));
            case 107:
                return Timing.RunCoroutine(Pattern107(pEnemyObject, pEnemyMain, bCounter));
            case 108:
                return Timing.RunCoroutine(Pattern108(pEnemyObject, pEnemyMain, bCounter));
            case 109:
                return Timing.RunCoroutine(Pattern109(pEnemyObject, pEnemyMain, bCounter));
            case 110:
                return Timing.RunCoroutine(Pattern110(pEnemyObject, pEnemyMain, bCounter));
            case 111:
                return Timing.RunCoroutine(Pattern111(pEnemyObject, pEnemyMain, bCounter));
            case 112:
                return Timing.RunCoroutine(Pattern112(pEnemyObject, pEnemyMain, bCounter, iFireCount));
            case 113:
                return Timing.RunCoroutine(Pattern113(pEnemyObject, pEnemyMain, bCounter));
            case 114:
                return Timing.RunCoroutine(Pattern114(pEnemyObject, pEnemyMain, bCounter));
            case 115:
                return Timing.RunCoroutine(Pattern115(pEnemyObject, pEnemyMain, bCounter));
            default:
                return pTempHandle;
        }
        return pTempHandle;
    }
    #endregion
}