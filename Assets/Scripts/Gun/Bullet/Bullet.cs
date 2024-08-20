using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;



public class Bullet : MonoBehaviour
{
    public BulletType bulletType;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 5f;

    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
    }

    private void Start()
    {
        Invoke("DisableBullet", lifetime);
    }

    public virtual void Shoot(GameObject source)
    {
        gameObject.SetActive(true);
        Physics2D.IgnoreLayerCollision(gameObject.layer, source.layer, true);

        transform.position = source.transform.position;

        Vector3 direction = source.transform.right;

        // Tính toán góc quay từ hướng hiện tại đến hướng bắn
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        rb.velocity = direction * speed;

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision logic (pooling)
        rb.velocity = Vector2.zero;
        BulletPool.Instance.ReturnBullet(this);
    }

    private void DisableBullet()
    {
        // Vô hiệu hóa đối tượng sau khi lifetime kết thúc
        BulletPool.Instance.ReturnBullet(this);
    }
}
