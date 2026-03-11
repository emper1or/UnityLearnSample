using UnityEngine;
using System.Collections;

public class EvgeniyScript : SampleScript
{
    public float speed = 10f;
    public Vector3 rotationAngle;

    private bool isRotating = false;

    public override void Use()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateObject());
        }
    }

    private IEnumerator RotateObject()
    {
        isRotating = true;

        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = initialRotation * Quaternion.Euler(rotationAngle);
        float angle = Quaternion.Angle(initialRotation, finalRotation);

        if (speed <= 0)
        {
            Debug.LogError("Speed must be a positive value.");
            isRotating = false;
            yield break;
        }
        
        float duration = angle / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = finalRotation;
        isRotating = false;
    }
}
