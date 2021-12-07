namespace Resxtractor
{
    using CommandLine;
    using Resxtractor.Options;
    using System;

    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int Main(string[] args)
        {
            try
            {
                return Parser.Default.ParseArguments<ExtractOptions>(args)
                    .MapResult(
                        (ExtractOptions opts) => opts.Extract(),
                        errs => { return 1; }
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
