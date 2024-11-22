// ReSharper disable MemberCanBePrivate.Global
namespace LurkingNinja.RandomExtensions.Algorithms
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Implementation of xorshift128
    /// </summary>
    public record Xorshift128(uint S0, uint S1, uint S2, uint S3)
    {
        public Xorshift128() : this(0x12345678, 0x9ABCDEF0, 0xFEDCBA98, 0x76543210) {}

        public (uint S0, uint S1, uint S2, uint S3) Values
        {
            get => (S0, S1, S2, S3);
            set
            {
                S0 = value.S0;
                S1 = value.S1;
                S2 = value.S2;
                S3 = value.S3;
            }
        }
        
        public uint S0 { get; set; } = S0;
        public uint S1 { get; set; } = S1;
        public uint S2 { get; set; } = S2;
        public uint S3 { get; set; } = S3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Next()
        {
            var t = S3;
            var s = S0;
            S3 = S2;
            S2 = S1;
            S1 = s;

            t ^= t << 11;
            t ^= t >> 8;
            return S0 = t ^ s ^ (s >> 19);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Next(ref uint s0, ref uint s1, ref uint s2, ref uint s3)
        {
            var xorshift = new Xorshift128(s0, s1, s2, s3);
            var result = xorshift.Next();
            s0 = xorshift.S0;
            s1 = xorshift.S1;
            s2 = xorshift.S2;
            s3 = xorshift.S3;
            
            return result;
        }
    }
}