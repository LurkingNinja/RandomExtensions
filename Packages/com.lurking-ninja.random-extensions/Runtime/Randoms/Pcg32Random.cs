namespace LurkingNinja.RandomExtensions.Randoms
{
    using RandomExtensions;
    using Algorithms;

    /// <summary>
    /// IRandom implementation using PCG32
    /// </summary>
    public sealed class Pcg32Random : IRandom
    {
        private Pcg32 _pcg = new();

        public void InitState(uint seed)
        {
            _pcg = new Pcg32();
            _pcg.State += seed;
            _pcg.Next();
        }

        public uint NextUInt() => _pcg.Next();

        public ulong NextULong() => (((ulong)_pcg.Next()) << 32) | _pcg.Next();
    }
}