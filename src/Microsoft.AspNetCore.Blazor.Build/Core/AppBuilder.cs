// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Blazor.Build.Core.FileSystem;
using System;
using System.IO;

namespace Microsoft.AspNetCore.Blazor.Build.Core
{
    internal static class AppBuilder
    {
        // Keep in sync with the const in Microsoft.AspNetCore.Blazor.Server's LiveReloading.cs
        const string BlazorBuildCompletedSignalFile = "__blazorBuildCompleted";

        internal static void Execute(string assemblyPath, string webRootPath)
        {
            var clientFileSystem = new ClientFileProvider(assemblyPath, webRootPath);
            var distDirPath = Path.Combine(Path.GetDirectoryName(assemblyPath), "dist");
            FileUtil.WriteFileProviderToDisk(clientFileSystem, distDirPath, clean: true);
            WriteBuildCompletedSignal(distDirPath);
        }

        private static void WriteBuildCompletedSignal(string distDirPath)
        {
            // The live reload mechanism needs to know when the build is *finished*, not
            // just when the first modified file starts being written. As a simple way
            // to signal this, write a file with a special name. We can then delete it.
            var signalFilePath = Path.Combine(distDirPath, BlazorBuildCompletedSignalFile);
            File.WriteAllText(signalFilePath, Guid.NewGuid().ToString());
            File.Delete(signalFilePath);
        }
    }
}
