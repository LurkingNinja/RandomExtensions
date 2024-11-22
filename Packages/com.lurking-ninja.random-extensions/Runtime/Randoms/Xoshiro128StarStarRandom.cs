// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace LurkingNinja.RandomExtensions.Randoms
{
    using RandomExtensions;
    using Algorithms;

    /// <summary>
    /// IRandom implementation using xoshiro128**
    /// </summary>
    public sealed class Xoshiro128StarStarRandom : IRandom
    {
        private Xoshiro128StarStar _xoshiro;

        public Xoshiro128StarStarRandom() => _xoshiro = new Xoshiro128StarStar();
        public Xoshiro128StarStarRandom(uint seed) => InitState(seed);
        public Xoshiro128StarStarRandom(uint s0, uint s1, uint s2, uint s3) =>
            _xoshiro = new Xoshiro128StarStar(s0, s1, s2, s3);

        public void InitState(uint seed)
        {
            _xoshiro ??= new Xoshiro128StarStar();
            
            do
            {
                _xoshiro.S0 = SplitMix32.Next(ref seed);
                _xoshiro.S1 = SplitMix32.Next(ref seed);
                _xoshiro.S2 = SplitMix32.Next(ref seed);
                _xoshiro.S3 = SplitMix32.Next(ref seed);
            } while (_xoshiro.S0 == 0 && _xoshiro.S1 == 0 && _xoshiro.S2 == 0 && _xoshiro.S3 == 0);
        }

        public uint NextUInt()
        {
            return _xoshiro.Next();
        }

        public ulong NextULong()
        {
            return (((ulong)_xoshiro.Next()) << 32) | _xoshiro.Next();
        }

        public void Jump()
        {
            _xoshiro.Jump();
        }

        public void LongJump()
        {
            _xoshiro.LongJump();
        }

        internal ref Xoshiro128StarStar GetState()
        {
            return ref _xoshiro;
        }
    }
}