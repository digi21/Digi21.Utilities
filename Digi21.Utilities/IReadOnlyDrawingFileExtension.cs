using Digi21.DigiNG.Entities;
using Digi21.DigiNG.IO;

namespace Digi21.Utilities
{
    public static class IReadOnlyDrawingFileExtension
    {
        public static bool Contains(this IReadOnlyDrawingFile file, Entity entity) => entity.Owner == file;
    }
}
