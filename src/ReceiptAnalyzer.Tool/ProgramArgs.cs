namespace BS.ReceiptAnalyzer.Tool
{
    internal sealed record ProgramArgs(string InputDirectory, string OutputDirectory, ProcessingType ProcessingType);

    internal enum ProcessingType
    {
        ReceiptRecognition,
        ReceiptPartitioning
    }

    internal static class ProgramArgsExtensions
    {
        public static ProgramArgs GetArgs(this string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
                throw new ArgumentException();

            var inputDirArg = args[0];
            var outputDirArg = args.Length == 2 ? $"{inputDirArg}\\output" : args[1];
            var typeArg = args.Length == 2 ? args[1] : args[2];
            var type = Enum.Parse<ProcessingType>(typeArg);

            return new ProgramArgs(inputDirArg, outputDirArg, type);
        }
    }
}
