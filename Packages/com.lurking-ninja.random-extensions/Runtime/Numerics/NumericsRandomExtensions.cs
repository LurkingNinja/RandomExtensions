// ReSharper disable MemberCanBePrivate.Global
namespace LurkingNinja.RandomExtensions.Numerics
{
    using System;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using RandomExtensions;

    public static class NumericsRandomExtensions
    {
        /// <summary>
        /// Returns a random Vector2 value with all components in [0, 1).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NextVector2(this IRandom random) => new(random.NextFloat(), random.NextFloat());

        /// <summary>
        /// Returns a random Vector2 value with all components in [0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NextVector2(this IRandom random, Vector2 max) =>
            new(random.NextFloat(max.X), random.NextFloat(max.Y));

        /// <summary>
        /// Returns a random Vector2 value with all components in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NextVector2(this IRandom random, Vector2 min, Vector2 max) =>
            new(random.NextFloat(min.X, max.X), random.NextFloat(min.Y, max.Y));

        /// <summary>
        /// Returns a unit length Vector2 vector representing a uniformly random 2D direction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NextVector2Direction(this IRandom random)
        {
            var angle = random.NextFloat() * MathF.PI * 2.0f;
            var s = MathF.Sin(angle);
            var c = MathF.Cos(angle);

            return new Vector2(c, s);
        }

        /// <summary>
        /// Returns a random point inside a unit circle.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NextVector2InsideCircle(this IRandom random) =>
            NextVector2Direction(random) * random.NextFloat();

        /// <summary>
        /// Returns a random Vector3 value with all components in [0, 1).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NextVector3(this IRandom random) =>
            new(random.NextFloat(), random.NextFloat(), random.NextFloat());

        /// <summary>
        /// Returns a random Vector3 value with all components in [0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NextVector3(this IRandom random, Vector3 max) =>
            new(random.NextFloat(max.X), random.NextFloat(max.Y), random.NextFloat(max.Z));

        /// <summary>
        /// Returns a random Vector3 value with all components in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NextVector3(this IRandom random, Vector3 min, Vector3 max) =>
            new(random.NextFloat(min.X, max.X), random.NextFloat(min.Y, max.Y), random.NextFloat(min.Z, max.Z));

        /// <summary>
        /// Returns a unit length Vector2 vector representing a uniformly random 2D direction.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NextVector3Direction(this IRandom random)
        {
            var rnd = NextVector2(random);
            var z = rnd.X * 2.0f - 1.0f;
            var r = MathF.Sqrt(MathF.Max(1.0f - z * z, 0.0f));
            var angle = rnd.Y * MathF.PI * 2.0f;

            var s = MathF.Sin(angle);
            var c = MathF.Cos(angle);

            return new Vector3(c * r, s * r, z);
        }

        /// <summary>
        /// Returns a random point inside a unit sphere.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NextVector3InsideSphere(this IRandom random) =>
            NextVector3Direction(random) * random.NextFloat();

        /// <summary>
        /// Returns a random Vector4 value with all components in [0, 1).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 NextVector4(this IRandom random) =>
            new(random.NextFloat(), random.NextFloat(), random.NextFloat(), random.NextFloat());

        /// <summary>
        /// Returns a random Vector4 value with all components in [0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 NextVector4(this IRandom random, Vector4 max) =>
            new(random.NextFloat(max.X), random.NextFloat(max.Y), random.NextFloat(max.Z), random.NextFloat(max.W));

        /// <summary>
        /// Returns a random Vector4 value with all components in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 NextVector4(this IRandom random, Vector4 min, Vector4 max) =>
            new(random.NextFloat(min.X, max.X), random.NextFloat(min.Y, max.Y),
                random.NextFloat(min.Z, max.Z), random.NextFloat(min.W, max.W));

        /// <summary>
        /// Returns a unit length quaternion representing a uniformly 3D rotation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion NextQuaternionRotation(this IRandom random)
        {
            var rnd = random.NextVector3(new Vector3(2.0f * MathF.PI, 2.0f * MathF.PI, 1.0f));
            var u1 = rnd.Z;
            var thetaRho = new Vector2(rnd.X, rnd.Y);

            var i = MathF.Sqrt(1.0f - u1);
            var j = MathF.Sqrt(u1);

            var sinThetaRho = new Vector2(MathF.Sin(thetaRho.X), MathF.Sin(thetaRho.Y));
            var cosThetaRho = new Vector2(MathF.Cos(thetaRho.X), MathF.Cos(thetaRho.Y));

            var q = new Quaternion(i * sinThetaRho.X, i * cosThetaRho.X, j * sinThetaRho.Y, j * cosThetaRho.Y);
            return q.W < 0.0f ? q : -q;
        }
    }
}