// ReSharper disable MemberCanBePrivate.Global
namespace LurkingNinja.RandomExtensions.Algorithms
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Implementation of xorshift64
    /// </summary>
    public record Xorshift64(ulong State)
    {
        public Xorshift64() : this(0x123456789ABCDEF) {}

        public ulong State { get; set; } = State;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong Next()
        {
            var x = State;
            x ^= x << 13;
            x ^= x >> 7;
            x ^= x << 17;
            return State = x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Next(ref ulong state)
        {
            var xorshift = new Xorshift64(state);
            var result = xorshift.Next();
            state = xorshift.State;
            
            return result;
        }
    }
}