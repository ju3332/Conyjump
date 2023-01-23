using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject fireButton;
    //public Button fireButton;

    // Start is called before the first frame update
    void Start()
    {
        fireButton = GameObject.Find("FireButtonPress");

    }

    // Update is called once per frame
    void Update()
    {
        /*        if(Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }*/

/*        if (fireButton.onClick.AddListener(Shoot))
        {
            Shoot();
        }*/
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
