﻿using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public struct Slot : IEquatable<Slot>
    {
        [SerializeField] private int _count;
        [SerializeField] private int _max;
        [SerializeField] private byte _id;

        public int Count => _count;
        public int Max => _max;
        
        public Slot(byte id, int count, int max = InventoryUtility.DEFAULT_MAX)
        {
            _id = id;
            _count = count;
            _max = max;
        }

        public void SetMax(int value)
        {
            if (value < InventoryUtility.DEFAULT_MAX)
                return;
            
            _max = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Slot && Equals((Slot)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _id.GetHashCode();
                hashCode = (hashCode * 397) ^ _count;
                hashCode = (hashCode * 397) ^ _max;
                return hashCode;
            }
        }

        public static bool operator ==(Slot a, Slot b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Slot a, Slot b)
        {
            return !(a == b);
        }

        public bool Equals(Slot other)
        {
            return _id == other._id && Count == other.Count;
        }

        public static Slot operator +(Slot packet, int count)
        {
            return new Slot(packet._id, ClampCount(packet.Count + count), packet._max);
        }

        public static Slot operator *(Slot packet, int multiplier)
        {
            return new Slot(packet._id, ClampCount(packet.Count * multiplier), packet._max);
        }

        public static Slot operator /(Slot packet, int divisor)
        {
            if (divisor == 0)
            {
                return new Slot(packet._id, packet.Count, packet._max);
            }

            return new Slot(packet._id, ClampCount(packet.Count / divisor), packet._max);
        }

        public static Slot operator -(Slot packet, int count)
        {
            return new Slot(packet._id, ClampCount(packet.Count - count), packet._max);
        }

        private static int ClampCount(int unclampedCount)
        {
            return Mathf.Max(unclampedCount, 0);
        }

        public override string ToString()
        {
            InventoryUtility.TryGetItem(_id, out ItemSO item);
            
            return $"({(item == null ? "unknown" : item.ItemName)}: {Count}{(_max > 0 ? $"/{_max}" : "")})";
        }
    }
}