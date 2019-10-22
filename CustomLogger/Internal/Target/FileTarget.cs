namespace CustomLogger.Internal
{
    using System;
    using System.IO;

    internal class FileTarget : Target, IEquatable<FileTarget>
    {
        public FileTarget(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public bool Equals(FileTarget other)
        {
            return Path.Equals(other.Path);
        }

        public override bool Equals(object obj)
        {
            return obj is FileTarget target && Equals(target);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path);
        }

        public override ITargetWriter GetTargetWriter()
        {
            FileStream loggingStream = null;

            try
            {
                loggingStream = File.Open(Path, FileMode.Append);
            }
            catch (IOException e)
            {
                // internal log and rethrow
                throw e;
            }
            catch (NotSupportedException e)
            {
                // internal log and rethrow
                throw e;
            }
            catch (UnauthorizedAccessException e)
            {
                // internal log and rethrow
                throw e;
            }

            return new FileWriter(loggingStream);
        }
    }
}
