using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        PlayerManager player;
        public WeaponItem currentWeaponBeingUsed;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            // PERFORM THE ACTION
            weaponAction.AttempToPerformAction(player, weaponPerformingAction);

            // NOTIFY THE SEVER WE HAVE PERFOMED THE ACTION, SO WE PERFORM IT FROM THERE PERSPECTIVE ALSO
        }

        
    }
}