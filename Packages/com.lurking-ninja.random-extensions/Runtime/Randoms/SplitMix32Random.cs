namespace LurkingNinja.RandomExtensions.Randoms
{
    using RandomExtensions;
    using Algorithms;

    /// <summary>
    /// IRandom implementation using splitmix32
    /// </summary>
    public sealed class SplitMix32Random : IRandom
    {
        private readonly SplitMix32 _splitMix = new(0x12345678);

        public void InitState(uint seed) => _splitMix.State = seed;
        public uint NextUInt() => _splitMix.Next();
        public ulong NextULong() => (((ulong)_splitMix.Next()) << 32) | _splitMix.Next();
    }
}