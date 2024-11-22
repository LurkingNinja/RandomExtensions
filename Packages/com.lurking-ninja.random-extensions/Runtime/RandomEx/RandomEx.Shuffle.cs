namespace LurkingNinja.RandomExtensions
{
    using System;
 
    public static partial class RandomEx
    {
        /// <summary>
        /// Performs an in-place shuffle of an array.
        /// </summary>
        public static void Shuffle<T>(this IRandom random, T[] values)
        {
            ThrowHelper.ThrowIfNull(values);

            for (var i = values.Length - 1; i > 0; i--)
            {
                var r = random.NextInt(i + 1);
                (values[i], values[r]) = (values[r], values[i]);
            }
        }

        /// <summary>
        /// Performs an in-place shuffle of a span.
        /// </summary>
        public static void Shuffle<T>(this IRandom random, Span<T> values)
        {
            for (var i = values.Length - 1; i > 0; i--)
            {
                var r = random.NextInt(i + 1);
                (values[i], values[r]) = (values[r], values[i]);
            }
        }
    }
}