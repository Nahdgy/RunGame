using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chances : MonoBehaviour
{
    public int chances;

    [SerializeField]
    private LayerMask obstacle;
    [SerializeField]
    private bool isInvisible;
    [SerializeField]
    private MeshRenderer coloring;
    [SerializeField]
    private float invicibilityFlashDelay = 0.05f;
    [SerializeField]
    private float invicibilyTimer;
    public IEnumerator InvicibilityFlash()
    {
        while (isInvisible)
        {
            coloring.material.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            coloring.material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }

    }
    public IEnumerator InvicibilityTimer()
    {
        yield return new WaitForSeconds(invicibilyTimer);
        isInvisible = false;
    }

    public void TakeDamagePlayer(int damage)
    {
        if (!isInvisible)
        {
            chances -= damage;

            if (chances <= 0)
            {
                //Die();
                return;
            }
            isInvisible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(InvicibilityTimer());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == obstacle)
        {
            TakeDamagePlayer(1);
        }
    }
}
