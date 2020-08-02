using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is a base script for weapons in the game.
/// It holds main functionalities, such as firing, and properties that each weapon will have.
/// Worked by: Nikodem Hamrol
/// References: 
/// Brackeys. (2017). Shooting with Raycasts - Unity Tutorial [online]. Available: https://www.youtube.com/watch?v=THnivyG0Mvo [Last Accessed 24th June 2020].
/// Gabriel Aguiar Prod. (2018). Unity 2018 - Game VFX - Projectile/Bullet Raycast Tutorial [online]. Available: https://www.youtube.com/watch?v=xenW67bXTgM [Last Accessed 9th June 2020].
/// Swindle Creative. (2019). Weapon Recoil Script - How To Make Procedural Recoil In Unity [online]. Available: https://www.youtube.com/watch?v=6hyQ2rPkMDY [Last Accessed 19th June 2020].
/// </summary>

public class WeaponBase : MonoBehaviour
{
    //Private fields
    //Boolean fields
    private bool m_isWeaponReloading; //This will check if the weapon is currently in the reloading state, which will prevent the weapon from firing
    private bool IsFiring; //This will check if the weapon is currently firing, which will prevent reloading from happening in the middle of firing a weapon

    //This float field that times the fire rate of a weapon
    private float m_fireTime;

    //This integer field to get the difference between ammo and clip size to have conservative ammo
    private int m_ammoDifference;

    //This Vector3 field is the rotation of the weapons' roation point, which is used to rotate the wepon itself with the current weapon rotation protected field
    private Vector3 m_weaponRotation;

    //Protected fields that will be used in child classes
    //Vector3 protected fields
    protected Vector3 m_weaponCurrentPosition; //This is the weapon postion point's current position, which is used to set the kick back and return it back to its original position
    protected Vector3 m_weaponRotationalRecoil; //This is the weapon rotational point's current rotation, which is used to set the rotate the point when fired and return it back to its original position

    //This is a reference to the sound script, in order to play sound effects
    protected SoundFX m_soundReference;

    //Weapon properties
    [Header("Ammunition properties")]
    [Tooltip("The current ammo the weapon has")]
    public int CurrentAmmo; //Amount of ammo in the clip
    [Tooltip("The amount of amount the weapon is allowed to have per clip")]
    public int WeaponClipSize; //Weapon's clip size
    [Tooltip("The maximum amount of ammo the weapon can have in total")]
    public int MaxAmmo; //Maximum ammo the weapon will have

    [Header("Fire Rates and Fire modes properties")]
    [Tooltip("Is the weapon going to be automatic?")]
    public bool IsAutomatic; //This will check if the weapon is automatic or semi-automatic
    [Tooltip("Is the weapon going to use hit scans?")]
    public bool UseHitscan; //This will check if the weapon will be using raycasts, or physical bullets
    [Tooltip("The fire rate that weapon will fire at. This only applies to weapons that are automatic")]
    public float FireRate; //Weapon fire rate
    [Tooltip("The damage the weapon will do. This only applies to weapons using hit scans")]
    public float WeaponDamage; //Weapon Damage

    [Header("Recoil properties")]
    [Tooltip("This is the position point of the weapon, which is attached to the Weapon Holder of the player")]
    public Transform WeaponPositionPoint; //The position point is the position of the weapon, which will be used to apply weapon kick back when fired, without moving the weapon itself
    [Tooltip("This is the rotation point of the weapon, which is attached to the Weapon Holder of the player")]
    public Transform WeaponRotationPoint; //The rotation point is the rotation of the weapon, which will be used to apply recoil rotation when fired, without rotating the weapon itself
    [Tooltip("This is the speed of the weapon kick back when fired, so changing recoil position")]
    public float PositionalRecoilSpeed; //The speed of the weapon kick back when fired
    [Tooltip("This is the speed of the weapon moving its rotation when fired, so recoil rotation")]
    public float RotationalRecoilSpeed; //The speed of the weapon rotating when fired
    [Tooltip("This is the speed of the weapon returning back to its original position after the weapon was fired")]
    public float PositionalReturnSpeed; //The speed of the weapon returning to its original position after it was fired
    [Tooltip("This is the speed of the weapon returning back to its original rotation after the weapon was fired")]
    public float RotationalReturnSpeed; //The speed of the weapon returning to its original rotation after it was fired
    [Tooltip("This is the maximum range the weapon position point will kick back")]
    public Vector3 RecoilKickBackMaxRange; //The max rotation range of the kick back will be used to set the kick back of the weapon between the max negative vlaue and max positive value
    [Tooltip("This is the maximum range the weapon rotation point will rotate")]
    public Vector3 RecoilRotationMaxRange; //The max rotation range of the weapon recoil will be used to set the rotation of the weapon between the max negative vlaue and max positive value

