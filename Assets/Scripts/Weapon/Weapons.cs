using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        



    }


    public enum WeaponType
    {
        REVOLVER,
        KATANA
    }


    // Update is called once per frame
    void Update()
    {
        
        //Cycle Weapon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int weaponNumber = (int)weaponType;
            weaponNumber++;
            weaponNumber %= (int)weaponType.COUNT;
            weaponType

        }







    }


    void ShootRifle()


}
