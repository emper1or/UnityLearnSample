using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    public GameObject prefab;
    private InteractiveBox _currentSelected;

    void Update()
    {
        // Левый клик
        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }

        // Правый клик
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
    }

    private void HandleLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("InteractivePlane"))
            {
                Vector3 spawnPos = hit.point + hit.normal * (prefab.transform.localScale.y / 2);
                Instantiate(prefab, spawnPos, Quaternion.identity);
            }
            else if (hit.collider.TryGetComponent<InteractiveBox>(out var clickedBox))
            {
                if (_currentSelected == null)
                {
                    _currentSelected = clickedBox;
                }
                else if (_currentSelected != clickedBox)
                {
                    _currentSelected.AddNext(clickedBox);
                    _currentSelected = null;
                }
            }
        }
    }

    private void HandleRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<InteractiveBox>(out var boxToDelete))
            {
                ClearReferencesTo(boxToDelete);

                if (_currentSelected == boxToDelete)
                {
                    _currentSelected = null;
                }

                Destroy(boxToDelete.gameObject);
            }
        }
    }

    private void ClearReferencesTo(InteractiveBox target)
    {
        InteractiveBox[] allBoxes = FindObjectsOfType<InteractiveBox>();

        foreach (var box in allBoxes)
        {
            if (box != target && box.next == target)
            {
                box.next = null;
            }
        }
    }
}