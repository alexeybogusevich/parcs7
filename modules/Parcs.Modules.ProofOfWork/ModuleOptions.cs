﻿namespace Parcs.Modules.ProofOfWork
{
    public class ModuleOptions
    {
        public int Difficulty { get; set; } = 10;

        public string Prompt { get; set; } = "Hello world!";

        public long NonceBatchSize { get; set; } = 1000;

        public long MaximumNonce { get; set; } = 3000000;

        public string OutputFilename { get; set; } = "Output.txt";
    }
}