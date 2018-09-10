using System;
using System.Text;

namespace Test1
{
    internal interface TestFileStream
    {
        String GetFileName();

        String GetFileDir();

        string FileName { get; set; }
        string FilePath { get; set; }
        long FileSize { get; }
        Encoding FileEncoding { get; }
        String ReadLine();
    }
}