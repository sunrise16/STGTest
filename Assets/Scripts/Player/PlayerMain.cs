#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class PlayerMain : MonoBehaviour
{
    #region VARIABLE
    private SpriteRenderer pHitPoint;
    private PlayerBase pPlayerBase;
    private Timer pShotTimer;
    private Vector2 vMoveSpeedVector;
    private Vector2 vMargin;
    private float fAlpha;
    #endregion

    #region UNITY LIFE CYCLE
    public void FixedUpdate()
    {
        PlayerMove();
        ShotBullet();
        ControlHitPoint();
        MoveInScreen();
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
            pHitPoint = pPlayerBase.GetTransform().GetChild(1).GetComponent<SpriteRenderer>();
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
        fAlpha = 0.0f;
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
                    (new Vector2(pPlayerBase.GetPositionX() + (i.Equals(0) ? -0.08f : 0.08f), pPlayerBase.GetPositionY()), Vector3.one,
                    EBulletType.enType_Box, EBulletShooter.enShooter_Player, EEnemyBulletType.None, EPlayerBulletType.enType_ReimuPrimary, 1.0f, 10.0f);
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
            // if (pPlayerBase.)
        }
        else if (enPlayerType.Equals(EPlayerType.enType_Marisa))
        {

        }
    }
    public void ControlHitPoint()
    {
        if (pPlayerBase.GetSlowMode().Equals(true))
        {
            fAlpha += 0.1f;
            if (fAlpha > 1.0f)
            {
                fAlpha = 1.0f;
            }
        }
        else
        {
            fAlpha -= 0.1f;
            if (fAlpha < 0.0f)
            {
                fAlpha = 0.0f;
            }
        }
        pHitPoint.color = new Color(pPlayerBase.GetColor().r, pPlayerBase.GetColor().g, pPlayerBase.GetColor().b, fAlpha);
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
    // public IEnumerator<float> Update()
    // {
    //     while (true)
    //     {
    //         yield return Timing.WaitForOneFrame;
    // 
    //         PlayerMove();
    //         // ShotBullet();
    //         ControlHitPoint();
    //         MoveInScreen();
    //     }
    // }
    #endregion
}