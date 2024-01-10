using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    RandomCalculations randCalc;
    // Start is called before the first frame update
    void Start()
    {
        //Creating player OBJECT
        Player myPlayer = new Player();

        //Creating Enemy OBJECT
        Enemy meleeEnemy = new Enemy(); //INSTANCES = COPIES of the enemies
        Enemy shooterEnemy = new Enemy();

        //Create weapon OBJECTS
        Weapon gun1 = new Weapon();
        Weapon machineGun = new Weapon();
        //Weapon meleeWeapon = new Weapon("Melee Weapon", 5f);

        myPlayer.weapon = gun1;
        shooterEnemy.weapon = machineGun;
        //meleeEnemy.weapon = meleeWeapon;

        RandomCalculations.randNum = 5;
        randCalc.lenght = 5f;
        RandomCalculations.CalculateRandNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
