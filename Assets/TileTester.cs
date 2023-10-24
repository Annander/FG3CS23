using UnityEngine;

public class TileTester : MonoBehaviour
{
    [SerializeField] private TileScript[] tileScripts;

    private void Update()
    {
        foreach (var tileScript in tileScripts)
        {
            var localizedOffset = transform.InverseTransformPoint(tileScript.Offset);

            if (localizedOffset.z > 0)
            {
                Debug.DrawLine(transform.position, tileScript.Offset, Color.green);
            }
            else
            {
                Debug.DrawLine(transform.position, tileScript.Offset, Color.red);
            }
        }
    }
}