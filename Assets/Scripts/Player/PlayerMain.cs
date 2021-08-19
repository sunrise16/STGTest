#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class PlayerSecondary
{
    #region VARIABLE
    private PlayerBase pPlayerBase;
    private Vector3 vRefVector;
    private int iNumber;
    #endregion

    #region CONSTRUCTOR
    public PlayerSecondary(PlayerBase pPlayerBase, int iNumber)
    {
        this.pPlayerBase = pPlayerBase;
        vRefVector = Vector3.one;
        this.iNumber = iNumber;
    }
    #endregion

    #region COMMON METHOD
    public void MoveSecondary(bool bKeyUp)
    {
        if (pPlayerBase.GetChildTransform(2).GetChild(iNumber).gameObject.activeSelf.Equals(false))
            return;

        if ((int)pPlayerBase.GetPlayerPower() == 1 || (int)pPlayerBase.GetPlayerPower() == 3)
        {
            if (((int)pPlayerBase.GetPlayerPower() == 1 && iNumber.Equals(0)) || ((int)pPlayerBase.GetPlayerPower() == 3 && iNumber.Equals(2)))
            {
                pPlayerBase.GetChildTransform(2).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(2).GetChild(iNumber).position,
                    pPlayerBase.GetChildTransform(2).GetChild(bKeyUp.Equals(true) ? 15 : 14).position, ref vRefVector, 0.05f);
            }
            else
            {
                pPlayerBase.GetChildTransform(2).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(2).GetChild(iNumber).position,
                    pPlayerBase.GetChildTransform(2).GetChild(bKeyUp.Equals(true) ? 10 + iNumber : 6 + iNumber).position, ref vRefVector, 0.05f);
            }
        }
        else
        {
            pPlayerBase.GetChildTransform(2).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(2).GetChild(iNumber).position,
                pPlayerBase.GetChildTransform(2).GetChild(bKeyUp.Equals(true) ? 10 + iNumber : 6 + iNumber).position, ref vRefVector, 0.05f);
        }
    }
    #endregion
}

public class PlayerMain : MonoBehaviour
{
    #region VARIABLE
    private SpriteRenderer pPlayerSprite;
    private SpriteRenderer pHitPointSprite;
    private PlayerBase pPlayerBase;
    private Timer pShotTimer;
    private List<PlayerSecondary> pSecondaryList;
    private Vector2 vMoveSpeedVector;
    private Vector2 vMargin;
    private float fPlayerAlpha;
    private float fHitPointAlpha;
    #endregion

    #region UNITY LIFE CYCLE
    public void FixedUpdate()
    {
        if (pPlayerBase.GetDeath().Equals(false))
        {
            PlayerMove();
            ShotBullet();
            MoveInScreen();
        }
        ControlHitPoint();

        // PLAYER SPRITE
        pPlayerSprite.color = new Color(1.0f, 1.0f, 1.0f, fPlayerAlpha);

        // PLAYER SECONDARY
        for (int i = 0; i < 4; i++)
        {
            pSecondaryList[i].MoveSecondary(pPlayerBase.GetSlowMode().Equals(true) ? true : false);
        }
    }
    #endregion

    #region GET METHOD
    public PlayerBase GetPlayerBase() { return pPlayerBase; }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        GameObject pPlayerObject = this.gameObject;
        Transform pTransform = pPlayerObject.GetComponent<Transform>();
        Vector3 vSpawnPosition = new Vector3(0.0f, -3.0f);
        Vector3 vScale = Vector3.one;

