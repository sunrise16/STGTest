#region USING
using UnityEngine;
#endregion

public class Utility : Singleton<Utility>
{
    #region BULLET DESTINATION
    public Vector2 GetDestination(GameObject pObject, Vector2 vTargetPosition)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public Vector2 GetDestination(Vector2 vObjectPosition, Vector2 vTargetPosition)
    {
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public Vector2 GetRandomDestination(GameObject pObject)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        Vector2 vTargetPosition = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public Vector2 GetRandomDestination(Vector2 vObjectPosition)
    {
        Vector2 vTargetPosition = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public Vector2 GetAimedDestination(GameObject pObject, GameObject pTargetObject)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        Vector2 vTargetPosition = pTargetObject.GetComponent<Transform>().position;
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public Vector2 GetAimedDestination(Vector2 vObjectPosition, GameObject pTargetObject)
    {
        Vector2 vTargetPosition = pTargetObject.GetComponent<Transform>().position;
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    public Vector2 GetAimedDestination(Vector2 vObjectPosition, Vector2 vTargetPosition)
    {
        float fDistance = Vector2.Distance(vTargetPosition, vObjectPosition);

        return !fDistance.Equals(0) ? new Vector2(vTargetPosition.x - vObjectPosition.x, vTargetPosition.y - vObjectPosition.y) : Vector2.zero;
    }
    #endregion

    #region BULLET POSITION
    public Vector2 GetBulletPosition(GameObject pObject, float fRadius, float fAngle)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fRadian = fAngle * Mathf.PI / 180;

        return new Vector2(vObjectPosition.x + (fRadius * Mathf.Cos(fRadian)), vObjectPosition.y + (fRadius * Mathf.Sin(fRadian)));
    }
    public Vector2 GetBulletPosition(Vector2 vObjectPosition, float fRadius, float fAngle)
    {
        float fRadian = fAngle * Mathf.PI / 180;

        return new Vector2(vObjectPosition.x + (fRadius * Mathf.Cos(fRadian)), vObjectPosition.y + (fRadius * Mathf.Sin(fRadian)));
    }
    public Vector2 GetRandomBulletPosition(GameObject pObject, float fRadius)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public Vector2 GetRandomBulletPosition(Vector2 vObjectPosition, float fRadius)
    {
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public Vector2 GetRandomBulletPosition(GameObject pObject, float fMargin, float fRadius)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fAddPositionX, fAddPositionY;

        fAddPositionX = Random.Range(-fRadius, fRadius);
        if (Mathf.Abs(fAddPositionX) < fMargin)
        {
            fAddPositionX = fAddPositionX < 0.0f ? fAddPositionX - (fMargin - Mathf.Abs(fAddPositionX)) : fAddPositionX + (fMargin - Mathf.Abs(fAddPositionX));
        }
        fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public Vector2 GetRandomBulletPosition(Vector2 vObjectPosition, float fMargin, float fRadius)
    {
        float fAddPositionX, fAddPositionY;

        fAddPositionX = Random.Range(-fRadius, fRadius);
        if (Mathf.Abs(fAddPositionX) < fMargin)
        {
            fAddPositionX = fAddPositionX < 0.0f ? fAddPositionX - (fMargin - Mathf.Abs(fAddPositionX)) : fAddPositionX + (fMargin - Mathf.Abs(fAddPositionX));
        }
        fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));

        return new Vector2(vObjectPosition.x + fAddPositionX, Random.Range(vObjectPosition.y - fAddPositionY, vObjectPosition.y + fAddPositionY));
    }
    public Vector2 GetRandomBulletPositionMax(GameObject pObject, float fRadius)
    {
        Vector2 vObjectPosition = pObject.GetComponent<Transform>().position;
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));
        int iPositionMultiply = Random.Range(0, 2);

        return new Vector2(vObjectPosition.x + fAddPositionX, (iPositionMultiply.Equals(0) ? vObjectPosition.y + fAddPositionY : vObjectPosition.y - fAddPositionY));
    }
    public Vector2 GetRandomBulletPositionMax(Vector2 vObjectPosition, float fRadius)
    {
        float fAddPositionX = Random.Range(-fRadius, fRadius);
        float fAddPositionY = Mathf.Sqrt(Mathf.Pow(fRadius, 2.0f) - Mathf.Pow(fAddPositionX, 2.0f));
        int iPositionMultiply = Random.Range(0, 2);

        return new Vector2(vObjectPosition.x + fAddPositionX, (iPositionMultiply.Equals(0) ? vObjectPosition.y + fAddPositionY : vObjectPosition.y - fAddPositionY));
    }
    #endregion

    #region BULLET ROTATION
    public Quaternion VectorToRotationSlerp(Quaternion srcRotation, Vector3 targetPos, float slerpSpeed)
    {
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        Quaternion rotationTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        return Quaternion.Slerp(srcRotation, rotationTarget, Time.deltaTime * slerpSpeed);
    }
    #endregion
}