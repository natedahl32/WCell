using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCell.RealmServer.AI;
using WCell.RealmServer.AI.Actions;
using WCell.RealmServer.AI.Brains;
using WCell.RealmServer.AI.Groups;
using WCell.RealmServer.Entities;
using WCell.RealmServer.Spells;
using WCell.RealmServer.Spells.Auras;
using WCell.RealmServer.Misc;
using WCell.Constants.Updates;

namespace WCell.Bots.AI
{
    public class BotBrain : IBrain
    {
        #region Declarations

        private readonly Character mOwner;

        #endregion

        #region Constructors

        public BotBrain(Character owner)
		{
            this.mOwner = owner;
		}

        #endregion

        #region IBrain Implementations

        public BrainState State
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public BrainState DefaultState
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAggressive
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public UpdatePriority UpdatePriority
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsRunning
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IAIActionCollection Actions
        {
            get { throw new NotImplementedException(); }
        }

        public IAIAction CurrentAction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Util.Graphics.Vector3 SourcePoint
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void EnterDefaultState()
        {
            throw new NotImplementedException();
        }

        public void StopCurrentAction()
        {
            throw new NotImplementedException();
        }

        public void Perform()
        {
            throw new NotImplementedException();
        }

        public bool ScanAndAttack()
        {
            throw new NotImplementedException();
        }

        public bool CheckCombat()
        {
            throw new NotImplementedException();
        }

        public void ClearCombat(BrainState newState)
        {
            throw new NotImplementedException();
        }

        public void OnGroupChange(AIGroup newGroup)
        {
            throw new NotImplementedException();
        }

        public void Update(int dt)
        {
            // do nothing
            var i = 0;
        }

        public void OnEnterCombat()
        {
            throw new NotImplementedException();
        }

        public void OnDebuff(Unit caster, SpellCast cast, Aura debuff)
        {
            throw new NotImplementedException();
        }

        public void OnLeaveCombat()
        {
            throw new NotImplementedException();
        }

        public void OnDamageReceived(IDamageAction action)
        {
            throw new NotImplementedException();
        }

        public void OnDamageDealt(IDamageAction action)
        {
            throw new NotImplementedException();
        }

        public void OnHeal(Unit healer, Unit healed, int amtHealed)
        {
            throw new NotImplementedException();
        }

        public void OnKilled(Unit killerUnit, Unit victimUnit)
        {
            throw new NotImplementedException();
        }

        public void OnDeath()
        {
            throw new NotImplementedException();
        }

        public void OnCombatTargetOutOfRange()
        {
            throw new NotImplementedException();
        }

        public void OnActivate()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
