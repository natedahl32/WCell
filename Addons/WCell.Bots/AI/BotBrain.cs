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
using WCell.Bots.Entities;
using WCell.Util.Graphics;

namespace WCell.Bots.AI
{
    public class BotBrain : IBrain
    {
        #region Declarations

        /// <summary>
        /// Current state
        /// </summary>
        protected BrainState m_state;

        /// <summary>
        /// Default state
        /// </summary>
        protected BrainState m_defaultState;

        /// <summary>
        /// Owner of the brain
        /// </summary>
        private BotPlayer mOwner;

        protected IAIAction m_currentAction;
        protected IAIActionCollection m_actions;

        protected Vector3 m_SourcePoint;

        #endregion

        #region Constructors

        public BotBrain(BotPlayer owner)
		{
            this.mOwner = owner;
            this.m_actions = new DefaultBotAIActionCollection();
		}

        #endregion

        #region IBrain Implementations

        public BrainState State
        {
            get { return m_state; }
            set 
            {
                if (m_state == value && m_currentAction != null)
                {
                    return;
                }

                if (!mOwner.IsInWorld)
                {
                    m_state = value;
                    return;
                }

                var action = m_actions[value];
                if (action == null)
                {
                    if (m_state != m_defaultState)
                    {
                        m_state = m_defaultState;
                        State = m_defaultState;
                    }
                }
                else
                {
#if DEBUG
                    //m_owner.Say(m_state + " => " + value);
#endif
                    m_state = value;

                    if (m_currentAction != null)
                    {
                        m_currentAction.Stop();
                    }
                    CurrentAction = action;
                }
            }
        }

        public BrainState DefaultState
        {
            get { return m_defaultState; }
            set 
            {
                var shouldEnter = m_defaultState == m_state;
                m_defaultState = value;
                if (shouldEnter)
                {
                    State = value;
                }
            }
        }

        public bool IsAggressive
        {
            get { return false; }   // bots are not aggressive by default
            set { 
                // do nothing 
            }
        }

        public UpdatePriority UpdatePriority
        {
            get { return IsRunning && m_currentAction != null ? m_currentAction.Priority : UpdatePriority.LowPriority; }
        }

        public bool IsRunning
        {
            get { return true; }    // a bots brain is always running
            set
            {
                // do nothing
            }
        }

        public IAIActionCollection Actions
        {
            get { return m_actions; }
        }

        public IAIAction CurrentAction
        {
            get { return m_currentAction; }
            set
            {
                if (m_currentAction != null)
                    m_currentAction.Stop();
                m_currentAction = value;
                if (value != null)
                    m_currentAction.Start();
            }
        }

        public Util.Graphics.Vector3 SourcePoint
        {
            get { return m_SourcePoint; }
            set { m_SourcePoint = value; }
        }

        public void EnterDefaultState()
        {
            State = m_defaultState;
        }

        public void StopCurrentAction()
        {
            if (m_currentAction != null)
                m_currentAction.Stop();
            m_currentAction = null;
        }

        public void Perform()
        {
            // update current Action if any
            if (mOwner.IsUsingSpell)
            {
                return;
            }

            if (m_currentAction == null)
            {
                m_currentAction = m_actions[m_state];
                if (m_currentAction == null)
                {
                    // no Action found for current state
                    State = m_defaultState;
                    return;
                }
                m_currentAction.Start();
            }
            else
            {
                m_currentAction.Update();
            }
        }

        public bool ScanAndAttack()
        {
            // as a bot, we don't do anything here. We don't attack on site, we make decisions.
            return false;
        }

        public bool CheckCombat()
        {
            // A bot is only in combat if they choose to be.
            return (State == BrainState.Combat);
        }

        public void ClearCombat(BrainState newState)
        {
            // TODO: Clear whatever collection we are using to hold NPCs that are in combat with us (NPCs clear ThreatCollection, but we don't have that)
            //if ((m_owner is NPC))
            //{
            //    ((NPC)m_owner).ThreatCollection.Clear();
            //}

            mOwner.IsInCombat = false;
            State = newState;
        }

        public void OnGroupChange(AIGroup newGroup)
        {
            // do nothing
        }

        public void Update(int dt)
        {
            if (!IsRunning) // should NEVER be not running
                return;

            // update current Action if any
            if (mOwner.IsUsingSpell)
                return;

            if (m_currentAction == null)
            {
                m_currentAction = m_actions[m_state];
                if (m_currentAction == null)
                {
                    // no Action found for current state
                    State = m_defaultState;
                    return;
                }
                m_currentAction.Start();
            }
            else
            {
                m_currentAction.Update();
            }
        }

        public void OnEnterCombat()
        {
            
        }

        public void OnDebuff(Unit caster, SpellCast cast, Aura debuff)
        {
            
        }

        public void OnLeaveCombat()
        {
            
        }

        public void OnDamageReceived(IDamageAction action)
        {
            
        }

        public void OnDamageDealt(IDamageAction action)
        {
            
        }

        public void OnHeal(Unit healer, Unit healed, int amtHealed)
        {
            
        }

        public void OnKilled(Unit killerUnit, Unit victimUnit)
        {
            
        }

        public void OnDeath()
        {
            State = BrainState.Dead;
        }

        public void OnCombatTargetOutOfRange()
        {
            
        }

        public void OnActivate()
        {
            if (!m_actions.IsInitialized)
            {
                // execute only on world enter (not on resurrect)
                m_actions.Init(mOwner);
            }
            m_SourcePoint = mOwner.Position;
            CurrentAction = m_actions[m_state];
        }

        public void Dispose()
        {
            mOwner = null;
        }

        #endregion
    }
}
