namespace LurkingNinja.RandomExtensions.Collections
{
    using System;
    using System.Collections.Generic;
    using RandomExtensions;

    public interface IWeightedCollection<T> : IReadOnlyCollection<WeightedValue<T>>
    {
        T GetItem(IRandom random);
        void GetItems(IRandom random, Span<T> destination);
    }
}