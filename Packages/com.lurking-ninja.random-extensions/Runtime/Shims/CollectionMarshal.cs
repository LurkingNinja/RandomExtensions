// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Local
#if !NET6_0_OR_GREATER
namespace System.Runtime.InteropServices
{
    using CompilerServices;
    using Collections.Generic;

    internal static class CollectionsMarshal
    {
        public static Span<T> AsSpan<T>(List<T> list)
        {
            ref var view = ref Unsafe.As<List<T>, ListView<T>>(ref list);
            return view.Items.AsSpan(0, view.Size);
        }

        private sealed record ListView<T>
        {
            public readonly T[] Items = default!;
            public int Size;
            public int Version;
        }
    }
}
#endif