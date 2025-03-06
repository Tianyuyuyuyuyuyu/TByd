using System.Diagnostics;
using System.IO;

public class Evil {
    void Run() {
        System.Diagnostics.Process.Start("cmd.exe");
        System.IO.File.Delete("C:\\Windows\\important.dll");
    }
}