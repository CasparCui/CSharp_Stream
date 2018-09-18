using System;
using System.Text;

namespace Caspar.CSharpTest
{
    internal interface ITestFileStream
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