// ReSharper disable UnusedType.Global
namespace LurkingNinja.RandomExtensions.Randoms
{
    using RandomExtensions;
    using Algorithms;

    /// <summary>
    /// IRandom implementation using xorshift128
    /// </summary>
    public sealed class Xorshift128Random : IRandom
    {
        private Xorshift128 _xorshift;

        public Xorshift128Random() => _xorshift = new Xorshift128();
        public Xorshift128Random(uint seed) => InitState(seed);
        public Xorshift128Random(uint s0, uint s1, uint s2, uint s3) => _xorshift = new Xorshift128(s0, s1, s2, s3);

        public void InitState(uint seed)
        {
            _xorshift ??= new Xorshift128();
            
            do
            {
                _xorshift.S0 = SplitMix32.Next(ref seed);
                _xorshift.S1 = SplitMix32.Next(ref seed);
                _xorshift.S2 = SplitMix32.Next(ref seed);
                _xorshift.S3 = SplitMix32.Next(ref seed);
            } while (_xorshift.S0 == 0 || _xorshift.S1 == 0 || _xorshift.S2 == 0 || _xorshift.S3 == 0);
        }

        public uint NextUInt() => _xorshift.Next();
        public ulong NextULong() => (((ulong)_xorshift.Next()) << 32) | _xorshift.Next();
    }
}