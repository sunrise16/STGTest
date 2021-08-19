#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class BulletMain : MonoBehaviour
{
    #region VARIABLE
    public DelegateCommon pCommonDelegate;
    public DelegateCommon pConditionDelegate;
    public DelegateCommon pChangeDelegate;
    public DelegateCommon pSplitDelegate;
    public DelegateCommon pAttachDelegate;

    private BulletBase pBulletBase;
    private PlayerMain pPlayerMain;
    private PlayerBase pPlayerBase;
    private GameObject pPlayerObject;
    private Transform pPlayerTransform;
    private Transform pTempTransform;
    private Timer pPatternTimer;
    private Timer pRotateTimer;
    private float fDistance;
    #endregion

    #region GET METHOD
    public BulletBase GetBulletBase() { return pBulletBase; }
    public PlayerMain GetPlayerMain() { return pPlayerMain; }
    public PlayerBase GetPlayerBase() { return pPlayerBase; }
    public GameObject GetPlayerObject() { return pPlayerObject; }
    public Transform GetPlayerTransform() { return pPlayerTransform; }
    public Transform GetTempTransform() { return pTempTransform; }
    public Timer GetPatternTimer() { return pPatternTimer; }
    public Timer GetRotateTimer() { return pRotateTimer; }
    public float GetDistance() { return fDistance; }
    #endregion

    #region UNITY LIFE CYCLE
    public void FixedUpdate()
    {
        if (pBulletBase == null)
        {
            return;
        }

        // SCREEN CHECK
        OutScreenCheck(pBulletBase.GetTransform(), pBulletBase.GetPadding());
        TouchScreenCheck(pBulletBase.GetTransform());

        // COLLIDER CHECK
        pTempTransform = pPlayerTransform;
        fDistance = Vector3.Distance(pBulletBase.GetTransform().position, pTempTransform.position);
        ColliderCheck(fDistance);

        // BULLET MOVE
        if (pBulletBase.GetBulletMoveAccelerationSpeed() != 0.0f)
            BulletMoveAcceleration();
        if (pBulletBase.GetBulletMoveDecelerationSpeed() != 0.0f)
            BulletMoveDeceleration();
        if (pBulletBase.GetBulletMoveSpeed() != 0.0f)
            BulletMove();

        // BULLET ROTATE
        if (pRotateTimer.GetSwitch().Equals(true))
        {
            pRotateTimer.RunTimer();
            if (pBulletBase.GetBulletRotateAccelerationSpeed() != 0.0f)
                BulletRotateAcceleration();
            if (pBulletBase.GetBulletRotateDecelerationSpeed() != 0.0f)
                BulletRotateDeceleration();
            if (pBulletBase.GetBulletRotateSpeed() != 0.0f)
                BulletRotate();
        }

        // PATTERN TIMER CHECK
        if (pPatternTimer.GetSwitch().Equals(true))
        {
            pPatternTimer.RunTimer();
            if (pPatternTimer.GetTrigger().Equals(true))
            {
                if (pCommonDelegate != null)
                {
                    pCommonDelegate();
                }
                pPatternTimer.ResetTimer(pPatternTimer.GetResetTime());
            }
        }

        // CONDITION PATTERN
        if (pConditionDelegate != null)
        {
            pConditionDelegate();
        }
    }
    private void OnTriggerEnter2D(Collider2D pCollider)
    {
        EnemyMain pEnemyMain = pCollider.GetComponent<EnemyMain>();

        if (pBulletBase.GetBulletShooter().Equals(EBulletShooter.enShooter_Player))
        {
            if (pCollider.CompareTag("ENEMY"))
            {
                switch (pBulletBase.GetPlayerBulletType())
                {
                    case EPlayerBulletType.enType_ReimuPrimary:
                    case EPlayerBulletType.enType_MarisaPrimary:
                        pEnemyMain.GetEnemyBase().SetEnemyHP
                            (pEnemyMain.GetEnemyBase().GetEnemyHP() - (pPlayerBase.GetPlayerPrimaryDamage() + pPlayerBase.GetPlayerPower()));
                        break;
                    case EPlayerBulletType.enType_ReimuSecondary_Homing:
                    case EPlayerBulletType.enType_MarisaSecondary_Missile:
                        pEnemyMain.GetEnemyBase().SetEnemyHP
                            (pEnemyMain.GetEnemyBase().GetEnemyHP() - (pPlayerBase.GetPlayerFastSecondaryDamage() + (pPlayerBase.GetPlayerPower() * 0.5f)));
                        break;
                    case EPlayerBulletType.enType_ReimuSecondary_Niddle:
                    case EPlayerBulletType.enType_MarisaSecondary_Laser:
                        pEnemyMain.GetEnemyBase().SetEnemyHP
                            (pEnemyMain.GetEnemyBase().GetEnemyHP() - (pPlayerBase.GetPlayerSlowSecondaryDamage() + (pPlayerBase.GetPlayerPower() * 0.5f)));
                        break;
                    default:
                        break;
                }
                if (pEnemyMain.GetEnemyBase().GetEnemyHP() <= 0.0f)
                {
                    pEnemyMain.DestroyEnemy();
                }

                if (pBulletBase.GetCollisionDestroy().Equals(true))
                {
                    DestroyBullet();
                }
            }
            else return;
        }
        else
        {
            if (pCollider.name.Equals("HitPoint"))
            {
                if (pBulletBase.GetCollisionDestroy().Equals(true))
                {
                    DestroyBullet();
                }
                if (pPlayerBase.GetDeath().Equals(false) && pPlayerBase.GetRevive().Equals(false))
                {
                    Timing.RunCoroutine(pPlayerMain.PlayerDeath());
                }
            }
            else if (pCollider.name.Equals("GrazePoint"))
            {
                if (pBulletBase.GetGraze().Equals(false))
                {
                    pBulletBase.SetGraze(true);
                    pPlayerBase.SetPlayerGrazeCount(pPlayerBase.GetPlayerGrazeCount() + 1);
                }
            }
            else return;
        }
    }
    #endregion

    #region COMMON METHOD
    public void Init(GameObject pBulletObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale, EBulletType enBulletType, EPlayerBulletType enPlayerBulletType, float fAlpha, float fPadding)
    {
        if (pBulletBase == null)
        {
            pBulletBase = new BulletBase(pBulletObject, pTransform, vSpawnPosition, vScale);
            pPlayerObject = GameManager.Instance.pPlayer;
            pPlayerMain = pPlayerObject.GetComponent<PlayerMain>();
            pPlayerBase = pPlayerMain.GetPlayerBase();
            pPlayerTransform = pPlayerObject.GetComponent<Transform>();
            pPatternTimer = new Timer();
            pRotateTimer = new Timer();
            pBulletBase.SetSpriteRenderer(pBulletBase.GetTransform().GetChild(0).GetComponent<SpriteRenderer>());
            pBulletBase.SetAnimator(pBulletBase.GetTransform().GetChild(0).GetComponent<Animator>());
            pBulletBase.SetRigidbody(pBulletBase.GetGameObject().GetComponent<Rigidbody2D>());
            pBulletBase.SetBoxCollider(pBulletBase.GetGameObject().GetComponent<BoxCollider2D>());
            pBulletBase.SetCapsuleCollider(pBulletBase.GetGameObject().GetComponent<CapsuleCollider2D>());
            pBulletBase.SetCircleCollider(pBulletBase.GetGameObject().GetComponent<CircleCollider2D>());
        }
        else
        {
            pBulletBase.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Bullet);
            pPatternTimer.InitTimer();
            pRotateTimer.InitTimer();
        }
        pBulletBase.GetGameObject().layer = LayerMask.NameToLayer("PLAYERBULLET");
        pBulletBase.SetBulletRotateAngle(0.0f);
        pBulletBase.SetBulletType(enBulletType);
        pBulletBase.SetBulletShooter(EBulletShooter.enShooter_Player);
        pBulletBase.SetPlayerBulletType(enPlayerBulletType);
        pBulletBase.SetColor(new Color(1, 1, 1, fAlpha));
        pBulletBase.GetSpriteRenderer().color = pBulletBase.GetColor();
        pBulletBase.GetSpriteRenderer().sortingOrder = 5;
        pBulletBase.SetPadding(fPadding);
        fDistance = 0.0f;
    }
    public void Init(GameObject pBulletObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale, EBulletType enBulletType, EEnemyBulletType enEnemyBulletType, float fAlpha, float fPadding)
    {
        if (pBulletBase == null)
        {
            pBulletBase = new BulletBase(pBulletObject, pTransform, vSpawnPosition, vScale);
            pPlayerObject = GameManager.Instance.pPlayer;
            pPlayerMain = pPlayerObject.GetComponent<PlayerMain>();
            pPlayerBase = pPlayerMain.GetPlayerBase();
            pPlayerTransform = pPlayerObject.GetComponent<Transform>();
            pPatternTimer = new Timer();
            pRotateTimer = new Timer();
            pBulletBase.SetSpriteRenderer(pBulletBase.GetTransform().GetChild(0).GetComponent<SpriteRenderer>());
            pBulletBase.SetAnimator(pBulletBase.GetTransform().GetChild(0).GetComponent<Animator>());
            pBulletBase.SetRigidbody(pBulletBase.GetGameObject().GetComponent<Rigidbody2D>());
            pBulletBase.SetBoxCollider(pBulletBase.GetGameObject().GetComponent<BoxCollider2D>());
            pBulletBase.SetCapsuleCollider(pBulletBase.GetGameObject().GetComponent<CapsuleCollider2D>());
            pBulletBase.SetCircleCollider(pBulletBase.GetGameObject().GetComponent<CircleCollider2D>());
        }
        else
        {
            pBulletBase.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Bullet);
            pPatternTimer.InitTimer();
            pRotateTimer.InitTimer();
        }
        pBulletBase.GetGameObject().layer = LayerMask.NameToLayer("ENEMYBULLET");
        pBulletBase.SetBulletRotateAngle(0.0f);
        pBulletBase.SetBulletType(enBulletType);
        pBulletBase.SetBulletShooter(EBulletShooter.enShooter_Enemy);
        pBulletBase.SetEnemyBulletType(enEnemyBulletType);
        pBulletBase.SetColor(new Color(1, 1, 1, fAlpha));
        pBulletBase.GetSpriteRenderer().color = pBulletBase.GetColor();
        pBulletBase.GetSpriteRenderer().sortingOrder = 6;
        pBulletBase.SetPadding(fPadding);
        fDistance = 0.0f;
    }
    public void OutScreenCheck(Transform pTransform, float fPadding)
    {
        Vector3 vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);

        if (vTempPosition.x < 0 - fPadding || vTempPosition.x > Screen.width + fPadding ||
            vTempPosition.y < 0 - fPadding || vTempPosition.y > Screen.height + fPadding)
        {
            DestroyBullet();
        }
    }
    public void TouchScreenCheck(Transform pTransform)
    {
        Vector3 vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);

        if (vTempPosition.x < 0 || vTempPosition.x > Screen.width || vTempPosition.y < 0 || vTempPosition.y > Screen.height)
        {
            // NOT BOTTOM SCREEN
            if (vTempPosition.x < 0 || vTempPosition.x > Screen.width || vTempPosition.y > Screen.height)
            {
                // BULLET REFLECT
                if (pBulletBase.GetBulletReflect().Equals(true))
                {
                    pBulletBase.SetBulletReflect(false);

                    if (vTempPosition.x < 0 || vTempPosition.x > Screen.width)
                    {
                        pBulletBase.SetRotationZ(-pBulletBase.GetRotationZ());
                    }
                    else if (vTempPosition.y > Screen.height)
                    {
                        if (pBulletBase.GetRotationZ() < 0)
                        {
                            pBulletBase.SetRotationZ(-180 - pBulletBase.GetRotationZ());
                        }
                        else if (pBulletBase.GetRotationZ() > 0)
                        {
                            pBulletBase.SetRotationZ(180 - pBulletBase.GetRotationZ());
                        }
                        else
                        {
                            pBulletBase.SetRotationZ(0);
                        }
                    }

                    if (pBulletBase.GetBulletBottomReflect().Equals(true))
                    {
                        pBulletBase.SetBulletBottomReflect(false);
                    }
                }

                // BULLET CHANGE
                if (pBulletBase.GetBulletChange().Equals(true))
                {
                    pBulletBase.SetBulletChange(false);
                    if (pChangeDelegate != null)
                    {
                        pChangeDelegate();
                    }

                    if (pBulletBase.GetBulletBottomChange().Equals(true))
                    {
                        pBulletBase.SetBulletBottomChange(false);
                    }
                }

                // BULLET SPLIT
                if (pBulletBase.GetBulletSplit().Equals(true))
                {
                    pBulletBase.SetBulletSplit(false);
                    if (pSplitDelegate != null)
                    {
                        pSplitDelegate();
                    }

                    if (pBulletBase.GetBulletBottomSplit().Equals(true))
                    {
                        pBulletBase.SetBulletBottomSplit(false);
                    }
                }
            }
            // BOTTOM SCREEN
            else if (vTempPosition.y < 0)
            {
                // BULLET BOTTOM REFLECT
                if (pBulletBase.GetBulletBottomReflect().Equals(true))
                {
                    pBulletBase.SetBulletBottomReflect(false);

                    if (pBulletBase.GetRotationZ() < 0)
                    {
                        pBulletBase.SetRotationZ(-180 - pBulletBase.GetRotationZ());
                    }
                    else if (pBulletBase.GetRotationZ() > 0)
                    {
                        pBulletBase.SetRotationZ(180 - pBulletBase.GetRotationZ());
                    }
                    else
                    {
                        pBulletBase.SetRotationZ(0);
                    }

                    if (pBulletBase.GetBulletReflect().Equals(true))
                    {
                        pBulletBase.SetBulletReflect(false);
                    }
                }

                // BULLET BOTTOM CHANGE
                if (pBulletBase.GetBulletBottomChange().Equals(true))
                {
                    pBulletBase.SetBulletBottomChange(false);
                    if (pChangeDelegate != null)
                    {
                        pChangeDelegate();
                    }

                    if (pBulletBase.GetBulletChange().Equals(true))
                    {
                        pBulletBase.SetBulletChange(false);
                    }
                }

                // BULLET BOTTOM SPLIT
                if (pBulletBase.GetBulletBottomSplit().Equals(true))
                {
                    pBulletBase.SetBulletBottomSplit(false);
                    if (pSplitDelegate != null)
                    {
                        pSplitDelegate();
                    }

                    if (pBulletBase.GetBulletSplit().Equals(true))
                    {
                        pBulletBase.SetBulletSplit(false);
                    }
                }
            }

            // BULLET ATTACH
            if (pBulletBase.GetBulletAttach().Equals(true))
            {
                pBulletBase.SetBulletAttach(false);
                if (pAttachDelegate != null)
                {
                    pAttachDelegate();
                }
            }
        }
    }
    public void DestroyBullet()
    {
        pPatternTimer.InitTimer(0, 0.0f, false);
        pRotateTimer.InitTimer(0, 0.0f, false);

        BulletManager.Instance.GetBulletPool().ReturnPool(pBulletBase.GetGameObject());
    }
    public void ColliderCheck(float fDistance)
    {
        if (pBulletBase.GetBulletShooter().Equals(EBulletShooter.enShooter_Player))
        {
            switch (pBulletBase.GetBulletType())
            {
                case EBulletType.enType_Box:
                    pBulletBase.GetBoxCollider().enabled = true;
                    break;
                case EBulletType.enType_Capsule:
                    pBulletBase.GetCapsuleCollider().enabled = true;
                    break;
                case EBulletType.enType_Circle:
                    pBulletBase.GetCircleCollider().enabled = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (pBulletBase.GetColliderTrigger().Equals(true) && pPlayerBase.GetDeath().Equals(false))
            {
                switch (pBulletBase.GetBulletType())
                {
                    case EBulletType.enType_Box:
                        if (fDistance <= 0.25f)
                        {
                            pBulletBase.GetBoxCollider().enabled = true;
                        }
                        else
                        {
                            pBulletBase.GetBoxCollider().enabled = false;
                        }
                        break;
                    case EBulletType.enType_Capsule:
                        if (fDistance <= 0.25f)
                        {
                            pBulletBase.GetCapsuleCollider().enabled = true;
                        }
                        else
                        {
                            pBulletBase.GetCapsuleCollider().enabled = false;
                        }
                        break;
                    case EBulletType.enType_Circle:
                        if (fDistance <= 0.25f)
                        {
                            pBulletBase.GetCircleCollider().enabled = true;
                        }
                        else
                        {
                            pBulletBase.GetCircleCollider().enabled = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (pBulletBase.GetBulletType())
                {
                    case EBulletType.enType_Box:
                        pBulletBase.GetBoxCollider().enabled = false;
                        break;
                    case EBulletType.enType_Capsule:
                        pBulletBase.GetCapsuleCollider().enabled = false;
                        break;
                    case EBulletType.enType_Circle:
                        pBulletBase.GetCircleCollider().enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void BulletMove()
    {
        pBulletBase.GetTransform().Translate(Vector2.up * pBulletBase.GetBulletMoveSpeed() * Time.deltaTime);
    }
    public void BulletMoveAcceleration()
    {
        pBulletBase.SetBulletMoveSpeed(pBulletBase.GetBulletMoveSpeed() + pBulletBase.GetBulletMoveAccelerationSpeed());
        if (pBulletBase.GetBulletMoveSpeed() >= pBulletBase.GetBulletMoveAccelerationSpeedMax())
        {
            pBulletBase.SetBulletMoveSpeed(pBulletBase.GetBulletMoveAccelerationSpeedMax());
            pBulletBase.SetBulletMoveAccelerationSpeed(0.0f);
            pBulletBase.SetBulletMoveAccelerationSpeedMax(0.0f);
        }
    }
    public void BulletMoveDeceleration()
    {
        pBulletBase.SetBulletMoveSpeed(pBulletBase.GetBulletMoveSpeed() - pBulletBase.GetBulletMoveDecelerationSpeed());
        if (pBulletBase.GetBulletMoveSpeed() <= pBulletBase.GetBulletMoveDecelerationSpeedMin())
        {
            pBulletBase.SetBulletMoveSpeed(pBulletBase.GetBulletMoveDecelerationSpeedMin());
            pBulletBase.SetBulletMoveDecelerationSpeed(0.0f);
            pBulletBase.SetBulletMoveDecelerationSpeedMin(0.0f);
        }
    }
    public void BulletRotate()
    {
        pBulletBase.GetTransform().Rotate(0.0f, 0.0f, pBulletBase.GetBulletRotateSpeed() * Time.deltaTime);
        pBulletBase.SetBulletRotateAngle(pBulletBase.GetBulletRotateAngle() + (pBulletBase.GetBulletRotateSpeed() > 0.0f ? pBulletBase.GetBulletRotateSpeed() * Time.deltaTime : -(pBulletBase.GetBulletRotateSpeed() * Time.deltaTime)));
    }
    public void BulletRotateAcceleration()
    {
        pBulletBase.SetBulletRotateSpeed(pBulletBase.GetBulletRotateSpeed() + pBulletBase.GetBulletRotateAccelerationSpeed());
        if (pBulletBase.GetBulletRotateSpeed() >= pBulletBase.GetBulletRotateAccelerationSpeedMax())
        {
            pBulletBase.SetBulletRotateSpeed(0);
            pBulletBase.SetBulletRotateAccelerationSpeed(0.0f);
            pBulletBase.SetBulletRotateAccelerationSpeedMax(0.0f);
        }
    }
    public void BulletRotateDeceleration()
    {
        pBulletBase.SetBulletRotateSpeed(pBulletBase.GetBulletRotateSpeed() - pBulletBase.GetBulletRotateDecelerationSpeed());
        if (pBulletBase.GetBulletRotateSpeed() <= pBulletBase.GetBulletRotateDecelerationSpeedMin())
        {
            pBulletBase.SetBulletRotateSpeed(0);
            pBulletBase.SetBulletRotateDecelerationSpeed(0.0f);
            pBulletBase.SetBulletRotateDecelerationSpeedMin(0.0f);
        }
    }
    #endregion

    #region IENUMERATOR
    
    #endregion
}