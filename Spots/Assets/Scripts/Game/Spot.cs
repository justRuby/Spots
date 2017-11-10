using UnityEngine;
using UnityEngine.UI;

public class Spot : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        GetComponentInParent<Main>().MoveSpots(gameObject.name);
    }
}
