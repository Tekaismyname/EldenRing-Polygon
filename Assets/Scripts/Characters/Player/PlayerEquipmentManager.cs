using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;
        public WeaponModelInstantiationSlot rightHandSlot;
        public WeaponModelInstantiationSlot leftHandSlot;

        public GameObject rightWeaponModel;
        public GameObject leftWeaponModel;

        protected override void Awake()
        {
            base.Awake();

            // GET OUR SLOTS
            player = GetComponent<PlayerManager>();
            InitializWeaponSLots();
        }

        protected override void Start()
        {
            base.Start();

            LoadWeaponsOnBothHands();
        }
        private void InitializWeaponSLots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlots) 
            { 
                if(weaponSlot.weaponSLot == WeaponModelSlot.RightHand)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSLot == WeaponModelSlot.LeftHand)
                {
                    leftHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponsOnBothHands()
        {
            LoadRightWeapon();
            LoadLeftWeapon();
        }

        public void LoadRightWeapon()
        {
            if(player.playerInventoryManager.currenRightHandWeapon != null)
            {
                rightWeaponModel = Instantiate(player.playerInventoryManager.currenRightHandWeapon.weaponModel);
                rightHandSlot.LoadWeapon(rightWeaponModel);
            }
        }

        public void LoadLeftWeapon()
        {
            if (player.playerInventoryManager.currenLeftHandWeapon != null)
            {
                leftWeaponModel = Instantiate(player.playerInventoryManager.currenLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeapon(leftWeaponModel);
            }
        }
    }
}
