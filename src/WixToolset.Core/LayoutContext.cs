// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Core
{
    using System.Collections.Generic;
    using System.Threading;
    using WixToolset.Extensibility;
    using WixToolset.Extensibility.Data;
    using WixToolset.Extensibility.Services;

    internal class LayoutContext : ILayoutContext
    {
        internal LayoutContext(IWixToolsetServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public IWixToolsetServiceProvider ServiceProvider { get; }

        public IEnumerable<ILayoutExtension> Extensions { get; set; }

        public IEnumerable<IFileSystemExtension> FileSystemExtensions { get; set; }

        public IEnumerable<IFileTransfer> FileTransfers { get; set; }

        public IEnumerable<ITrackedFile> TrackedFiles { get; set; }

        public string IntermediateFolder { get; set; }

        public string ContentsFile { get; set; }

        public string OutputsFile { get; set; }

        public string BuiltOutputsFile { get; set; }

        public bool SuppressAclReset { get; set; }

        public CancellationToken CancellationToken { get; set; }
    }
}
