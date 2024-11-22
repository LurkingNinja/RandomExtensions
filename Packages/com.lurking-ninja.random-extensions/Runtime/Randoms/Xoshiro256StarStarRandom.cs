// ReSharper disable UnusedMember.Global
namespace LurkingNinja.RandomExtensions.Randoms
{
    using RandomExtensions;
    using Algorithms;

    /// <summary>
    /// IRandom implementation using xoshiro256**
    /// </summary>
    public sealed class Xoshiro256StarStarRandom : IRandom
    {
        private Xoshiro256StarStar _xoshiro;

        public Xoshiro256StarStarRandom() => _xoshiro = new Xoshiro256StarStar();

        public Xoshiro256StarStarRandom(uint seed) => InitState(seed);

        public Xoshiro256StarStarRandom(uint s0, uint s1, uint s2, uint s3) =>
            _xoshiro = new Xoshiro256StarStar(s0, s1, s2, s3);

        public void InitState(uint seed)
        {
            var s = (ulong)seed;
            
            _xoshiro ??= new Xoshiro256StarStar();
            
            do
            {
                _xoshiro.S0 = SplitMix64.Next(ref s);
                _xoshiro.S1 = SplitMix64.Next(ref s);
                _xoshiro.S2 = SplitMix64.Next(ref s);
                _xoshiro.S3 = SplitMix64.Next(ref s);
            } while (_xoshiro.S0 == 0 && _xoshiro.S1 == 0 && _xoshiro.S2 == 0 && _xoshiro.S3 == 0);
        }

        public uint NextUInt() => (uint)(_xoshiro.Next() >> 32);

        public ulong NextULong() => _xoshiro.Next();

        public void Jump() => _xoshiro.Jump();

        public void LongJump() => _xoshiro.LongJump();

        internal ref Xoshiro256StarStar GetState() => ref _xoshiro;
    }
}