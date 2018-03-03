﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Blazor.Razor
{
    // Constants for method names used in code-generation
    // Keep these in sync with the actual definitions
    internal static class BlazorApi
    {
        public static class BlazorComponent
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.BlazorComponent";

            public static readonly string BuildRenderTree = nameof(BuildRenderTree);
        }

        public static class LayoutAttribute
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Layouts.LayoutAttribute";
        }

        public static class IComponent
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.Components.IComponent";

            public static readonly string MetadataName = FullTypeName;
        }

        public static class IDictionary
        {
            public static readonly string MetadataName = "System.Collection.IDictionary`2";
        }

        public static class RenderFragment
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.RenderFragment";
        }
        
        public static class RenderTreeBuilder
        {
            public static readonly string FullTypeName = "Microsoft.AspNetCore.Blazor.RenderTree.RenderTreeBuilder";

            public static readonly string OpenElement = nameof(OpenElement);

            public static readonly string CloseElement = nameof(CloseElement);

            public static readonly string OpenComponent = nameof(OpenComponent);

            public static readonly string CloseComponent = nameof(CloseComponent);

            public static readonly string AddContent = nameof(AddContent);

            public static readonly string AddAttribute = nameof(AddAttribute);

            public static readonly string Clear = nameof(Clear);

            public static readonly string GetFrames = nameof(GetFrames);

            public static readonly string ChildContent = nameof(ChildContent);
        }
    }
}
