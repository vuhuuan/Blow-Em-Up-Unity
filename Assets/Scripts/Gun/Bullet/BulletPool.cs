using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType
{
    Yellow,
    Green
}

public class BulletPool : MonoBehaviour
{
    [SerializeField] private List<Bullet> bulletPrefab;

    public int initPoolSize;
    public static BulletPool Instance { get; set; }

    private Transform bulletPoolContainer;

    private List<Bullet> pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }

        bulletPoolContainer = gameObject.transform;
        pool = new List<Bullet>();
        InitializeBulletPool();
    }

    void InitializeBulletPool()
    {
        for (int i = 0; i < initPoolSize; i++)
        {
            NewBulletInPool(BulletType.Yellow);
        }

        for (int i = 0; i < initPoolSize; i++)
        {
            NewBulletInPool(BulletType.Green);
        }
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        foreach (Bullet bullet in pool)
        {
            if (!bullet.gameObject.activeSelf)
            {
                if (bullet.bulletType == bulletType)
                {
                    return bullet;
                }
            }
        }

        return NewBulletInPool(bulletType);
    }

    public Bullet NewBulletInPool(BulletType bulletType)
    {
        Bullet newBullet = Instantiate(bulletPrefab[(int) bulletType], bulletPoolContainer);
        pool.Add(newBullet);
        newBullet.gameObject.SetActive(false);
        

        return newBullet;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
