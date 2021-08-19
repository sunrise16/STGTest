#region USING
using UnityEngine;
#endregion

public class BulletBase : GameObjectBase, IObjectBase
{
    #region VARIABLE
    private SpriteRenderer pSpriteRenderer;
    private Animator pAnimator;
    private Rigidbody2D pRigidbody;
    private BoxCollider2D pBoxCollider;
    private CapsuleCollider2D pCapsuleCollider;
    private CircleCollider2D pCircleCollider;
    private Color pColor;
    private EBulletType enBulletType;
    private EBulletShooter enBulletShooter;
    private EPlayerBulletType enPlayerBulletType;
    private EEnemyBulletType enEnemyBulletType;
    private float fBulletMoveSpeed;
    private float fBulletMoveAccelerationSpeed;
    private float fBulletMoveAccelerationSpeedMax;
    private float fBulletMoveDecelerationSpeed;
    private float fBulletMoveDecelerationSpeedMin;
    private float fBulletRotateAngle;
    private float fBulletRotateSpeed;
    private float fBulletRotateAccelerationSpeed;
    private float fBulletRotateAccelerationSpeedMax;
    private float fBulletRotateDecelerationSpeed;
    private float fBulletRotateDecelerationSpeedMin;
    private float fPadding;
    private bool bBulletReflect;
    private bool bBulletBottomReflect;
    private bool bBulletChange;
    private bool bBulletBottomChange;
    private bool bBulletSplit;
    private bool bBulletBottomSplit;
    private bool bBulletAttach;
    private bool bCondition;
    private bool bCollisionDestroy;
    private bool bColliderTrigger;
    private bool bGraze;
    #endregion

