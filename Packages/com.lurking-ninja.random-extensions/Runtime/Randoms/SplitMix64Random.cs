namespace LurkingNinja.RandomExtensions.Randoms
{
    using RandomExtensions;
    using Algorithms;

    /// <summary>
    /// IRandom implementation using splitmix64
    /// </summary>
    public sealed class SplitMix64Random : IRandom
    {
        private readonly SplitMix64 _splitMix = new(0x12345678);

        public void InitState(uint seed) => _splitMix.State = seed;
        public uint NextUInt() => (uint)(_splitMix.Next() >> 32);
        public ulong NextULong() => _splitMix.Next();
    }
}