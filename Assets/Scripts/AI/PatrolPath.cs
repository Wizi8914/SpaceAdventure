using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public Transform[] patrolPoints;

    public void OnDrawGizmos()
    {
        if (patrolPoints == null) return;
        if (patrolPoints.Length == 0) return;

        Gizmos.color = Color.blue;
        Gizmos.matrix = transform.localToWorldMatrix;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            //Gizmos.matrix = patrolPoints[i].localToWorldMatrix;
            Gizmos.DrawSphere(patrolPoints[i].localPosition, 0.5f);
            
            Vector3 from = patrolPoints[i].localPosition;
            Vector3 to = patrolPoints[(i+1) % patrolPoints.Length].localPosition;
            Gizmos.DrawLine(from, to);
        }
    }
}