    #region CONSTRUCTOR
    public BulletBase(GameObject pBulletObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale)
    {
        base.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Bullet);
    }
    #endregion

    #region GET METHOD
    public SpriteRenderer GetSpriteRenderer() { return pSpriteRenderer; }
    public Animator GetAnimator() { return pAnimator; }
    public Rigidbody2D GetRigidbody() { return pRigidbody; }
    public BoxCollider2D GetBoxCollider() { return pBoxCollider; }
    public CapsuleCollider2D GetCapsuleCollider() { return pCapsuleCollider; }
    public CircleCollider2D GetCircleCollider() { return pCircleCollider; }
    public Color GetColor() { return pColor; }
    public EBulletType GetBulletType() { return enBulletType; }
    public EBulletShooter GetBulletShooter() { return enBulletShooter; }
    public EPlayerBulletType GetPlayerBulletType() { return enPlayerBulletType; }
    public EEnemyBulletType GetEnemyBulletType() { return enEnemyBulletType; }
    public float GetBulletMoveSpeed() { return fBulletMoveSpeed; }
    public float GetBulletMoveAccelerationSpeed() { return fBulletMoveAccelerationSpeed; }
    public float GetBulletMoveAccelerationSpeedMax() { return fBulletMoveAccelerationSpeedMax; }
    public float GetBulletMoveDecelerationSpeed() { return fBulletMoveDecelerationSpeed; }
    public float GetBulletMoveDecelerationSpeedMin() { return fBulletMoveDecelerationSpeedMin; }
    public float GetBulletRotateAngle() { return fBulletRotateAngle; }
    public float GetBulletRotateSpeed() { return fBulletRotateSpeed; }
    public float GetBulletRotateAccelerationSpeed() { return fBulletRotateAccelerationSpeed; }
    public float GetBulletRotateAccelerationSpeedMax() { return fBulletRotateAccelerationSpeedMax; }
    public float GetBulletRotateDecelerationSpeed() { return fBulletRotateDecelerationSpeed; }
    public float GetBulletRotateDecelerationSpeedMin() { return fBulletRotateDecelerationSpeedMin; }
    public float GetPadding() { return fPadding; }
    public bool GetBulletReflect() { return bBulletReflect; }
    public bool GetBulletBottomReflect() { return bBulletBottomReflect; }
    public bool GetBulletChange() { return bBulletChange; }
    public bool GetBulletBottomChange() { return bBulletBottomChange; }
    public bool GetBulletSplit() { return bBulletSplit; }
    public bool GetBulletBottomSplit() { return bBulletBottomSplit; }
    public bool GetBulletAttach() { return bBulletAttach; }
    public bool GetCondition() { return bCondition; }
    public bool GetCollisionDestroy() { return bCollisionDestroy; }
    public bool GetColliderTrigger() { return bColliderTrigger; }
    public bool GetGraze() { return bGraze; }
    #endregion

    #region SET METHOD
    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer) { this.pSpriteRenderer = pSpriteRenderer; }
    public void SetAnimator(Animator pAnimator) { this.pAnimator = pAnimator; }
    public void SetRigidbody(Rigidbody2D pRigidbody) { this.pRigidbody = pRigidbody; }
    public void SetBoxCollider(BoxCollider2D pBoxCollider) { this.pBoxCollider = pBoxCollider; }
    public void SetCapsuleCollider(CapsuleCollider2D pCapsuleCollider) { this.pCapsuleCollider = pCapsuleCollider; }
    public void SetCircleCollider(CircleCollider2D pCircleCollider) { this.pCircleCollider = pCircleCollider; }
    public void SetColor(Color pColor) { this.pColor = pColor; }
    public void SetBulletType(EBulletType enBulletType) { this.enBulletType = enBulletType; }
    public void SetBulletShooter(EBulletShooter enBulletShooter) { this.enBulletShooter = enBulletShooter; }
    public void SetPlayerBulletType(EPlayerBulletType enPlayerBulletType) { this.enPlayerBulletType = enPlayerBulletType; }
    public void SetEnemyBulletType(EEnemyBulletType enEnemyBulletType) { this.enEnemyBulletType = enEnemyBulletType; }
    public void SetBulletMoveSpeed(float fBulletMoveSpeed) { this.fBulletMoveSpeed = fBulletMoveSpeed; }
    public void SetBulletMoveAccelerationSpeed(float fBulletMoveAccelerationSpeed) { this.fBulletMoveAccelerationSpeed = fBulletMoveAccelerationSpeed; }
    public void SetBulletMoveAccelerationSpeedMax(float fBulletMoveAccelerationSpeedMax) { this.fBulletMoveAccelerationSpeedMax = fBulletMoveAccelerationSpeedMax; }
    public void SetBulletMoveDecelerationSpeed(float fBulletMoveDecelerationSpeed) { this.fBulletMoveDecelerationSpeed = fBulletMoveDecelerationSpeed; }
    public void SetBulletMoveDecelerationSpeedMin(float fBulletMoveDecelerationSpeedMin) { this.fBulletMoveDecelerationSpeedMin = fBulletMoveDecelerationSpeedMin; }
    public void SetBulletSpeed(float fBulletMoveSpeed, float fBulletMoveAccelerationSpeed = 0.0f, float fBulletMoveAccelerationSpeedMax = 0.0f, float fBulletMoveDecelerationSpeed = 0.0f, float fBulletMoveDecelerationSpeedMin = 0.0f)
    {
        SetBulletMoveSpeed(fBulletMoveSpeed);
        SetBulletMoveAccelerationSpeed(fBulletMoveAccelerationSpeed);
        SetBulletMoveAccelerationSpeedMax(fBulletMoveAccelerationSpeedMax);
        SetBulletMoveDecelerationSpeed(fBulletMoveDecelerationSpeed);
        SetBulletMoveDecelerationSpeedMin(fBulletMoveDecelerationSpeedMin);
    }
    public void SetBulletRotateAngle(float fBulletRotateAngle) { this.fBulletRotateAngle = fBulletRotateAngle; }
    public void SetBulletRotateSpeed(float fBulletRotateSpeed) { this.fBulletRotateSpeed = fBulletRotateSpeed; }
    public void SetBulletRotateAccelerationSpeed(float fBulletRotateAccelerationSpeed) { this.fBulletRotateAccelerationSpeed = fBulletRotateAccelerationSpeed; }
    public void SetBulletRotateAccelerationSpeedMax(float fBulletRotateAccelerationSpeedMax) { this.fBulletRotateAccelerationSpeedMax = fBulletRotateAccelerationSpeedMax; }
    public void SetBulletRotateDecelerationSpeed(float fBulletRotateDecelerationSpeed) { this.fBulletRotateDecelerationSpeed = fBulletRotateDecelerationSpeed; }
    public void SetBulletRotateDecelerationSpeedMin(float fBulletRotateDecelerationSpeedMin) { this.fBulletRotateDecelerationSpeedMin = fBulletRotateDecelerationSpeedMin; }
    public void SetBulletRotate(float fBulletRotateAngle, float fBulletRotateSpeed = 0.0f, float fBulletRotateAccelerationSpeed = 0.0f, float fBulletRotateAccelerationSpeedMax = 0.0f, float fBulletRotateDecelerationSpeed = 0.0f, float fBulletRotateDecelerationSpeedMin = 0.0f)
    {
        SetRotationZ(fBulletRotateAngle);
        SetBulletRotateSpeed(fBulletRotateSpeed);
        SetBulletRotateAccelerationSpeed(fBulletRotateAccelerationSpeed);
        SetBulletRotateAccelerationSpeedMax(fBulletRotateAccelerationSpeedMax);
        SetBulletRotateDecelerationSpeed(fBulletRotateDecelerationSpeed);
        SetBulletRotateDecelerationSpeedMin(fBulletRotateDecelerationSpeedMin);
    }
    public void SetPadding(float fPadding) { this.fPadding = fPadding; }
    public void SetBulletReflect(bool bBulletReflect) { this.bBulletReflect = bBulletReflect; }
    public void SetBulletBottomReflect(bool bBulletBottomReflect) { this.bBulletBottomReflect = bBulletBottomReflect; }
    public void SetBulletChange(bool bBulletChange) { this.bBulletChange = bBulletChange; }
    public void SetBulletBottomChange(bool bBulletBottomChange) { this.bBulletBottomChange = bBulletBottomChange; }
    public void SetBulletSplit(bool bBulletSplit) { this.bBulletSplit = bBulletSplit; }
    public void SetBulletBottomSplit(bool bBulletBottomSplit) { this.bBulletBottomSplit = bBulletBottomSplit; }
    public void SetBulletAttach(bool bBulletAttach) { this.bBulletAttach = bBulletAttach; }
    public void SetBulletOption(bool bBulletReflect = false, bool bBulletBottomReflect = false, bool bBulletChange = false, bool bBulletBottomChange = false, bool bBulletSplit = false, bool bBulletBottomSplit = false, bool bBulletAttach = false)
    {
        SetBulletReflect(bBulletReflect);
        SetBulletBottomReflect(bBulletBottomReflect);
        SetBulletChange(bBulletChange);
        SetBulletBottomChange(bBulletBottomChange);
        SetBulletSplit(bBulletSplit);
        SetBulletBottomSplit(bBulletBottomSplit);
        SetBulletAttach(bBulletAttach);
    }
    public void SetCondition(bool bCondition) { this.bCondition = bCondition; }
    public void SetCollisionDestroy(bool bCollisionDestroy) { this.bCollisionDestroy = bCollisionDestroy; }
    public void SetColliderTrigger(bool bColliderTrigger) { this.bColliderTrigger = bColliderTrigger; }
    public void SetGraze(bool bGraze) { this.bGraze = bGraze; }
    #endregion

    #region COMMON METHOD
    public override void AllReset()
    {
        GetGameObject().layer = LayerMask.NameToLayer("Default");
        GetSpriteRenderer().sortingOrder = 6;

        SetColor(Color.white);
        SetBulletType(EBulletType.None);
        SetBulletShooter(EBulletShooter.None);
        SetPlayerBulletType(EPlayerBulletType.None);
        SetEnemyBulletType(EEnemyBulletType.None);
        SetBulletSpeed(0.0f);
        SetBulletRotate(0.0f);
        SetBulletRotateAngle(0.0f);
        SetPadding(0.0f);
        SetBulletOption();
        SetCondition(false);
        SetCollisionDestroy(false);
        SetColliderTrigger(false);
        SetGraze(false);

        base.AllReset();
    }
    #endregion
}
