namespace LurkingNinja.RandomExtensions.Randoms
{
    using Algorithms;

    /// <summary>
    /// IRandom implementation using xorshift32
    /// </summary>
    public sealed class Xorshift32Random : IRandom
    {
        private readonly Xorshift32 _xorshift = new();

        public void InitState(uint seed) => _xorshift.State = seed;
        public uint NextUInt() => _xorshift.Next();
        public ulong NextULong() => (((ulong)_xorshift.Next()) << 32) | _xorshift.Next();
    }
}