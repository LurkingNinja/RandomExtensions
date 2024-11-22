// ReSharper disable UnusedMember.Global
namespace LurkingNinja.RandomExtensions.Collections
{
    using System;
    using System.Collections.Generic;

    public readonly struct WeightedValue<T> : IEquatable<WeightedValue<T>>
    {
        public readonly T Value;
        public readonly double Weight;
        
        public WeightedValue(T value, double weight)
        {
            Value = value;
            Weight = weight;
        }

        public bool Equals(WeightedValue<T> other) =>
            EqualityComparer<T>.Default.Equals(Value, other.Value) && Weight.Equals(other.Weight);

        public override bool Equals(object obj) => obj is WeightedValue<T> other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Value, Weight);
    }

    internal sealed class WeightedValueEqualityComparer<T> : IEqualityComparer<WeightedValue<T>>
    {
        public static readonly WeightedValueEqualityComparer<T> INSTANCE = new();

        public bool Equals(WeightedValue<T> x, WeightedValue<T> y) =>
            EqualityComparer<T>.Default.Equals(x.Value, y.Value);

        public int GetHashCode(WeightedValue<T> obj) =>
            EqualityComparer<T>.Default.GetHashCode(obj.Value!);
    }
}