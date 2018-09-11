using System;
using System.IO;
using System.Text;

namespace Test1
{
    internal class TestFileStreamC : TestFileStream, IDisposable
    {
        private FileStream fileStream;
        private StreamReader streamReader;
        private readonly byte[] Unicode = new byte[] { 0xFF, 0xFE };
        private readonly byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
        private readonly byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
        private const int length_1M = 10 * 10;//for test 10 * 10;//静态变量会在编译时完成计算，此处写计算值对系统开销可以忽略。

        public TestFileStreamC(String filePath)
        {
            this.FilePath = filePath;
            fileStream = GetFileStreamToOpen();
            FileEncoding = GetEncoding(fileStream);
            streamReader = new StreamReader(fileStream, FileEncoding);
            var fileinfo = new FileInfo(this.FilePath);
            FileName = fileinfo.Name;
            FileSize = fileinfo.Length;
        }

        public string FilePath { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; }
        public Encoding FileEncoding { get; }


        public string GetFileDir() => FilePath;

        public string GetFileName() => FileName;

        private FileStream GetFileStreamToOpen()
        {
            return new FileStream(this.FilePath, FileMode.Open);
        }

        public string ReadLine()
        {
            return streamReader.ReadLine();
        }

        public void Dispose()
        {
            fileStream.Close();
            streamReader.Close();
        }

        public System.Text.Encoding GetEncoding(FileStream fs)
        {
            //定义 数组后使用可以加快调用速度，这个地方需要写成编译时常量。

            Encoding reVal = Encoding.Default;

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            //字节流 r 由于是由文件流注入生成，实际上就是文件流的一个二次包装，文件流已经有特殊的 dispose 方法时，无需特殊进行 close。
            int i = Convert.ToInt32(fs.Length);
            byte[] ss;
            //一次读取过多内容会造成系统开销过大，采用 1M 一次的多次读取来解放文件流开销。速度还是慢。可以考虑先读3个。
            ss = r.ReadBytes(3);
            reVal = CheckEncoding(ss);
            if (reVal.Equals(Encoding.Default))
            {
                i -= 3;//焦点已经移动了3，利用偏移量来保证系统不出问题。此处也可以使用 Position 属性，但是开销比手动移动要大。
                if (i < length_1M)
                {
                    ss = r.ReadBytes(i);
                    reVal = CheckEncoding(ss);
                }
                else
                {
                    for (int j = i / (length_1M); j > 0; j--)
                    {
                        
                        ss = r.ReadBytes(length_1M);
                        var encoding = CheckEncoding(ss);
                        if (encoding.Equals(Encoding.UTF8))
                        {
                            if (ss[0] == UTF8[0] && ss[1] == UTF8[1] && ss[2] == UTF8[2])
                            {
                                reVal = encoding;
                                break;
                            }
                            continue;
                        }
                        else { reVal = encoding; }
                        if(j>128)
                        {
                            j -= 10;
                        }//单一文件超过128M，仅调查最后100M，前面部分进行抽样调查。
                    }
                    int c = i % (length_1M);
                    ss = r.ReadBytes(c);
                    reVal = CheckEncoding(ss);
                }
            }

            fs.Position = 0;//判断字符集之后文件已经完成读取，流的位置会指向文件末尾，如果不做归零操作，将无法读出任何数据。
            return reVal;
        }

        private Encoding CheckEncoding(byte[] ss)
        {
            Encoding encoding = Encoding.Default;
            if ((ss[0] == UTF8[0] && ss[1] == UTF8[1] && ss[2] == UTF8[2]) || IsUTF8Bytes(ss))//开销大的动作放前面不合理。
            {
                encoding = Encoding.UTF8;
            }
            else if (ss[0] == UnicodeBIG[0] && ss[1] == UnicodeBIG[1] && ss[2] == UnicodeBIG[2])
            {
                encoding = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == Unicode[0] && ss[1] == Unicode[1])//不同语言的操作系统，最后一位并不完全相同所以没有必要做最后一位的对比。
            {
                encoding = Encoding.Unicode;
            }
            return encoding;
        }

        /// <summary>
        /// 判断流中文件字符集是否为 UTF-8 需要判断所有字符，开销极大。
        /// </summary>
        /// <param name="data">文件流读取出来的全部字节</param>
        /// <returns></returns>
        private bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
    }
}