namespace LurkingNinja.RandomExtensions.Randoms
{
    using RandomExtensions;
    using Algorithms;

    /// <summary>
    /// IRandom implementation using xorshift64
    /// </summary>
    public sealed class Xorshift64Random : IRandom
    {
        private readonly Xorshift64 _xorshift = new();

        public void InitState(uint seed) => _xorshift.State = seed;

        public uint NextUInt() => (uint)(_xorshift.Next() >> 32);
        public ulong NextULong() => _xorshift.Next();
    }
}