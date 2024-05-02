using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponController : MonoBehaviour
{
    [Header("References")]
    public Transform weaponMuzzle;

    [Header("General")]
    public LayerMask hittableLayers;
    public GameObject bulletHolePrefab;

    [Header("Shoot Parameters")]
    public float fireRange = 200;
    public float recoilForce = 4f; // Fuerza de retroceso del arma
    public float fireRate = 0.5f; // Cadencia de disparo en segundos

    [Header("Sounds & Visuals")]
    public GameObject flashEffect;

    private Transform cameraPlayerTransform;
    private Vector3 originalPosition; // Guarda la posición original del arma
    private bool canShoot = true; // Indica si se puede disparar
    private float nextFireTime = 0f; // Siguiente tiempo en que se puede disparar

    private void Start()
    {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        originalPosition = transform.localPosition; // Guarda la posición original al inicio
    }

    private bool isAKActive = false;
private bool isAwpActive = false;

private void Update()
{
    HandleShoot();

    // Restaura la posición original utilizando Lerp
    transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5f);

    if (Input.GetKeyDown(KeyCode.Alpha1) && !isAKActive) // Verifica si AK no está activo para evitar repeticiones
    {
        isAKActive = true;
        isAwpActive = false;

        akON.gameObject.SetActive(true);
        awpOFF.gameObject.SetActive(true);
        akOFF.gameObject.SetActive(false);
        awpON.gameObject.SetActive(false);
    }
    else if (Input.GetKeyDown(KeyCode.Alpha2) && !isAwpActive) // Verifica si AWP no está activo para evitar repeticiones
    {
        isAKActive = false;
        isAwpActive = true;

        akON.gameObject.SetActive(false);
        awpOFF.gameObject.SetActive(false);
        akOFF.gameObject.SetActive(true);
        awpON.gameObject.SetActive(true);
    }
}

    private void HandleShoot()
    {
        if (Input.GetButtonDown("Fire1") && canShoot && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Shoot()
    {
        GameObject flashClone = Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
        Destroy(flashClone, 1f);

        AddRecoil();

        RaycastHit hit;
        if (Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, fireRange, hittableLayers))
        {
            GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
            Destroy(bulletHoleClone, 4f);
        }

        StartCoroutine(ShootCooldown());
    }

    private void AddRecoil()
    {
        transform.Rotate(-recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (recoilForce / 50f);
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.4f)); // Personalizar el cooldown aquí
        canShoot = true;
    }

    public RawImage akOFF;
    public RawImage akON;
    public RawImage awpOFF;
    public RawImage awpON;



}
