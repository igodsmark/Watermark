// See https://aka.ms/new-console-template for more information
using Watermark;


List<string> files = new List<string>();
if(args.Length > 0)
{
    Console.WriteLine(args[0]);
    var directories = Directory.GetDirectories(args[0]);
    if(directories.Length > 0)
    {
        foreach(var directory in directories)
        {
            Console.WriteLine($"Searching for images in {directory}");
            files.AddRange(Directory.GetFiles(directory));
        }
    }
    else
    {

        files = Directory.GetFiles(args[0]).ToList();

    }
    
}
else
{

    files = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "*.jpg").ToList();
}
foreach(var file in files)
{
    Console.WriteLine(file);
    Watermark.Utilities.WatermarkedImage(Directory.GetParent(file).FullName, Path.GetFileName(file), "Watermark!", Directory.GetParent(file).FullName);
}