using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchControler : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{
    private IPointerClickHandler _pointerClickHandlerImplementation;

    public void OnPointerClick(PointerEventData eventData)
    {
        Ray touch = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(touch, out RaycastHit hit, 20000f))
        {
            if (hit.collider.gameObject.GetComponent<ObjectSelector>())
            {
                if (!CameraController.I.Draged)
                {
                    print("hit point");
                }
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(WaitToRelease());
    }

    public IEnumerator WaitToRelease()
    {
        yield return new WaitForSeconds(.1f);
        CameraController.I.Draged = false;
    }
}
