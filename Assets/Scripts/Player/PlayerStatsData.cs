using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// file name: tên mặc định, menu name: tên khi chọn tạo ở inspector; order: thứ tự hiển thị trong menu lúc tạo assets.
[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStatsData : ScriptableObject
{
    [Header("Movement")]

    public float moveSpeed = 8f;
    public float acceleration = 60f;
    public float deceleration = 60f;

    [Range(0, 1)]
    public float GroundRayCastBuffer = 0.2f;


    [Header("Jump")]
    public float jumpAcceleration = 0.32f;
    public float jumpForce = 24f;


    [Header("Heavy fall")]
    public float fallAcceleration = 60f;

    [Range(-40, 0)]
    public float maxFallSpeed = -24f;

}
