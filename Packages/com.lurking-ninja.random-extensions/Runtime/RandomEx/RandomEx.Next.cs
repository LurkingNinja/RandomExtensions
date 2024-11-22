// ReSharper disable MemberCanBePrivate.Global
namespace LurkingNinja.RandomExtensions
{
    using System;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using Algorithms;
    using Randoms;
    using Shims;

    public static partial class RandomEx
    {
        /// <summary>
        /// Returns a random bool value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBool(this IRandom random) => (random.NextUInt() & 1) == 1;

        /// <summary>
        /// Returns a random uint value in [0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint NextUInt(this IRandom random, uint max) => (uint)((random.NextUInt() * (ulong)max) >> 32);

        /// <summary>
        /// Returns a random uint value in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint NextUInt(this IRandom random, uint min, uint max)
        {
            ThrowHelper.CheckMinMax(min, max);
            var range = max - min;
            return (uint)(random.NextUInt() * (ulong)range >> 32) + min;
        }

        /// <summary>
        /// Returns a random int value in [int.MinValue, int.MaxValue].
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextInt(this IRandom random) => (int)random.NextUInt() ^ -2147483648;

        /// <summary>
        /// Returns a random int value in [0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextInt(this IRandom random, int max)
        {
            ThrowHelper.CheckMax(max);
            return (int)((random.NextUInt() * (ulong)max) >> 32);
        }

        /// <summary>
        /// Returns a random int value in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextInt(this IRandom random, int min, int max)
        {
            ThrowHelper.CheckMinMax(min, max);
            var range = (uint)(max - min);
            return (int)(random.NextUInt() * (ulong)range >> 32) + min;
        }

        /// <summary>
        /// Returns a random ulong value in [0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong NextULong(this IRandom random, ulong max)
        {
            switch (max)
            {
                case <= uint.MaxValue:
                    return random.NextUInt((uint)max);
                case > 1:
                {
                    var bits = Log2Ceiling(max);
                    while (true)
                    {
                        var result = random.NextULong() >> (sizeof(ulong) * 8 - bits);
                        
                        if (result < max) return result;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a random ulong value in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong NextULong(this IRandom random, ulong min, ulong max)
        {
            ThrowHelper.CheckMinMax(min, max);
            return NextULong(random, max - min) + min;
        }

        /// <summary>
        /// Returns a random long value in [long.MinValue, long.MaxValue].
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long NextLong(this IRandom random)
        {
            return (long)random.NextULong();
        }

        /// <summary>
        /// Returns a random long value in [0, max).
        /// </summary>
        public static long NextLong(this IRandom random, long max)
        {
            ThrowHelper.CheckMax(max);

            return max switch
            {
                <= int.MaxValue => (int)((random.NextUInt() * (ulong)max) >> 32),
                > 1 => (long)random.NextULong()
            };
        }

        /// <summary>
        /// Returns a random long value in [min, max).
        /// </summary>
        public static long NextLong(this IRandom random, long min, long max) => NextLong(random, max - min) + min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Log2Ceiling(ulong value)
        {
            var result = BitOperations.Log2(value);
            if (BitOperations.PopCount(value) != 1)
            {
                result++;
            }
            return result;
        }

        /// <summary>
        /// Returns a random float value in [0.0, 1.0).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this IRandom random) => (random.NextUInt() >> 8) * (1.0f / (1u << 24));

        /// <summary>
        /// Returns a random float value in [0.0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this IRandom random, float max)
        {
            ThrowHelper.CheckMax(max);
            return NextFloat(random) * max;
        }

        /// <summary>
        /// Returns a random float value in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this IRandom random, float min, float max)
        {
            ThrowHelper.CheckMinMax(min, max);

            var r = NextFloat(random) * (max - min) + min;

            if (r >= max)
            {
#if !NET6_0_OR_GREATER
                r = MathEx.BitDecrement(max);
#else
            r = MathF.BitDecrement(max);
#endif
            }

            return r;
        }

        /// <summary>
        /// Returns a random double value in [0.0, 1.0).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NextDouble(this IRandom random) => (random.NextULong() >> 11) * (1.0 / (1ul << 53));

        /// <summary>
        /// Returns a random double value in [0.0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NextDouble(this IRandom random, double max)
        {
            ThrowHelper.CheckMax(max);
            return NextDouble(random) * max;
        }

        /// <summary>
        /// Returns a random double value in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NextDouble(this IRandom random, double min, double max)
        {
            ThrowHelper.CheckMinMax(min, max);

            var r = NextDouble(random) * (max - min) + min;

            // correct for rounding
            if (r >= max)
            {
#if !NET6_0_OR_GREATER
                r = MathEx.BitDecrement(max);
#else
            r = Math.BitDecrement(max);
#endif
            }

            return r;
        }

        /// <summary>
        /// Returns a Gaussian distributed random double value with mean 0.0 and standard deviation 1.0.
        /// </summary>
        public static double NextDoubleGaussian(this IRandom random)
        {
            double v1, s;
            do
            {
                v1 = 2 * NextDouble(random) - 1;
                var v2 = 2 * NextDouble(random) - 1;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1 || s == 0);

            var multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
            return v1 * multiplier;
        }

        /// <summary>
        /// Fills the buffer with random bytes [0..0x7f].
        /// </summary>
        public static void NextBytes(this IRandom random, byte[] buffer)
        {
            ThrowHelper.ThrowIfNull(buffer);
            NextBytes(random, buffer.AsSpan());
        }

        /// <summary>
        /// Fills the buffer with random bytes [0..0x7f].
        /// </summary>
        public static void NextBytes(this IRandom random, Span<byte> buffer)
        {
            switch (random)
            {
                case Xoshiro128StarStarRandom xoshiro1:
                    FastNextBytes(ref xoshiro1.GetState(), buffer);
                    break;
                case SharedRandom when SharedRandom.GetLocalInstance() is Xoshiro128StarStarRandom xoshiro2:
                    FastNextBytes(ref xoshiro2.GetState(), buffer);
                    break;
                default:
                {
                    for (var i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = (byte)random.NextUInt();
                    }
                    break;
                }
            }
        }

        private static unsafe void FastNextBytes(ref Xoshiro128StarStar state, Span<byte> buffer)
        {
            uint s0 = state.S0, s1 = state.S1, s2 = state.S2, s3 = state.S3;

            while (buffer.Length >= sizeof(uint))
            {
                Unsafe.WriteUnaligned(
                    ref MemoryMarshal.GetReference(buffer), BitOperations.RotateLeft(s1 * 5, 7) * 9);

                var t = s1 << 9;
                s2 ^= s0;
                s3 ^= s1;
                s1 ^= s2;
                s0 ^= s3;
                s2 ^= t;
                s3 = BitOperations.RotateLeft(s3, 11);

                buffer = buffer[sizeof(uint)..];
            }

            if (!buffer.IsEmpty)
            {
                var next = BitOperations.RotateLeft(s1 * 5, 7) * 9;
                var remainingBytes = (byte*)&next;

                for (var i = 0; i < buffer.Length; i++) buffer[i] = remainingBytes[i];

                var t = s1 << 9;
                s2 ^= s0;
                s3 ^= s1;
                s1 ^= s2;
                s0 ^= s3;
                s2 ^= t;
                s3 = BitOperations.RotateLeft(s3, 11);
            }

            state.S0 = s0;
            state.S1 = s1;
            state.S2 = s2;
            state.S3 = s3;
        }
    }
}