using System.Collections;
using UnityEngine;

public class MoveModule : SampleScript
{
    [SerializeField] 
    private Vector3 targetPosition;
    
    [SerializeField] 
    private float speed = 1.0f;

    public override void Use() => StartCoroutine(MoveRoutine());

    private IEnumerator MoveRoutine()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}