using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace EnergyHexPrint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var argv = new Energy.Base.Command.Arguments(args)
                    .About("Dump hexadecimal values for file")
                    .Usage("Usage: " + Energy.Core.Application.GetCommandName() + " file.bin")
                    .Switch("help")
                    .Switch("version")
                    .Switch("colorless")
                    .Parameter("line")
                    .Parameter("group")
                    .Alias("?", "help")
                    .Alias("t", "colorless")
                    .Alias("V", "version")
                    .Alias("l", "line")
                    .Alias("g", "group")
                    .Help("help", "Show this information")
                    .Help("version", "Display version number")
                    .Help("colorless", "Colorless output")
                    .Help("colorless", "Colorless output")
                    .Help("line", "Number of bytes per line")
                    .Help("group", "Number of bytes per group")
                    .Greetings("Greetings for VLX/Lamers" + "\r\n")
                    .Parse();

                if (argv["help"].IsTrue)
                {
                    Console.WriteLine(argv.Print());
                    return;
                }

                if (args.Length == 0)
                {
                    Console.WriteLine("No arguments, use --help for information");
                } 
                else
                {
                    var data = File.ReadAllBytes(args[0]);
                    //Console.WriteLine(Energy.Base.Hex.Print(data));
                    var printFormatSettings = new Energy.Base.Hex.PrintFormatSettings();
                    printFormatSettings.TildeOutput = !argv["colorless"].IsTrue;
                    printFormatSettings.OffsetSize = 8;
                    printFormatSettings.OffsetSeparator = "    ";
                    printFormatSettings.RepresentationSeparator = "    ";
                    printFormatSettings.BreakEvery = 8;
                    if (argv["line"].IsTrue)
                    {
                        printFormatSettings.LineSize = Energy.Base.Cast.AsInteger(argv["line"].Value);
                    }
                    if (argv["group"].IsTrue)
                    {
                        printFormatSettings.GroupSize = Energy.Base.Cast.AsInteger(argv["group"].Value);
                    }
                    Energy.Core.Tilde.WriteLine(Energy.Base.Hex.Print(data, printFormatSettings));
                }
            } catch (Exception e)
            {
                Energy.Core.Tilde.WriteException(e, true);
            }
            finally
            {
                if (Debugger.IsAttached) {
                    Energy.Core.Tilde.Break();
                    Energy.Core.Tilde.Pause();
                }
            }
        }
    }
}
