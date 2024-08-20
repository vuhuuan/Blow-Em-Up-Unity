using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float viewRadius = 5f;

    [Range(0, 360)]
    public float viewAngle = 90;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool PlayerDetected { get; set; }

    private void Start()
    {
        PlayerDetected = false;
    }
    private void OnDrawGizmos()
    {
        if (PlayerDetected)
        {
            Gizmos.color = Color.red;
        } else
        {
            Gizmos.color = Color.green;
        }
        //Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector2 leftBoundary = DirFromAngle(-viewAngle / 2, false);
        Vector2 rightBoundary = DirFromAngle(viewAngle / 2, false);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rightBoundary * viewRadius);
    }

    private void Update()
    {
        FindPlayer();
    }

    // Hàm tính toán hướng từ góc
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }

    public void FindPlayer()
    {
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
            
            // Kiểm tra nếu đối tượng nằm trong góc nhìn hình quạt
            if (Vector2.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.transform.position);
                // Kiểm tra nếu không có chướng ngại vật che khuất tầm nhìn
                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    Debug.Log("See player: " + target.name);
                    PlayerDetected = true;
                    return;
                }
            }
        }
        PlayerDetected = false;
    }
}
