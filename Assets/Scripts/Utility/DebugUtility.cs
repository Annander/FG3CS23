using UnityEngine;

public class DebugUtility 
{
	public static void DrawCross(Vector3 worldPosition, Color color, float size = .5f, float duration = 0 ) 
	{
		Debug.DrawLine(worldPosition + (Vector3.up * size), worldPosition + (Vector3.down * size), color, duration);
		Debug.DrawLine(worldPosition + (Vector3.right * size), worldPosition + (Vector3.left * size), color, duration);
		Debug.DrawLine(worldPosition + (Vector3.forward * size), worldPosition + (Vector3.back * size), color, duration);
	}
	
	public static void DrawGizmoCross(Vector3 worldPosition, Color color, float size = .5f)
	{
		var previousColor = Gizmos.color;
		
		Gizmos.color = color;
		
		Gizmos.DrawLine(worldPosition + (Vector3.up * size), worldPosition + (Vector3.down * size));
		Gizmos.DrawLine(worldPosition + (Vector3.right * size), worldPosition + (Vector3.left * size));
		Gizmos.DrawLine(worldPosition + (Vector3.forward * size), worldPosition + (Vector3.back * size));

		Gizmos.color = previousColor;
	}
}