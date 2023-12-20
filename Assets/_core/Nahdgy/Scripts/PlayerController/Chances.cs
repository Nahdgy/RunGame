using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chances : MonoBehaviour
{
    public int chances;

    [SerializeField]
    private float endFOV;
    [SerializeField]
    private int obstacle, winZone;
    [SerializeField]
    private bool isInvisible;
    [SerializeField]
    private GameObject coloring;
    [SerializeField]
    private float invicibilityFlashDelay = 0.05f;
    [SerializeField]
    private float invicibilyTimer;
    private GameObject collideObject;
    private CinemachineVirtualCamera playerCamera;

    [SerializeField]
    private UIManager Ui;

    private void Awake()
    {
        playerCamera = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();
    }
    public IEnumerator InvicibilityFlash()
    {
        while (isInvisible)
        {
            coloring.SetActive(false);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            coloring.SetActive(true);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            coloring.SetActive(false);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            coloring.SetActive(true);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }

    }
    public IEnumerator InvicibilityTimer()
    {   
        yield return new WaitForSeconds(invicibilyTimer);
        isInvisible = false;
    }
    IEnumerator ChangeFOV(CinemachineVirtualCamera cam, float duration)
    {
        float startFOV = cam.m_Lens.FieldOfView;
        endFOV = startFOV + 20;
        float time = 0;
        while (time < duration)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, endFOV, time / duration);
            yield return null;
            time += Time.deltaTime;
        }
    }

    public void TakeDamagePlayer(int damage)
    {
        if (!isInvisible)
        {
            chances -= damage;
            //UnZoom the camera
            StartCoroutine(ChangeFOV(playerCamera, 1f));

            if (chances <= 0)
            {
                Ui.GameOver();
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
            Debug.Log("Paw");
            collideObject = collision.gameObject;
            Physics.IgnoreCollision(collideObject.GetComponent<Collider>(), GetComponent<Collider>());  
            TakeDamagePlayer(1);
          
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.layer == obstacle))
        {
            Ui.Win();
        }
    }
}
