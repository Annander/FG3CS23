using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private Vector3 despawnOffset;

    public Vector3 Offset => transform.TransformPoint(despawnOffset);

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Offset, .25f);
    }
}
