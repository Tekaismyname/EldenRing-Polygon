using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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
            if (player.IsOwner)
            {
                weaponAction.AttempToPerformAction(player, weaponPerformingAction);

                // NOTIFY THE SEVER WE HAVE PERFOMED THE ACTION, SO WE PERFORM IT FROM THERE PERSPECTIVE ALSO
                player.playerNetworkManager.NotifyTheSeverOfWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId, weaponAction.actionID, weaponPerformingAction.itemID);
            }
        }

        public virtual void DrainStaminaBasedOnAttack()
        {
            if(!player.IsOwner) { return; }

            if (currentWeaponBeingUsed == null) return;

            float staminaDeducted = 0;

            switch (currentAttackType)
            {
                case AttackType.LightAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.lightAttackStaminaCostMultiplier;
                    break;
                default:
                    break;
            }
            Debug.Log("STAMINA DEDUCTED: " + staminaDeducted);
            player.playerNetworkManager.currentStamina.Value -= Mathf.RoundToInt(staminaDeducted);
        }
    }
}