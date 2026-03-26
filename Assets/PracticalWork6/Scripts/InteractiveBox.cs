using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    public InteractiveBox next;

    public void AddNext(InteractiveBox box)
    {
        next = box;
    }

    void Update()
    {
        if (next != null)
        {
            Vector3 start = transform.position;
            Vector3 end = next.transform.position;
            Vector3 direction = end - start;

            Debug.DrawLine(start, end, Color.cyan);

            RaycastHit hit;
            if (Physics.Raycast(start, direction, out hit, direction.magnitude))
            {
                if (hit.collider.TryGetComponent<ObstacleItem>(out var obstacle))
                {
                    obstacle.GetDamage(Time.deltaTime);
                }
            }
        }
    }
}