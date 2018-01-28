using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    Renderer cachedRenderer;
    Rigidbody m_rigidibody;

    [Range(0, 10)] public float sicknessTime = 30;
    public float startingSicknessTime;
    public bool infected;
    public bool dead;


    void Start()
    {

        startingSicknessTime = sicknessTime;
        cachedRenderer = GetComponent<Renderer>();
        cachedRenderer.material.color = Color.yellow;

    }

    void Update()
    {

        if (infected & !dead)
        {
            DyingOfSickness();
        }

    }

    public void DyingOfSickness()
    {

        sicknessTime -= Time.deltaTime;
        if (sicknessTime <= 0)
        {
            Dead();
        }

    }


    public void Dead()
    {
        cachedRenderer.material.color = Color.black;

        infected = false;
        dead = true;
    }

    public void Heal()
    {
        if (!dead)
        {
            cachedRenderer.material.color = Color.yellow;
            sicknessTime = startingSicknessTime;
            infected = false;
        }
    }

    public void Infected()
    {
        cachedRenderer.material.color = Color.green;
        infected = true;

    }
}
