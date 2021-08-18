#region USING
using UnityEngine;
#endregion

public class GameObjectBase
{
    #region VARIABLE
    protected GameObject pGameObject = null;
    protected Transform pTransform = null;
    protected Vector3 vTempPosition = Vector3.zero;
    protected Vector3 vTempLocalPosition = Vector3.zero;
    protected Vector3 vTempRotation = Vector3.zero;
    protected Vector3 vTempLocalRotation = Vector3.zero;
    protected Vector3 vTempScale = Vector3.zero;
    protected EGameObjectType enGameObjectType = EGameObjectType.Max;
    protected int iUniqueNumber = 0;
    protected int iUniqueSubNumber = 0;
    protected string szUniqueString = "";
    protected string szUniqueSubString = "";
    protected bool bVisible = false;
    #endregion

    #region GET METHOD
    public GameObject GetGameObject() { return (!pGameObject.Equals(null)) ? pGameObject : null; }
    public GameObject GetChildGameObject(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).gameObject : null; }
    public Transform GetTransform() { return (!pGameObject.Equals(null)) ? pTransform : null; }
    public Transform GetChildTransform(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex) : null; }

    public Vector3 GetPosition() { return (!pGameObject.Equals(null)) ? GetTransform().position : Vector3.zero; }
    public float GetPositionX() { return (!pGameObject.Equals(null)) ? GetTransform().position.x : 0; }
    public float GetPositionY() { return (!pGameObject.Equals(null)) ? GetTransform().position.y : 0; }
    public float GetPositionZ() { return (!pGameObject.Equals(null)) ? GetTransform().position.z : 0; }

    public Vector3 GetLocalPosition() { return (!pGameObject.Equals(null)) ? GetTransform().localPosition : Vector3.zero; }
    public float GetLocalPositionX() { return (!pGameObject.Equals(null)) ? GetTransform().localPosition.x : 0; }
    public float GetLocalPositionY() { return (!pGameObject.Equals(null)) ? GetTransform().localPosition.y : 0; }
    public float GetLocalPositionZ() { return (!pGameObject.Equals(null)) ? GetTransform().localPosition.z : 0; }

    public Vector3 GetRotation() { return (!pGameObject.Equals(null)) ? GetTransform().rotation.eulerAngles : Quaternion.identity.eulerAngles; }
    public float GetRotationX() { return (!pGameObject.Equals(null)) ? GetTransform().rotation.eulerAngles.x : 0; }
    public float GetRotationY() { return (!pGameObject.Equals(null)) ? GetTransform().rotation.eulerAngles.y : 0; }
    public float GetRotationZ() { return (!pGameObject.Equals(null)) ? GetTransform().rotation.eulerAngles.z : 0; }

    public Vector3 GetLocalRotation() { return (!pGameObject.Equals(null)) ? GetTransform().localRotation.eulerAngles : Quaternion.identity.eulerAngles; }
    public float GetLocalRotationX() { return (!pGameObject.Equals(null)) ? GetTransform().localRotation.eulerAngles.x : 0; }
    public float GetLocalRotationY() { return (!pGameObject.Equals(null)) ? GetTransform().localRotation.eulerAngles.y : 0; }
    public float GetLocalRotationZ() { return (!pGameObject.Equals(null)) ? GetTransform().localRotation.eulerAngles.z : 0; }

    public Vector3 GetScale() { return (!pGameObject.Equals(null)) ? GetTransform().localScale : Vector3.one; }
    public float GetScaleX() { return (!pGameObject.Equals(null)) ? GetTransform().localScale.x : 1; }
    public float GetScaleY() { return (!pGameObject.Equals(null)) ? GetTransform().localScale.y : 1; }
    public float GetScaleZ() { return (!pGameObject.Equals(null)) ? GetTransform().localScale.z : 1; }

    public Vector3 GetTempPosition() { return vTempPosition; }
    public float GetTempPositionX() { return vTempPosition.x; }
    public float GetTempPositionY() { return vTempPosition.y; }
    public float GetTempPositionZ() { return vTempPosition.z; }

    public Vector3 GetTempLocalPosition() { return vTempLocalPosition; }
    public float GetTempLocalPositionX() { return vTempLocalPosition.x; }
    public float GetTempLocalPositionY() { return vTempLocalPosition.y; }
    public float GetTempLocalPositionZ() { return vTempLocalPosition.z; }

    public Vector3 GetTempRotation() { return vTempRotation; }
    public float GetTempRotationX() { return vTempRotation.x; }
    public float GetTempRotationY() { return vTempRotation.y; }
    public float GetTempRotationZ() { return vTempRotation.z; }

    public Vector3 GetTempLocalRotation() { return vTempLocalRotation; }
    public float GetTempLocalRotationX() { return vTempLocalRotation.x; }
    public float GetTempLocalRotationY() { return vTempLocalRotation.y; }
    public float GetTempLocalRotationZ() { return vTempLocalRotation.z; }

    public Vector3 GetTempScale() { return vTempScale; }
    public float GetTempScaleX() { return vTempScale.x; }
    public float GetTempScaleY() { return vTempScale.y; }
    public float GetTempScaleZ() { return vTempScale.z; }

    public Vector3 GetChildPosition(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).position : Vector3.zero; }
    public float GetChildPositionX(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).position.x : 0; }
    public float GetChildPositionY(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).position.y : 0; }
    public float GetChildPositionZ(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).position.z : 0; }

    public Vector3 GetChildLocalPosition(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localPosition : Vector3.zero; }
    public float GetChildLocalPositionX(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localPosition.x : 0; }
    public float GetChildLocalPositionY(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localPosition.y : 0; }
    public float GetChildLocalPositionZ(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localPosition.z : 0; }

    public Vector3 GetChildRotation(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).rotation.eulerAngles : Quaternion.identity.eulerAngles; }
    public float GetChildRotationX(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).rotation.eulerAngles.x : 0; }
    public float GetChildRotationY(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).rotation.eulerAngles.y : 0; }
    public float GetChildRotationZ(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).rotation.eulerAngles.z : 0; }

    public Vector3 GetChildLocalRotation(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localRotation.eulerAngles : Quaternion.identity.eulerAngles; }
    public float GetChildLocalRotationX(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localRotation.eulerAngles.x : 0; }
    public float GetChildLocalRotationY(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localRotation.eulerAngles.y : 0; }
    public float GetChildLocalRotationZ(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localRotation.eulerAngles.z : 0; }

    public Vector3 GetChildScale(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localScale : Vector3.one; }
    public float GetChildScaleX(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localScale.x : 1; }
    public float GetChildScaleY(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localScale.y : 1; }
    public float GetChildScaleZ(int iChildIndex) { return (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? GetTransform().GetChild(iChildIndex).localScale.z : 1; }

    public EGameObjectType GetGameObjectType() { return enGameObjectType; }
    public int GetUniqueNumber() { return iUniqueNumber; }
    public int GetUniqueSubNumber() { return iUniqueSubNumber; }
    public string GetUniqueString() { return szUniqueString; }
    public string GetUniqueSubString() { return szUniqueSubString; }
    public bool GetVisible() { return bVisible; }
    #endregion

    #region SET METHOD
    public void SetGameObject(GameObject pGameObject) { this.pGameObject = pGameObject; }
    public void SetTransform(Transform pTransform) { this.pTransform = pTransform; }

    public void SetPosition() { GetTransform().position = Vector3.zero; }
    public void SetPosition(Vector3 vObjectPosition) { GetTransform().position = (!pGameObject.Equals(null)) ? vObjectPosition : Vector3.zero; }
    public void SetPositionX(float vObjectPositionX) { GetTransform().position = (!pGameObject.Equals(null)) ? new Vector3(vObjectPositionX, GetPositionY(), GetPositionZ()) : Vector3.zero; }
    public void SetPositionY(float vObjectPositionY) { GetTransform().position = (!pGameObject.Equals(null)) ? new Vector3(GetPositionX(), vObjectPositionY, GetPositionZ()) : Vector3.zero; }
    public void SetPositionZ(float vObjectPositionZ) { GetTransform().position = (!pGameObject.Equals(null)) ? new Vector3(GetPositionX(), GetPositionY(), vObjectPositionZ) : Vector3.zero; }

    public void SetLocalPosition() { GetTransform().localPosition = Vector3.zero; }
    public void SetLocalPosition(Vector3 vObjectLocalPosition) { GetTransform().localPosition = (!pGameObject.Equals(null)) ? vObjectLocalPosition : Vector3.zero; }
    public void SetLocalPositionX(float vObjectLocalPositionX) { GetTransform().localPosition = (!pGameObject.Equals(null)) ? new Vector3(vObjectLocalPositionX, GetLocalPositionY(), GetLocalPositionZ()) : Vector3.zero; }
    public void SetLocalPositionY(float vObjectLocalPositionY) { GetTransform().localPosition = (!pGameObject.Equals(null)) ? new Vector3(GetLocalPositionX(), vObjectLocalPositionY, GetLocalPositionZ()) : Vector3.zero; }
    public void SetLocalPositionZ(float vObjectLocalPositionZ) { GetTransform().localPosition = (!pGameObject.Equals(null)) ? new Vector3(GetLocalPositionX(), GetLocalPositionY(), vObjectLocalPositionZ) : Vector3.zero; }

    public void SetRotation() { GetTransform().rotation = Quaternion.identity; }
    public void SetRotation(Vector3 vObjectRotation) { GetTransform().rotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(vObjectRotation) : Quaternion.identity; }
    public void SetRotationX(float vObjectRotationX) { GetTransform().rotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(new Vector3(vObjectRotationX, GetRotationY(), GetRotationZ())) : Quaternion.identity; }
    public void SetRotationY(float vObjectRotationY) { GetTransform().rotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetRotationX(), vObjectRotationY, GetRotationZ())) : Quaternion.identity; }
    public void SetRotationZ(float vObjectRotationZ) { GetTransform().rotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetRotationX(), GetRotationY(), vObjectRotationZ)) : Quaternion.identity; }

    public void SetLocalRotation() { GetTransform().localRotation = Quaternion.identity; }
    public void SetLocalRotation(Vector3 vObjectLocalRotation) { GetTransform().localRotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(vObjectLocalRotation) : Quaternion.identity; }
    public void SetLocalRotationX(float vObjectLocalRotationX) { GetTransform().localRotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(new Vector3(vObjectLocalRotationX, GetLocalRotationY(), GetLocalRotationZ())) : Quaternion.identity; }
    public void SetLocalRotationY(float vObjectLocalRotationY) { GetTransform().localRotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetLocalRotationX(), vObjectLocalRotationY, GetLocalRotationZ())) : Quaternion.identity; }
    public void SetLocalRotationZ(float vObjectLocalRotationZ) { GetTransform().localRotation = (!pGameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetLocalRotationX(), GetLocalRotationY(), vObjectLocalRotationZ)) : Quaternion.identity; }

    public void SetScale() { GetTransform().localScale = Vector3.one; }
    public void SetScale(Vector3 vObjectScale) { GetTransform().localScale = (!pGameObject.Equals(null)) ? vObjectScale : Vector3.one; }
    public void SetScaleX(float vObjectScaleX) { GetTransform().localScale = (!pGameObject.Equals(null)) ? new Vector3(vObjectScaleX, GetScaleY(), GetScaleZ()) : Vector3.one; }
    public void SetScaleY(float vObjectScaleY) { GetTransform().localScale = (!pGameObject.Equals(null)) ? new Vector3(GetScaleX(), vObjectScaleY, GetScaleZ()) : Vector3.one; }
    public void SetScaleZ(float vObjectScaleZ) { GetTransform().localScale = (!pGameObject.Equals(null)) ? new Vector3(GetScaleX(), GetScaleY(), vObjectScaleZ) : Vector3.one; }

    public void SetTempPosition() { vTempPosition = new Vector3(0, 0, 0); }
    public void SetTempPosition(Vector3 vTempPosition) { this.vTempPosition = vTempPosition; }
    public void SetTempPositionX(float vTempPositionX) { vTempPosition = new Vector3(vTempPositionX, GetTempPositionY(), GetTempPositionZ()); }
    public void SetTempPositionY(float vTempPositionY) { vTempPosition = new Vector3(GetTempPositionX(), vTempPositionY, GetTempPositionZ()); }
    public void SetTempPositionZ(float vTempPositionZ) { vTempPosition = new Vector3(GetTempPositionX(), GetTempPositionY(), vTempPositionZ); }

    public void SetTempLocalPosition() { vTempLocalPosition = new Vector3(0, 0, 0); }
    public void SetTempLocalPosition(Vector3 vTempLocalPosition) { this.vTempLocalPosition = vTempLocalPosition; }
    public void SetTempLocalPositionX(float vTempLocalPositionX) { vTempLocalPosition = new Vector3(vTempLocalPositionX, GetTempLocalPositionY(), GetTempLocalPositionZ()); }
    public void SetTempLocalPositionY(float vTempLocalPositionY) { vTempLocalPosition = new Vector3(GetTempLocalPositionX(), vTempLocalPositionY, GetTempLocalPositionZ()); }
    public void SetTempLocalPositionZ(float vTempLocalPositionZ) { vTempLocalPosition = new Vector3(GetTempLocalPositionX(), GetTempLocalPositionY(), vTempLocalPositionZ); }

    public void SetTempRotation() { vTempRotation = new Vector3(0, 0, 0); }
    public void SetTempRotation(Vector3 vTempRotation) { this.vTempRotation = vTempRotation; }
    public void SetTempRotationX(float vTempRotationX) { vTempRotation = new Vector3(vTempRotationX, GetTempRotationY(), GetTempRotationZ()); }
    public void SetTempRotationY(float vTempRotationY) { vTempRotation = new Vector3(GetTempRotationX(), vTempRotationY, GetTempRotationZ()); }
    public void SetTempRotationZ(float vTempRotationZ) { vTempRotation = new Vector3(GetTempRotationX(), GetTempRotationY(), vTempRotationZ); }

    public void SetTempLocalRotation() { vTempLocalRotation = new Vector3(0, 0, 0); }
    public void SetTempLocalRotation(Vector3 vTempLocalRotation) { this.vTempLocalRotation = vTempLocalRotation; }
    public void SetTempLocalRotationX(float vTempLocalRotationX) { vTempLocalRotation = new Vector3(vTempLocalRotationX, GetTempLocalRotationY(), GetTempLocalRotationZ()); }
    public void SetTempLocalRotationY(float vTempLocalRotationY) { vTempLocalRotation = new Vector3(GetTempLocalRotationX(), vTempLocalRotationY, GetTempLocalRotationZ()); }
    public void SetTempLocalRotationZ(float vTempLocalRotationZ) { vTempLocalRotation = new Vector3(GetTempLocalRotationX(), GetTempLocalRotationY(), vTempLocalRotationZ); }

    public void SetTempScale() { vTempScale = new Vector3(0, 0, 0); }
    public void SetTempScale(Vector3 vTempScale) { this.vTempScale = vTempScale; }
    public void SetTempScaleX(float vTempScaleX) { vTempScale = new Vector3(vTempScaleX, GetTempScaleY(), GetTempScaleZ()); }
    public void SetTempScaleY(float vTempScaleY) { vTempScale = new Vector3(GetTempScaleX(), vTempScaleY, GetTempScaleZ()); }
    public void SetTempScaleZ(float vTempScaleZ) { vTempScale = new Vector3(GetTempScaleX(), GetTempScaleY(), vTempScaleZ); }

    public void SetChildPosition(int iChildIndex) { GetTransform().GetChild(iChildIndex).position = Vector3.zero; }
    public void SetChildPosition(int iChildIndex, Vector3 vObjectPosition) { GetTransform().GetChild(iChildIndex).position = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? vObjectPosition : Vector3.zero; }
    public void SetChildPositionX(int iChildIndex, float vObjectPositionX) { GetTransform().GetChild(iChildIndex).position = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(vObjectPositionX, GetChildPositionY(iChildIndex), GetChildPositionZ(iChildIndex)) : Vector3.zero; }
    public void SetChildPositionY(int iChildIndex, float vObjectPositionY) { GetTransform().GetChild(iChildIndex).position = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(GetChildPositionX(iChildIndex), vObjectPositionY, GetChildPositionZ(iChildIndex)) : Vector3.zero; }
    public void SetChildPositionZ(int iChildIndex, float vObjectPositionZ) { GetTransform().GetChild(iChildIndex).position = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(GetChildPositionX(iChildIndex), GetChildPositionY(iChildIndex), vObjectPositionZ) : Vector3.zero; }

    public void SetChildLocalPosition(int iChildIndex) { GetTransform().GetChild(iChildIndex).localPosition = Vector3.zero; }
    public void SetChildLocalPosition(int iChildIndex, Vector3 vObjectLocalPosition) { GetTransform().GetChild(iChildIndex).localPosition = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? vObjectLocalPosition : Vector3.zero; }
    public void SetChildLocalPositionX(int iChildIndex, float vObjectLocalPositionX) { GetTransform().GetChild(iChildIndex).localPosition = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(vObjectLocalPositionX, GetChildLocalPositionY(iChildIndex), GetChildLocalPositionZ(iChildIndex)) : Vector3.zero; }
    public void SetChildLocalPositionY(int iChildIndex, float vObjectLocalPositionY) { GetTransform().GetChild(iChildIndex).localPosition = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(GetChildLocalPositionX(iChildIndex), vObjectLocalPositionY, GetChildLocalPositionZ(iChildIndex)) : Vector3.zero; }
    public void SetChildLocalPositionZ(int iChildIndex, float vObjectLocalPositionZ) { GetTransform().GetChild(iChildIndex).localPosition = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(GetChildLocalPositionX(iChildIndex), GetChildLocalPositionY(iChildIndex), vObjectLocalPositionZ) : Vector3.zero; }

    public void SetChildRotation(int iChildIndex) { GetTransform().GetChild(iChildIndex).rotation = Quaternion.identity; }
    public void SetChildRotation(int iChildIndex, Vector3 vObjectRotation) { GetTransform().GetChild(iChildIndex).rotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(vObjectRotation) : Quaternion.identity; }
    public void SetChildRotationX(int iChildIndex, float vObjectRotationX) { GetTransform().GetChild(iChildIndex).rotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(new Vector3(vObjectRotationX, GetChildRotationY(iChildIndex), GetChildRotationZ(iChildIndex))) : Quaternion.identity; }
    public void SetChildRotationY(int iChildIndex, float vObjectRotationY) { GetTransform().GetChild(iChildIndex).rotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetChildRotationX(iChildIndex), vObjectRotationY, GetChildRotationZ(iChildIndex))) : Quaternion.identity; }
    public void SetChildRotationZ(int iChildIndex, float vObjectRotationZ) { GetTransform().GetChild(iChildIndex).rotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetChildRotationX(iChildIndex), GetChildRotationY(iChildIndex), vObjectRotationZ)) : Quaternion.identity; }

    public void SetChildLocalRotation(int iChildIndex) { GetTransform().GetChild(iChildIndex).localRotation = Quaternion.identity; }
    public void SetChildLocalRotation(int iChildIndex, Vector3 vObjectLocalRotation) { GetTransform().GetChild(iChildIndex).localRotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(vObjectLocalRotation) : Quaternion.identity; }
    public void SetChildLocalRotationX(int iChildIndex, float vObjectLocalRotationX) { GetTransform().GetChild(iChildIndex).localRotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(new Vector3(vObjectLocalRotationX, GetChildLocalRotationY(iChildIndex), GetChildLocalRotationZ(iChildIndex))) : Quaternion.identity; }
    public void SetChildLocalRotationY(int iChildIndex, float vObjectLocalRotationY) { GetTransform().GetChild(iChildIndex).localRotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetChildLocalRotationX(iChildIndex), vObjectLocalRotationY, GetChildLocalRotationZ(iChildIndex))) : Quaternion.identity; }
    public void SetChildLocalRotationZ(int iChildIndex, float vObjectLocalRotationZ) { GetTransform().GetChild(iChildIndex).localRotation = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? Quaternion.Euler(new Vector3(GetChildLocalRotationX(iChildIndex), GetChildLocalRotationY(iChildIndex), vObjectLocalRotationZ)) : Quaternion.identity; }

    public void SetChildScale(int iChildIndex) { GetTransform().GetChild(iChildIndex).localScale = Vector3.one; }
    public void SetChildScale(int iChildIndex, Vector3 vObjectScale) { GetTransform().GetChild(iChildIndex).localScale = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? vObjectScale : Vector3.one; }
    public void SetChildScaleX(int iChildIndex, float vObjectScaleX) { GetTransform().GetChild(iChildIndex).localScale = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(vObjectScaleX, GetChildScaleY(iChildIndex), GetChildScaleZ(iChildIndex)) : Vector3.one; }
    public void SetChildScaleY(int iChildIndex, float vObjectScaleY) { GetTransform().GetChild(iChildIndex).localScale = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(GetChildScaleX(iChildIndex), vObjectScaleY, GetChildScaleZ(iChildIndex)) : Vector3.one; }
    public void SetChildScaleZ(int iChildIndex, float vObjectScaleZ) { GetTransform().GetChild(iChildIndex).localScale = (!GetTransform().GetChild(iChildIndex).gameObject.Equals(null)) ? new Vector3(GetChildScaleX(iChildIndex), GetChildScaleY(iChildIndex), vObjectScaleZ) : Vector3.one; }

    public void SetGameObjectType() { enGameObjectType = EGameObjectType.None; }
    public void SetGameObjectType(EGameObjectType enGameObjectType) { this.enGameObjectType = enGameObjectType; }
    public void SetUniqueNumber() { iUniqueNumber = 0; }
    public void SetUniqueNumber(int iUniqueNumber) { this.iUniqueNumber = iUniqueNumber; }
    public void SetUniqueSubNumber() { iUniqueSubNumber = 0; }
    public void SetUniqueSubNumber(int iUniqueSubNumber) { this.iUniqueSubNumber = iUniqueSubNumber; }
    public void SetUniqueString() { szUniqueString = ""; }
    public void SetUniqueString(string szUniqueString) { this.szUniqueString = szUniqueString; }
    public void SetUniqueSubString() { szUniqueSubString = ""; }
    public void SetUniqueSubString(string szUniqueSubString) { this.szUniqueSubString = szUniqueSubString; }
    public void SetVisible(bool bVisible)
    {
        this.bVisible = bVisible;
        if (!pGameObject.Equals(null)) pGameObject.SetActive(bVisible);
    }
    #endregion

    #region VIRTUAL METHOD
    public virtual void Init(GameObject pObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale, EGameObjectType enGameObjectType)
    {
        SetGameObject(pObject);
        SetTransform(pTransform);

        SetPosition(vSpawnPosition);
        SetRotation();
        SetScale(vScale);

        SetTempPosition(vSpawnPosition);
        SetTempLocalPosition();
        SetTempRotation();
        SetTempLocalRotation();
        SetTempScale(vScale);

        SetGameObjectType(enGameObjectType);
        SetUniqueNumber();
        SetUniqueSubNumber();
        SetUniqueString();
        SetUniqueSubString();
        SetVisible(true);
    }
    public virtual void AllClear()
    {
        SetGameObject(null);
        SetTransform(null);
        AllReset();

        GameObject.Destroy(pGameObject);
    }
    public virtual void AllReset()
    {
        SetPosition();
        SetLocalPosition();
        SetRotation();
        SetLocalRotation();
        SetScale();

        SetTempPosition();
        SetTempLocalPosition();
        SetTempRotation();
        SetTempLocalRotation();
        SetTempScale();

        for (int i = 0; i < GetTransform().childCount; i++)
        {
            SetChildPosition(i);
            SetChildLocalPosition(i);
            SetChildRotation(i);
            SetChildLocalRotation(i);
            SetChildScale(i);
        }

        SetGameObjectType();
        SetUniqueNumber();
        SetUniqueSubNumber();
        SetUniqueString();
        SetUniqueSubString();
        SetVisible(false);
    }
    #endregion
}