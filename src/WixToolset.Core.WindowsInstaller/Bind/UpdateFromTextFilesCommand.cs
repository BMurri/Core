// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolset.Core.WindowsInstaller.Bind
{
    using System;
    using System.IO;
    using System.Linq;
    using WixToolset.Data;
    using WixToolset.Data.Symbols;
    using WixToolset.Extensibility.Services;

    internal class UpdateFromTextFilesCommand
    {
        public UpdateFromTextFilesCommand(IMessaging messaging, IntermediateSection section)
        {
            this.Messaging = messaging;
            this.Section = section;
        }

        private IMessaging Messaging { get; }

        private IntermediateSection Section { get; }

        public void Execute()
        {
            foreach (var bbControl in this.Section.Symbols.OfType<BBControlSymbol>().Where(t => t.SourceFile != null))
            {
                bbControl.Text = this.ReadTextFile(bbControl.SourceLineNumbers, bbControl.SourceFile.Path);
            }

            foreach (var control in this.Section.Symbols.OfType<ControlSymbol>().Where(t => t.SourceFile != null))
            {
                control.Text = this.ReadTextFile(control.SourceLineNumbers, control.SourceFile.Path);
            }

            foreach (var customAction in this.Section.Symbols.OfType<CustomActionSymbol>().Where(c => c.ScriptFile != null))
            {
                customAction.Target = this.ReadTextFile(customAction.SourceLineNumbers, customAction.ScriptFile.Path);
            }
        }

        /// <summary>
        /// Reads a text file and returns the contents.
        /// </summary>
        /// <param name="sourceLineNumbers">Source line numbers for row from source.</param>
        /// <param name="source">Source path to file to read.</param>
        /// <returns>Text string read from file.</returns>
        private string ReadTextFile(SourceLineNumber sourceLineNumbers, string source)
        {
            try
            {
                using (var reader = new StreamReader(source))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (DirectoryNotFoundException e)
            {
                this.Messaging.Write(ErrorMessages.BinderFileManagerMissingFile(sourceLineNumbers, e.Message));
            }
            catch (FileNotFoundException e)
            {
                this.Messaging.Write(ErrorMessages.BinderFileManagerMissingFile(sourceLineNumbers, e.Message));
            }
            catch (IOException e)
            {
                this.Messaging.Write(ErrorMessages.BinderFileManagerMissingFile(sourceLineNumbers, e.Message));
            }
            catch (NotSupportedException)
            {
                this.Messaging.Write(ErrorMessages.FileNotFound(sourceLineNumbers, source));
            }

            return null;
        }
    }
}
