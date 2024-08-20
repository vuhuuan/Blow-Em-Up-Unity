using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotNo1StateMachine))]
public class RobotNo1Controller : MonoBehaviour
{
    [SerializeField] private float movingRadius = 4f;
    [SerializeField] private Vector3 centerPointPos;
    [SerializeField] private float speed = 1f; // Tốc độ di chuyển của robot
    [SerializeField] private Vector3[] targetAngles;

    [Range(0f, 1f)]
    [SerializeField] private float lerpSpeed = 0.2f; // Tốc độ di chuyển của robot
    [SerializeField] private float epsilon = 0.1f; // Ngưỡng để xác định hoàn tất Lerp

    private float angle = 0f;
    private int currentTargetIndex = 0;

    [SerializeField] private RobotNo1StateMachine _stateMachine;
    [SerializeField] private EnemyVision _enemyVision;
    public bool IsAttacking = false;



    [SerializeField] private Transform player;

    public float LostPlayerCoolDown;
  
    private void Awake()
    {
        _stateMachine = GetComponent<RobotNo1StateMachine>();
        _enemyVision = GetComponent<EnemyVision>();
    }

    private void Start()
    {
        centerPointPos = transform.position;

    }

    // Điều khiển các trạng thái
    private void Update()
    {

    }


    // các hành động kết hợp lại thành trạng thái.

    public void MovingAround()
    {
        // Tính toán góc mới của robot
        angle += speed * Time.deltaTime;

        // Tính toán vị trí mới của robot dựa trên bán kính và góc hiện tại
        float x = centerPointPos.x + Mathf.Cos(angle) * movingRadius;
        float y = centerPointPos.y + Mathf.Sin(angle) * movingRadius;

        // Cập nhật vị trí của robot
        transform.position = new Vector3(x, y, transform.position.z);
    }


    public void LookAround()
    {
        // lerp to target angle

        Quaternion currentQtn = Quaternion.Euler(transform.localEulerAngles);
        Quaternion targetQtn = Quaternion.Euler(targetAngles[currentTargetIndex]);

        transform.rotation = Quaternion.Lerp(currentQtn, targetQtn, lerpSpeed);
        
        // Kiểm tra xem quá trình Lerp đã hoàn tất chưa
        if (Quaternion.Angle(currentQtn, targetQtn) < epsilon)
        {
            // Nếu đã hoàn tất, chuyển sang target index khác
            currentTargetIndex = (currentTargetIndex + 1) % targetAngles.Length;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;

        float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        Quaternion currentQtn = transform.rotation;

        Quaternion targetQtn = Quaternion.Euler(new Vector3 (0, 0, targetAngle));

        transform.rotation = Quaternion.Lerp(currentQtn, targetQtn, lerpSpeed * 2);
    }

    public bool DetectPlayer()
    {
        return _enemyVision.PlayerDetected;
    }

    public void ChangeState(string stateName)
    {
        _stateMachine.ChangeState(stateName);
    }
}