        if (pPlayerBase == null)
        {
            pPlayerBase = new PlayerBase(pPlayerObject, pTransform, vSpawnPosition, vScale);
            pShotTimer = new Timer(0, 0.03f, 0);
            pPlayerBase.SetCamera(Camera.main);
            pPlayerBase.SetAnimator(pTransform.GetChild(0).GetComponent<Animator>());
            pPlayerBase.SetSpriteRenderer(pTransform.GetChild(1).GetComponent<SpriteRenderer>());
            pPlayerBase.SetRigidbody2D(GetComponent<Rigidbody2D>());
            pPlayerBase.SetAction((string szTrigger1, string szTrigger2, string szTrigger3) =>
            {
                pPlayerBase.GetAnimator().SetTrigger(szTrigger1);
                pPlayerBase.GetAnimator().ResetTrigger(szTrigger2);
                pPlayerBase.GetAnimator().ResetTrigger(szTrigger3);
            });
            pPlayerSprite = pPlayerBase.GetChildTransform(0).GetComponent<SpriteRenderer>();
            pHitPointSprite = pPlayerBase.GetChildTransform(1).GetComponent<SpriteRenderer>();

            pSecondaryList = new List<PlayerSecondary>();
            for (int i = 0; i < 4; i++)
            {
                pSecondaryList.Add(new PlayerSecondary(pPlayerBase, i));
            }
        }
        else
        {
            pPlayerBase.Init(pPlayerObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Player);
            pShotTimer.InitTimer(0, 0.03f, 0);
        }
        pPlayerBase.SetColor(new Color(1, 1, 1, 0));
        pPlayerBase.SetSlowMode(false);
        pPlayerBase.SetDeath(false);
        pPlayerBase.SetRevive(false);
        vMoveSpeedVector = Vector2.zero;
        vMargin = new Vector2(0.03f, 0.03f);
        fPlayerAlpha = 1.0f;
        fHitPointAlpha = 0.0f;
    }
    public void PlayerMove()
    {
        // SLOW MODE CHECK
        pPlayerBase.SetSlowMode(Input.GetKey(KeyCode.LeftShift) ? true : false);

        // PLAYER MOVING
        pPlayerBase.GetRigidbody2D().velocity = vMoveSpeedVector;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                vMoveSpeedVector.x = -pPlayerBase.GetPlayerMoveSpeed() + 0.3f;
            }
            else
            {
                vMoveSpeedVector.x = -pPlayerBase.GetPlayerMoveSpeed();
            }
            pPlayerBase.GetAction()("isLeftMove", "isIdle", "isRightMove");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                vMoveSpeedVector.x = pPlayerBase.GetPlayerMoveSpeed() - 0.3f;
            }
            else
            {
                vMoveSpeedVector.x = pPlayerBase.GetPlayerMoveSpeed();
            }
            pPlayerBase.GetAction()("isRightMove", "isIdle", "isLeftMove");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                vMoveSpeedVector.y = pPlayerBase.GetPlayerMoveSpeed() - 0.3f;
            }
            else
            {
                vMoveSpeedVector.y = pPlayerBase.GetPlayerMoveSpeed();
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                vMoveSpeedVector.y = -pPlayerBase.GetPlayerMoveSpeed() + 0.3f;
            }
            else
            {
                vMoveSpeedVector.y = -pPlayerBase.GetPlayerMoveSpeed();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            vMoveSpeedVector.x = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            vMoveSpeedVector.y = 0.0f;
        }

        // PLAYER SPRITE CHECK
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                pPlayerBase.GetAction()("isRightMove", "isIdle", "isLeftMove");
            }
            else
            {
                pPlayerBase.GetAction()("isIdle", "isLeftMove", "isRightMove");
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                pPlayerBase.GetAction()("isLeftMove", "isIdle", "isRightMove");
            }
            else
            {
                pPlayerBase.GetAction()("isIdle", "isLeftMove", "isRightMove");
            }
        }
    }
    public void ShotBullet()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            pShotTimer.RunTimer();
            if (pShotTimer.GetTrigger().Equals(true))
            {
                pShotTimer.SetRepeatCount(pShotTimer.GetRepeatCount() + 1);
                ExtractBullet(pPlayerBase.GetPlayerType(), pPlayerBase.GetPlayerWeaponType());
                pShotTimer.ResetTimer(pShotTimer.GetResetTime());
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            pShotTimer.SetRepeatCount(0);
            pShotTimer.ResetTimer(pShotTimer.GetResetTime());
        }
    }
    public void ExtractBullet(EPlayerType enPlayerType, EPlayerWeaponType enPlayerWeaponType)
    {
        Vector3 vPosition = pPlayerBase.GetPosition();
        Vector3 vSubPosition = Vector3.one;

        if (enPlayerType.Equals(EPlayerType.enType_Reimu))
        {
            // PRIMARY SHOT
            for (int i = 0; i < 2; i++)
            {
                GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                    (pPlayerBase.GetChildTransform(2).GetChild(i + 4).position, Vector3.one, EBulletType.enType_Box, EBulletShooter.enShooter_Player,
                    EEnemyBulletType.None, EPlayerBulletType.enType_ReimuPrimary, 0.6f, 10.0f);
                BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                BulletBase pBulletBase = pBulletMain.GetBulletBase();

                pBulletBase.SetChildRotationZ(0, 90.0f);
                pBulletBase.SetUniqueNumber(0);
                pBulletMain.GetPatternTimer().InitTimer();
                pBulletMain.GetRotateTimer().InitTimer();
                pBulletBase.SetBulletSpeed(18.0f);
                pBulletBase.SetBulletRotate(0.0f);
                pBulletBase.SetBulletOption();
                pBulletBase.SetCondition(false);
                pBulletBase.SetCollisionDestroy(true);
                pBulletBase.SetColliderTrigger(true);
                pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSprite[Convert.ToInt32(EPlayerBulletType.enType_ReimuPrimary)];
                pBulletMain.pCommonDelegate = null;
                pBulletMain.pConditionDelegate = null;
                pBulletMain.pChangeDelegate = null;
                pBulletMain.pSplitDelegate = null;
                pBulletMain.pAttachDelegate = null;
            }

            // SECONDARY SHOT
            if (pPlayerBase.GetPlayerPower() >= 1.0f)
            {
                for (int i = 0; i < (int)pPlayerBase.GetPlayerPower(); i++)
                {
                    if (pPlayerBase.GetSlowMode().Equals(true))
                    {
                        GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pPlayerBase.GetChildTransform(2).GetChild(i).position, Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Player,
                            EEnemyBulletType.None, EPlayerBulletType.enType_ReimuSecondary_Niddle, 0.6f, 10.0f);
                        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                        BulletBase pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetChildRotationZ(0, 90.0f);
                        pBulletBase.SetUniqueNumber(0);
                        pBulletMain.GetPatternTimer().InitTimer();
                        pBulletMain.GetRotateTimer().InitTimer();
                        pBulletBase.SetBulletSpeed(18.0f);
                        pBulletBase.SetBulletRotate(0.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSprite[Convert.ToInt32(EPlayerBulletType.enType_ReimuSecondary_Niddle)];
                        pBulletMain.pCommonDelegate = null;
                        pBulletMain.pConditionDelegate = null;
                        pBulletMain.pChangeDelegate = null;
                        pBulletMain.pSplitDelegate = null;
                        pBulletMain.pAttachDelegate = null;
                    }
                    else
                    {
                        GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pPlayerBase.GetChildTransform(2).GetChild(i).position, Vector3.one, EBulletType.enType_Box, EBulletShooter.enShooter_Player,
                            EEnemyBulletType.None, EPlayerBulletType.enType_ReimuSecondary_Homing, 0.6f, 20.0f);
                        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                        BulletBase pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetChildRotationZ(0, 90.0f);
                        pBulletBase.SetUniqueNumber(0);
                        pBulletMain.GetPatternTimer().InitTimer();
                        pBulletMain.GetRotateTimer().InitTimer();
                        pBulletBase.SetBulletSpeed(12.0f);
                        pBulletBase.SetBulletRotate(0.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCondition(false);
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSprite[Convert.ToInt32(EPlayerBulletType.enType_ReimuSecondary_Homing)];
                        pBulletMain.pCommonDelegate = null;
                        pBulletMain.pConditionDelegate = null;
                        pBulletMain.pChangeDelegate = null;
                        pBulletMain.pSplitDelegate = null;
                        pBulletMain.pAttachDelegate = null;
                    }
                }
            }
        }
        else if (enPlayerType.Equals(EPlayerType.enType_Marisa))
        {
            // UNDER CONSTRUCTION
        }
    }
    public void ControlHitPoint()
    {
        if (pPlayerBase.GetSlowMode().Equals(true))
        {
            fHitPointAlpha += 0.1f;
            if (fHitPointAlpha > 1.0f)
            {
                fHitPointAlpha = 1.0f;
            }
        }
        else
        {
            fHitPointAlpha -= 0.1f;
            if (fHitPointAlpha < 0.0f)
            {
                fHitPointAlpha = 0.0f;
            }
        }

        if (pPlayerBase.GetDeath().Equals(true))
        {
            fHitPointAlpha = 0.0f;
        }
        pHitPointSprite.color = new Color(1.0f, 1.0f, 1.0f, fHitPointAlpha);
    }
    public void MoveInScreen()
    {
        Vector3 vPosition = pPlayerBase.GetCamera().WorldToViewportPoint(pPlayerBase.GetPosition());
        vPosition.x = Mathf.Clamp(vPosition.x, 0.0f + vMargin.x, 1.0f - vMargin.x);
        vPosition.y = Mathf.Clamp(vPosition.y, 0.0f + vMargin.y, 1.0f - vMargin.y);

        pPlayerBase.SetPosition(pPlayerBase.GetCamera().ViewportToWorldPoint(vPosition));
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> PlayerDeath()
    {
        pPlayerBase.SetDeath(true);
        pPlayerBase.SetSlowMode(false);
        pPlayerBase.SetPlayerMissCount(pPlayerBase.GetPlayerMissCount() + 1);

        fPlayerAlpha = 0.0f;
        pPlayerBase.GetRigidbody2D().velocity = Vector2.zero;
        vMoveSpeedVector = Vector2.zero;
        pPlayerBase.GetAction()("isIdle", "isLeftMove", "isRightMove");
        pPlayerBase.GetChildGameObject(2).SetActive(false);

        // CREATE EFFECT HERE

        yield return Timing.WaitForSeconds(1.0f);

        fPlayerAlpha = 0.8f;
        pPlayerBase.SetPosition(new Vector3(0.0f, -4.5f, 0.0f));
        pPlayerBase.GetChildGameObject(2).SetActive(true);
        iTween.MoveTo(pPlayerBase.GetGameObject(), iTween.Hash("position", new Vector3(0.0f, -3.25f, 0.0f), "easetype", iTween.EaseType.linear, "time", 1.0f));

        yield return Timing.WaitForSeconds(1.0f);

        pPlayerBase.SetDeath(false);
        pPlayerBase.SetRevive(true);
        Timing.RunCoroutine(PlayerRevive());

        yield break;
    }
    public IEnumerator<float> PlayerRevive()
    {
        int iCount = 0;

        while (iCount <= 96)
        {
            fPlayerAlpha = (iCount % 2).Equals(0) ? 0.8f : 0.3f;
            iCount++;

            yield return Timing.WaitForSeconds(0.03f);
        }
        fPlayerAlpha = 1.0f;
        pPlayerBase.SetRevive(false);

        yield break;
    }
    #endregion
}