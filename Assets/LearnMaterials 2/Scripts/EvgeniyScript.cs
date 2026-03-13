using UnityEngine;
using System.Collections;

public class EvgeniyScript : SampleScript
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 rotationAngle;

    public override void Use()
    { 
        StartCoroutine(RotateObject());
    }

    private IEnumerator RotateObject()
    {

        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = initialRotation * Quaternion.Euler(rotationAngle);
        float angle = Quaternion.Angle(initialRotation, finalRotation);

        if (speed <= 0)
        {
            Debug.LogError("Speed must be a positive value.");
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
    }
}