// Guids.cs
// MUST match guids.h
using System;

namespace Company.ColorSchemeSelector
{
    static class GuidList
    {
        public const string guidColorSchemeSelectorPkgString = "21ba397b-c8ca-4b78-862f-47c457493c46";
        public const string guidColorSchemeSelectorCmdSetString = "edcf5d10-812c-4739-8471-ef23df1f603a";
        public const string guidToolWindowPersistanceString = "2b119b11-7f31-4e10-adf1-6b43a180dbac";

        public static readonly Guid guidColorSchemeSelectorCmdSet = new Guid(guidColorSchemeSelectorCmdSetString);
    };
}