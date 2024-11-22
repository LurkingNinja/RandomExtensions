// ReSharper disable MemberCanBePrivate.Global
namespace LurkingNinja.RandomExtensions.Unity
{
    using System.Runtime.CompilerServices;
    using RandomExtensions;
    using UnityEngine;

    public static class UnityRandomExtensions
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
            new(random.NextFloat(max.x), random.NextFloat(max.y));

        /// <summary>
        /// Returns a random Vector2 value with all components in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 NextVector2(this IRandom random, Vector2 min, Vector2 max) =>
            new(random.NextFloat(min.x, max.x), random.NextFloat(min.y, max.y));

        /// <summary>
        /// Returns a unit length Vector2 vector representing a uniformly random 2D direction.
        /// </summary>
        public static Vector2 NextVector2Direction(this IRandom random)
        {
            var angle = random.NextFloat() * Mathf.PI * 2.0f;

            var s = Mathf.Sin(angle);
            var c = Mathf.Cos(angle);

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
            new(random.NextFloat(max.x), random.NextFloat(max.y), random.NextFloat(max.z));

        /// <summary>
        /// Returns a random Vector3 value with all components in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 NextVector3(this IRandom random, Vector3 min, Vector3 max) =>
            new(random.NextFloat(min.x, max.x), random.NextFloat(min.y, max.y), random.NextFloat(min.z, max.z));

        /// <summary>
        /// Returns a unit length Vector2 vector representing a uniformly random 2D direction.
        /// </summary>
        public static Vector3 NextVector3Direction(this IRandom random)
        {
            var rnd = NextVector2(random);
            var z = rnd.x * 2.0f - 1.0f;
            var r = Mathf.Sqrt(Mathf.Max(1.0f - z * z, 0.0f));
            var angle = rnd.y * Mathf.PI * 2.0f;

            var s = Mathf.Sin(angle);
            var c = Mathf.Cos(angle);

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
            new(random.NextFloat(max.x), random.NextFloat(max.y), random.NextFloat(max.z), random.NextFloat(max.w));

        /// <summary>
        /// Returns a random Vector4 value with all components in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 NextVector4(this IRandom random, Vector4 min, Vector4 max) =>
            new(random.NextFloat(min.x, max.x), random.NextFloat(min.y, max.y),
                random.NextFloat(min.z, max.z), random.NextFloat(min.w, max.w));

        /// <summary>
        /// Returns a unit length quaternion representing a uniformly 3D rotation.
        /// </summary>
        public static Quaternion NextQuaternionRotation(this IRandom random)
        {
            var rnd = random.NextVector3(new Vector3(2.0f * Mathf.PI, 2.0f * Mathf.PI, 1.0f));
            var u1 = rnd.z;
            var thetaRho = (Vector2)rnd;

            var i = Mathf.Sqrt(1.0f - u1);
            var j = Mathf.Sqrt(u1);

            var sinThetaRho = new Vector2(Mathf.Sin(thetaRho.x), Mathf.Sin(thetaRho.y));
            var cosThetaRho = new Vector2(Mathf.Cos(thetaRho.x), Mathf.Cos(thetaRho.y));

            var q = new Quaternion(i * sinThetaRho.x, i * cosThetaRho.x, j * sinThetaRho.y, j * cosThetaRho.y);
            return q.w < 0.0f ? q : new Quaternion(-q.x, -q.y, -q.z, -q.w);
        }

        /// <summary>
        /// Returns a random Color.
        /// </summary>
        public static Color NextColor(this IRandom random)
        {
            var v = random.NextVector4();
            return new Color(v.x, v.y, v.z, v.w);
        }

        /// <summary>
        /// Returns a random Color value with all components in [0, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color NextColor(this IRandom random, Color max) =>
            new Vector4(random.NextFloat(max.r), random.NextFloat(max.g),
                random.NextFloat(max.b), random.NextFloat(max.a));

        /// <summary>
        /// Returns a random Color value with all components in [min, max).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color NextColor(this IRandom random, Color min, Color max) => 
            new Vector4(random.NextFloat(min.r, max.r), random.NextFloat(min.g, max.g),
                random.NextFloat(min.b, max.b), random.NextFloat(min.a, max.a));

        /// <summary>
        /// Returns a random Color with HSV values.
        /// </summary>
        public static Color NextColorHSV(this IRandom random, float hueMin, float hueMax, float saturationMin,
            float saturationMax, float valueMin, float valueMax)
        {
            var h = Mathf.Lerp(hueMin, hueMax, random.NextFloat());
            var s = Mathf.Lerp(saturationMin, saturationMax, random.NextFloat());
            var v = Mathf.Lerp(valueMin, valueMax, random.NextFloat());
            
            return Color.HSVToRGB(h, s, v, true);
        }

        /// <summary>
        /// Returns a random Color with HSV and alpha values.
        /// </summary>
        public static Color NextColorHSV(this IRandom random, float hueMin, float hueMax, float saturationMin,
            float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
        {
            var color = NextColorHSV(random, hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
            color.a = Mathf.Lerp(alphaMin, alphaMax, random.NextFloat());
            
            return color;
        }
    }
}