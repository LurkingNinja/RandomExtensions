#nullable enable
namespace LurkingNinja.RandomExtensions
{
    using System;
    using Randoms;

    public static partial class RandomEx
    {
        [ThreadStatic] private static System.Random? _seedGenerator;
        private static Random SeedGenerator => _seedGenerator ??= new Random();

        /// <summary>
        /// Provides a thread-safe IRandom instance that may be used concurrently from any thread.
        /// </summary>
        public static IRandom Shared { get; } = new SharedRandom();

        /// <summary>
        /// Creates an IRandom instance initialized with a random seed.
        /// </summary>
        public static IRandom Create() => new Xoshiro256StarStarRandom((uint)SeedGenerator.Next());
    }

    internal sealed class SharedRandom : IRandom
    {
        [ThreadStatic] private static IRandom? _random;
        private static IRandom LocalRandom => _random ?? Create();

        private static IRandom Create() => _random = RandomEx.Create();

        public void InitState(uint seed) => LocalRandom.InitState(seed);

        public uint NextUInt() => LocalRandom.NextUInt();

        public ulong NextULong() => LocalRandom.NextULong();

        internal static IRandom GetLocalInstance() => LocalRandom;
    }
}