// See https://aka.ms/new-console-template for more information


using System.Diagnostics;

var process = new Process();

process.StartInfo = new ProcessStartInfo("http://localhost/")
{
    UseShellExecute = true
};
for (int i = 0; i < 2049; i++)
{
    process.Start();
}