    [Header("Other Weapon Properties")]
    [Tooltip("The Animator component that's attached to the weapon")]
    public Animator WeaponAnimator; //This is the animator that will change between the weapon idle state and the reloading animation mainly in theis script
    [Tooltip("The muzzle object that's child to the weapon itself")]
    public GameObject WeaponMuzzle; //Reference to the weapon muzzle that will be used to get the position of where the projectile will spawn

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Get the sound effect reference, b finding the object that contains that script
        m_soundReference = FindObjectOfType<SoundFX>();

        //Set current ammo as clip size and get reference, only if the player is not in the tutorial scene
        if (!TutorialController.InTutorialScene)
        {
            CurrentAmmo = WeaponClipSize;
        }
    }

    //When the weapon is enabled, stop the animation in case if its already relaoding and set the reloading to false, which will return it back to its original position
    void OnEnable()
    {
        WeaponAnimator.SetBool("Reloading", false);
        m_isWeaponReloading = false;
    }

    // Fixed update called every fixed frame. It's only used for setting up recoil
    void FixedUpdate()
    {
        //Setting the current position point and current rotation point of the weapon with a lerp, which will return the current position and current rotation back to 0, which only takes affect after firing
        m_weaponRotationalRecoil = Vector3.Lerp(m_weaponRotationalRecoil, Vector3.zero, RotationalReturnSpeed * Time.fixedDeltaTime);
        m_weaponCurrentPosition = Vector3.Lerp(m_weaponCurrentPosition, Vector3.zero, PositionalReturnSpeed * Time.fixedDeltaTime);

        //Setting the current position point and current rotation point of the weapon with a slerp, which will move the current position and rotation spherically.
        //This helps those current transforms to move in the correct movement and at constant rate as well
        WeaponPositionPoint.localPosition = Vector3.Slerp(WeaponPositionPoint.localPosition, m_weaponCurrentPosition, PositionalRecoilSpeed * Time.fixedDeltaTime);
        m_weaponRotation = Vector3.Slerp(m_weaponRotation, m_weaponRotationalRecoil, RotationalRecoilSpeed * Time.fixedDeltaTime);

        //This will rorate the weapon's roatation point, which will be the weapon itself
        WeaponRotationPoint.localRotation = Quaternion.Euler(m_weaponRotation);
    }

    // Update is called once per frame
    void Update()
    {
        //If the controls are enebled, then player can shoot and reload
        if (PlayerController.ControlsEnabled == true)
        {
            //Run the shooting function
            PlayerShooting();
            WeaponReload();
        }
    }

    //This function handles when the player shoots their weapon
    protected virtual void PlayerShooting()
    {
        //If there is ammo left and the game is not paused, and the weapon is not currently reloading
        if (CurrentAmmo > 0 && !PauseMenu.IsGamePaused && m_isWeaponReloading == false)
        {
            ///Automatic weapon solution
            //If player holds down the left mouse button and if it is time to fire and if the player is not reloading
            if (Input.GetButton("Fire1") && Time.time >= m_fireTime && IsAutomatic == true)
            {
                //The weapon is firing and play the shooting sound
                IsFiring = true;
                m_soundReference.PrimaryFire.Play();

                //Decrement the ammo by 1
                CurrentAmmo -= 1;
                
                //Initiate the raycast and set the fire timer
                HitDetection();
                m_fireTime = Time.time + 1 / FireRate;

                //Move weapon for recoil, which applies the kick back and the rotation
                m_weaponRotationalRecoil += new Vector3(-RecoilRotationMaxRange.x, Random.Range(-RecoilRotationMaxRange.y, RecoilRotationMaxRange.y), Random.Range(-RecoilRotationMaxRange.z, RecoilRotationMaxRange.z));
                m_weaponCurrentPosition += new Vector3(Random.Range(-RecoilKickBackMaxRange.x, RecoilKickBackMaxRange.x), Random.Range(-RecoilKickBackMaxRange.y, RecoilKickBackMaxRange.y), RecoilKickBackMaxRange.z);
            }

            ///Semi-automatic weapon solution
            //If the player presses the left mouse button and if the player is not reloading
            if (Input.GetButtonDown("Fire1") && IsAutomatic == false)
            {
                //The weapon is firing and play the shooting sound
                IsFiring = true;
                m_soundReference.PrimaryFire.Play();

                //Decrement the ammo by 1
                CurrentAmmo -= 1;

                //Initiate a raycast
                HitDetection();

                //Move weapon for recoil, which applies the kick back and the rotation
                m_weaponRotationalRecoil += new Vector3(-RecoilRotationMaxRange.x, Random.Range(-RecoilRotationMaxRange.y, RecoilRotationMaxRange.y), Random.Range(-RecoilRotationMaxRange.z, RecoilRotationMaxRange.z));
                m_weaponCurrentPosition += new Vector3(Random.Range(-RecoilKickBackMaxRange.x, RecoilKickBackMaxRange.x), Random.Range(-RecoilKickBackMaxRange.y, RecoilKickBackMaxRange.y), RecoilKickBackMaxRange.z);
            }
        }

        //If the player has let go of the left mouse button for fire, it will set the firing to false, which the player can reload
        if (Input.GetButtonUp("Fire1"))
        {
            IsFiring = false;
        }
    }

    //This function handles the reloading of a weapon
    private void WeaponReload()
    {
        //If R key is pressed and if player is not already reloading
        if (Input.GetKeyDown(KeyCode.R) && IsFiring == false && MaxAmmo != 0)
        {
            //If current clip is not full
            if (CurrentAmmo < WeaponClipSize)
            {
                StartCoroutine(Reloading());
                //Set ammo difference, by subtracting the clip size by ammo in clip
                m_ammoDifference = WeaponClipSize - CurrentAmmo;

                //If there's more ammo left
                if (MaxAmmo >= m_ammoDifference)
                {
                    CurrentAmmo += m_ammoDifference; //Add the ammo difference to the current ammo, so that it's not bigger than the clip size
                    MaxAmmo -= m_ammoDifference; //Subtract max ammo by the ammo difference
                }
                else //If there's no ammo left
                {
                    //Add the remaining ammo and set max ammo to 0
                    CurrentAmmo += MaxAmmo;
                    MaxAmmo = 0;
                }
            }
        }
    }

    //This coroutine is for when the player is relaoding
    private IEnumerator Reloading()
    {        
        //Start the reload animation
        WeaponAnimator.SetBool("Reloading", true);

        //Wait until this amount of seconds, which a little less than the animation duration
        yield return new WaitForSeconds(0.52f);
        //Set reloading to true, stop the animation, which will return it to idle, play the reloading sound and set the wpeon is realodin bollean to false, so the player can fire again
        m_isWeaponReloading = true;
        WeaponAnimator.SetBool("Reloading", false);
        m_soundReference.PrimaryHandling.Play();
        m_isWeaponReloading = false;
    }

    //This function handles the raycast initiation
    private void HitDetection()
    {
        //Initiate raycast from the weapon muzzle and point it forward and get the information on the object hit from 
        if (Physics.Raycast(WeaponMuzzle.transform.position, WeaponMuzzle.transform.forward, out RaycastHit m_raycastHitDetector, 100) && UseHitscan == true)
        {
            //Set the enemy for the raycast should mainly detect
            EnemyBase enemyTarget = m_raycastHitDetector.transform.GetComponent<EnemyBase>();

            //If the enemy target exists, then apply damage
            if (enemyTarget != null)
            {
                enemyTarget.EnemyDamaged(WeaponDamage, 0);
            }
        }
    }
}
