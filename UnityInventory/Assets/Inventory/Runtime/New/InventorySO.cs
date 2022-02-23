﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.New
{
    public abstract class InventorySO : ScriptableObject
    {
        private HashSet<Action> _onChangedActions = new HashSet<Action>();
        private HashSet<Action<ItemSO, long>> _onChangedDeltaActions = new HashSet<Action<ItemSO, long>>();

        public abstract void AddAmount(ItemSO item, long amount);
        public abstract void RemoveAmount(ItemSO item, long amount);
        public abstract void SetAmount(ItemSO item, long amount);
        public abstract long GetAmount(ItemSO item);
        
        public abstract void SetLimit(ItemSO item, long limit);
        public abstract long GetLimit(ItemSO item);
        
        public void Register(Action action)
        {
            _onChangedActions.Add(action);   
        }

        public void Register(Action<ItemSO, long> action)
        {
            _onChangedDeltaActions.Add(action);   
        }
        
        public void Unregister(Action action)
        {
            _onChangedActions.Add(action);   
        }

        public void Unregister(Action<ItemSO, long> action)
        {
            _onChangedDeltaActions.Add(action);   
        }

        protected void OnChanged(ItemSO item, long delta)
        {
            if (delta == 0)
                return;
            
            foreach (Action<ItemSO, long> action in _onChangedDeltaActions)
            {
                action.Invoke(item, delta);
            }
            
            OnChanged();
        }
        
        private void OnChanged()
        {
            foreach (Action action in _onChangedActions)
            {
                action.Invoke();
            }
        }
    }
}