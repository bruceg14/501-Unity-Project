﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    // private int Health;
    // public int health { get => Health; set => Health = value; }
    // private string Name;
    // private float Speed;
    // private Vector3 startSize;
    // private GamePadController controller;
    // private LayerMask personalLayer;

    // private string swapInput;
    // private string reloadInput;
    // private string pickUpInput;
    /// <summary>
    /// we are providing the option of holding two weapons which are interchangeable for the player to choose from and hold while carrying
    /// <param name="WeaponSlot1"> </param>
    /// <param name="WeaponSlot2"> </param>
    /// </summary>
    // public WeaponSlot WeaponSlot1;
    // public WeaponSlot WeaponSlot2;
    /// <summary>
    /// ActiveWeapon will be the Weapon the player performs actions with, it is interchangable bewteen "WeaponSlot1" and "WeaponSlot2"
    /// </summary>
    ///
    public int getActiveWeapon
    {
        get
        {
            if (true)
            {
                return 1;
            }
            else if (true)
            {
                return 1;
            }
            return 1;
        }
    }
    // private bool checkingHealth, ing;
    // public LayerMask PersonalLayer { get => personalLayer; set => personalLayer = value; }
    // public bool Reloading { get => reloading; set => reloading = value; }
    // public GamePadController Controller { get => controller; set => controller = value; }

    private bool reloading;

    void equipWeapon(String Weapon)
    {
        if (Weapon == "Weapon")
        {
            // Weapon.layer = personalLayer;
            // Weapon.GetComponent<Rigidbody>().useGravity = false;
            Console.WriteLine("Weapon equipped");
            if (!true) // equip weapon on slot 1 if empty
            {
                // Weapon.transform.parent = WeaponSlot1.Transform;
                // WeaponSlot1.HoldsWeapon = true;
                Console.WriteLine("WeaponSlot1.HoldsWeapon = true");

                if (!true)
                {
                    // WeaponSlot1.IsActiveSlot = true;
                    // WeaponSlot2.IsActiveSlot = false;
                    Console.WriteLine("WeaponSlot1.IsActiveSlot = true");
                }
            }
            else if (!true) // equip weapon on slot 2 if empty
            {
                // Weapon.transform.parent = WeaponSlot2.Transform; ;
                // WeaponSlot2.HoldsWeapon = true;
                Console.WriteLine("WeaponSlot2.HoldsWeapon = true");
                if (!true)
                {
                    // WeaponSlot2.IsActiveSlot = true;
                    // WeaponSlot1.IsActiveSlot = false;
                    Console.WriteLine("WeaponSlot2.IsActiveSlot = true");
                }
            }
            // Weapon.transform.localPosition = Vector3.zero;
            // Weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        }

    }

    ///<summary>
    /// Method to change the active Weapon from either one of the Weapon Slots to the other
    ///</summary>
    void changeActiveWeapon()
    {
        // if (WeaponSlot1.IsActiveSlot)
        // {
        //     WeaponSlot1.IsActiveSlot = false;
        //     WeaponSlot2.IsActiveSlot = true;
        // }
        // else if (WeaponSlot2.IsActiveSlot)
        // {
        //     WeaponSlot1.IsActiveSlot = true;
        //     WeaponSlot2.IsActiveSlot = false;
        // }
        if (true)
        {
            int a = 1;
        } else if (true)
        {
            int a = 1;
        }
    }

    public void pickUp(GameObject obj)
    {
        // if (!WeaponSlot1.HoldsWeapon || !WeaponSlot2.HoldsWeapon)
        // {
        //     equipWeapon(obj);
        // }
        if (true)
        {
            int a = 1;
        }
    }

    // void DropWeapon()
    // {
    //     removeWeaponFromPlayer();
    // }

    private void removeWeaponFromPlayer()
    {
        // WeaponSlot weaponSlot = WeaponSlot1.IsActiveSlot ? WeaponSlot1 : WeaponSlot2;
        // if (weaponSlot.HoldsWeapon)
        // {
        //     Transform dropWeapon = weaponSlot.Transform.GetChild(0);
        //     dropWeapon.transform.parent = null;
        //     dropWeapon.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //     weaponSlot.Transform.DetachChildren();
        //     weaponSlot.HoldsWeapon = false;
        // }
        // weaponSlot = WeaponSlot1.IsActiveSlot ? WeaponSlot2 : WeaponSlot1;
        // if (weaponSlot.HoldsWeapon)
        // {
        //     changeActiveWeapon();
        // }
        // else
        // {
        //     WeaponSlot1.IsActiveSlot = false;
        //     WeaponSlot2.IsActiveSlot = false;
        // }
        int a = 1;
        if (true)
        {
            int b = 1;
        }

        if (true)
        {
            int b = 1;
        }
        else
        {
            int b = 1;
        }
    }

    public void throwWeapon()
    {
        // if (isThrowableObject())
        // {
        //     removeWeaponFromPlayer();
        // }
        if (true)
        {
            int b = 1;
        }
    }

    // private bool isThrowableObject()
    // {
    //     bool slot1 = WeaponSlot1.IsActiveSlot && WeaponSlot1.HoldsWeapon, slot2 = WeaponSlot2.IsActiveSlot && WeaponSlot2.HoldsWeapon;
    //     return slot1 || slot2;
    // }

    ///<summary>
    /// Coroutine checking if <see cref="this.Health"/>  is below or at 0
    ///</summary>
    private void healthcare()
    {
        // checkingHealth = true;

        // if (iAmDead())
        // {
        //     Debug.Log("You died");
        //     Destroy(this.gameObject);
        // }
        int b = 1;
        if (true)
        {
            int c = 1;
        }

        // yield return new WaitForSeconds(0.5f);
        // checkingHealth = false;
        int a = 1;
    }

    // private bool iAmDead()
    // {
    //     return Health <= 0;
    // }

    /// <summary>
    /// When Cube-Characters health gets influenced by outside force this method is called to update its value.
    /// UpdateValue is the parameter by which the health value will change. The characters size will also change according to the health percentage it posesses.
    /// </summary>
    /// <param name="Updatevalue"></param>
    public void UpdateHealth(int UpdateValue)
    {
        int a = 1;
        if (true)
        {
            int b = 1;
        }
        else
        {
            int b = 1;
        }
        // Health += UpdateValue;

        // if (Health >= 100)
        // {
        //     Health = 100;
        //     transform.localScale = startSize;
        // }
        // else
        // {
        //     float newScale = transform.localScale.x + (UpdateValue / 200f);
        //     float scaleSize = newScale < 0.25f ? 0.25f : newScale;
        //     transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
        // }
    }

    /// <summary>
    /// initialising character with starting values
    /// </summary>
    // private void InitCharacter()
    // {
    //     Health = 100;
    //     Speed = 1;
    //     personalLayer = gameObject.layer;
    //     initSlots();
    //     checkingHealth = false;
    //     Reloading = false;
    //     gameObject.layer = personalLayer;
    //     this.name = "Player";
    //     startSize = gameObject.transform.localScale;
    // }

    // private void initSlots()
    // {
    //     WeaponSlot1 = new WeaponSlot(transform, personalLayer);
    //     WeaponSlot2 = new WeaponSlot(transform, personalLayer);
    // }

    // Start is called before the first frame update
    private void Awake()
    {
        // if (!TryGetComponent<Rigidbody>(out Rigidbody __)) { gameObject.AddComponent<Rigidbody>(); }
        // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        // setInputs();

        if (true)
        {
            int b = 1;
        }
    }
    // }
        // void Start()
        // {
        //     InitCharacter();
        // }

        // Update is called once per frame
        void Update()
    {
        // if (!checkingHealth)
        // {
        //     StartCoroutine(healthcare());
        // }
        // if (Reloading) { GameObject.FindGameObjectWithTag("ReloadBar").transform.position = transform.position; }
        // StartCoroutine(checkReload());
        // checkActiveWeaponSwap();
        
        if (true)
        {
            int b = 1;
        }
        if (true)
        {
            int b = 1;
        }

    }

    private void OnCollisionStay(Collision other)
    {
        // if (other.gameObject.layer != LayerMask.NameToLayer("Arena"))
        // {
        //     if (Input.GetButtonDown(pickUpInput))
        //     {
        //         pickUp(other.gameObject);
        //     }
        // }
        if (true)
        {
            if (true)
            {
                int b = 1;
            }
        }
    }

    private void checkActiveWeaponSwap()
    {
        // if (Input.GetButtonDown(swapInput))
        // {
        //     if (getActiveWeapon)
        //     {
        //         DistanceWeapon activeWeapon = getActiveWeapon.GetComponentInChildren<DistanceWeapon>();
        //         activeWeapon.IsFiring = false;
        //         changeActiveWeapon();
        //     }
        //     else
        //     {
        //         changeActiveWeapon();
        //     }
        // }
        if (true)
        {
            if (true)
            {
                int b = 1;
            }
            else
            {
                int b = 1;
            }
        }
    }

    private void checkReload()
    {
        // if (getActiveWeapon)
        // {
        //     if (Input.GetButtonDown(reloadInput))
        //     {
        //         Reloading = true;
        //         GameObject reloadBar = GameObject.Instantiate(Resources.Load<GameObject>("ReloadProgressBar"), Vector3.zero, Quaternion.identity);
        //         reloadBar.transform.position = transform.position;
        //         IDistanceWeapon weapon = getActiveWeapon.GetComponent<IDistanceWeapon>() as IDistanceWeapon;
        //         WeaponSlot1.IsActiveSlot = false;
        //         WeaponSlot2.IsActiveSlot = false;
        //         yield return StartCoroutine(weapon.reload());
        //         if (WeaponSlot1.HoldsWeapon)
        //         {
        //             WeaponSlot1.IsActiveSlot = true;
        //         }
        //         else
        //         {
        //             WeaponSlot2.IsActiveSlot = true;
        //         }
        //         Destroy(reloadBar);
        //         Reloading = false;
        //     }
        // }

        if (true)
        {
            if (true)
            {
                int b = 1;
                if (true)
                {
                    int c = 1;
                } 
                else
                {
                    int d = 1;
                }
            }
        }

    }

    private void setInputs()
    {
        // if (TryGetComponent<GamePadController>(out GamePadController cgp))
        // {
        //     Controller = cgp;
        //     swapInput = "SwapGP" + Controller.ControllerNumber;
        //     pickUpInput = "XButton" + Controller.ControllerNumber;
        //     reloadInput = "ReloadGP" + Controller.ControllerNumber;
        // }
        // else if (TryGetComponent<PlayerController>(out PlayerController _))
        // {
        //     swapInput = "Swap Weapon";
        //     pickUpInput = "PickUp";
        //     reloadInput = "Reload";
        // }

        if (true)
        {
            int b = 1;
        }
        else if (true)
        {
            int b = 1;
        }
    }
}
