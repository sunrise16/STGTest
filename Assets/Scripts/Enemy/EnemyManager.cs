﻿#region USING
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class EnemyPool : IPoolBase
{
    #region VARIABLE
    private List<GameObject> pEnemyList;
    private Transform pEnemyParent;
    private Transform pActiveEnemyParent;
    #endregion

    #region CONSTRUCTOR
    public EnemyPool(Transform pEnemyParent, Transform pActiveEnemyParent)
    {
        pEnemyList = new List<GameObject>();
        this.pEnemyParent = pEnemyParent;
        this.pActiveEnemyParent = pActiveEnemyParent;
    }
    #endregion

    #region COMMON METHOD
    public GameObject AddPool()
    {
        GameObject pEnemyObject = GameObject.Instantiate(Resources.Load(GlobalData.szEnemyPrefabPath)) as GameObject;
        Transform pTransform = pEnemyObject.GetComponent<Transform>();

        pEnemyList.Add(pEnemyObject);
        pTransform.position = Vector2.zero;
        pTransform.rotation = Quaternion.identity;
        pTransform.localScale = Vector3.one;
        pTransform.parent = pEnemyParent;
        pEnemyObject.SetActive(false);
        return pEnemyObject;
    }
    public void ReturnPool(GameObject pEnemyObject)
    {
        Transform pTransform = pEnemyObject.GetComponent<Transform>();
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        EnemyBase pEnemyBase = pEnemyMain.GetEnemyBase();

        pEnemyList.Add(pEnemyObject);
        pTransform.parent = pEnemyParent;
        pEnemyBase.GetSpriteRenderer().color = Color.white;
        pEnemyBase.GetSpriteRenderer().sprite = null;
        pEnemyBase.GetAnimator().runtimeAnimatorController = null;
        pEnemyMain.pEnemyMoveDelegateList.Clear();
        pEnemyBase.GetItemDictionary().Clear();
        pEnemyBase.AllReset();
        if (pTransform.childCount > 1)
        {
            for (int i = 1; i < pTransform.childCount; i++)
            {
                if (pTransform.GetChild(i).GetComponent<BulletMain>())
                {
                    BulletManager.Instance.GetBulletPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                else if (pTransform.GetChild(i).GetComponent<EffectMain>())
                {
                    EffectManager.Instance.GetEffectPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                else if (pTransform.GetChild(i).GetComponent<EnemyMain>())
                {
                    EnemyManager.Instance.GetEnemyPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                else if (pTransform.GetChild(i).GetComponent<ItemMain>())
                {
                    ItemManager.Instance.GetItemPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                i--;
            }
        }
    }
    public GameObject ExtractEnemy(Vector3 vSpawnPosition, Vector3 vScale, EEnemyType enEnemyType, float fEnemyHP, bool bCounter, bool bOutScreenShot)
    {
        GameObject pEnemyObject = pEnemyList.Count.Equals(0) ? AddPool() : pEnemyList[0];
        Transform pTransform = pEnemyObject.GetComponent<Transform>();
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();

        pEnemyMain.Init(pEnemyObject, pTransform, vSpawnPosition, vScale, enEnemyType, fEnemyHP, bCounter, bOutScreenShot);
        pTransform.parent = pActiveEnemyParent;
        pEnemyList.RemoveAt(0);
        return pEnemyObject;
    }
    public void AddSinglePattern(GameObject pEnemyObject, int iFlag, int iShotTimerRepeatLimit, float iShotTimerLimitTime, float iDelayTimerLimitTime)
    {
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        pEnemyMain.GetPatternTimerList().Add(new Timer(iShotTimerRepeatLimit, iShotTimerLimitTime, iFlag, 0.0f, 0.0f, iDelayTimerLimitTime));
    }
    public void AddRepeatPattern(GameObject pEnemyObject, int iPatternIndex, float fShotDelay)
    {
        Timing.RunCoroutine(Delay(pEnemyObject, iPatternIndex, fShotDelay));
    }
    public void AddCounterPattern(GameObject pEnemyObject, int iFlag)
    {
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        pEnemyMain.GetCounterPatternList().Add(iFlag);
    }
    public void AddItemDictionary(GameObject pEnemyObject, EItemType enItemType, int iAmount)
    {
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        pEnemyMain.GetEnemyBase().GetItemDictionary().Add(enItemType, iAmount);
    }
    public void SetEnemyMoving(GameObject pEnemyObject, float fStartDelay, params KeyValuePair<DelegateGameObject, float>[] pDelegate)
    {
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        foreach (KeyValuePair<DelegateGameObject, float> pDel in pDelegate)
        {
            pEnemyMain.pEnemyMoveDelegateList.Add(pDel.Key, pDel.Value);
        }
        Timing.RunCoroutine(pEnemyMain.EnemyMove(fStartDelay));
    }
    public void SetEnemyMoveX(GameObject pEnemyObject, float fEnemyMoveSpeedX, float fEnemyMoveAccelerationSpeedX = 0.0f, float fEnemyMoveAccelerationSpeedXMax = 0.0f, float fEnemyMoveDecelerationSpeedX = 0.0f, float fEnemyMoveDecelerationSpeedXMin = 0.0f)
    {
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        EnemyBase pEnemyBase = pEnemyMain.GetEnemyBase();
        pEnemyBase.SetEnemySpeedX(fEnemyMoveSpeedX, fEnemyMoveAccelerationSpeedX, fEnemyMoveAccelerationSpeedXMax, fEnemyMoveDecelerationSpeedX, fEnemyMoveDecelerationSpeedXMin);
    }
    public void SetEnemyMoveY(GameObject pEnemyObject, float fEnemyMoveSpeedY, float fEnemyMoveAccelerationSpeedY = 0.0f, float fEnemyMoveAccelerationSpeedYMax = 0.0f, float fEnemyMoveDecelerationSpeedY = 0.0f, float fEnemyMoveDecelerationSpeedYMin = 0.0f)
    {
        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        EnemyBase pEnemyBase = pEnemyMain.GetEnemyBase();
        pEnemyBase.SetEnemySpeedY(fEnemyMoveSpeedY, fEnemyMoveAccelerationSpeedY, fEnemyMoveAccelerationSpeedYMax, fEnemyMoveDecelerationSpeedY, fEnemyMoveDecelerationSpeedYMin);
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> Delay(GameObject pEnemyObject, int iPatternIndex, float fShotDelay)
    {
        yield return Timing.WaitForSeconds(fShotDelay);

        EnemyMain pEnemyMain = pEnemyObject.GetComponent<EnemyMain>();
        pEnemyMain.GetRepeatPatternList().Add(GameManager.Instance.PatternCall(pEnemyObject, pEnemyMain, iPatternIndex));

        yield break;
    }
    #endregion
}

public class EnemyManager : Singleton<EnemyManager>
{
    #region VARIABLE
    private EnemyPool pEnemyPool;
    #endregion

    #region GET METHOD
    public EnemyPool GetEnemyPool() { return pEnemyPool; }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        Transform pEnemyParent = GameObject.Find("EnemyPool").GetComponent<Transform>();
        Transform pActiveEnemyParent = GameObject.Find("ActiveEnemys").GetComponent<Transform>();

        pEnemyPool = new EnemyPool(pEnemyParent, pActiveEnemyParent);
    }
    #endregion
}
