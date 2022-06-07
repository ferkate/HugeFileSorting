// See https://aka.ms/new-console-template for more information

var fileName = Environment.GetCommandLineArgs()[1];
var fileSizeParseResult = long.TryParse(Environment.GetCommandLineArgs()[2], out var fileSize);

Console.WriteLine("File start to generate, please wait ...");
var g = new InputGenerator.InputGenerator();
g.GenerateInputFile(fileName, fileSize);
Console.WriteLine("File was successfully generated");