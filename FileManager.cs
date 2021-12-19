using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BleCommunication
{
    class FileManager : IDisposable
    {
        private readonly string filePath = "log.csv";
        private FileStream fs;
        private bool isOpen;

        public FileManager(string filepath)
        {
            filePath = filepath;
        }

        public void Dispose()
        {
            Close();
            fs = null;
        }

        ~FileManager()
        {
            Dispose();
        }

        public void Open()
        {
            if (isOpen) return;
            if (File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            }
            else
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            }
            isOpen = true;
        }

        public void Close()
        {
            if (!isOpen) return;
            isOpen = false;
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
        }

        public async Task Write(string text)
        {
            if (isOpen)
            {
                var data = Encoding.GetEncoding("shift_jis").GetBytes(text + "\n");
                await fs.WriteAsync(data, 0, data.Length);
            }
        }

        public async Task<string> Read()
        {
            if (isOpen)
            {
                var sb = new StringBuilder();

                byte[] buffer = new byte[4096];
                int numRead;
                while ((numRead = await fs.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.GetEncoding("shift_jis").GetString(buffer, 0, numRead);
                    sb.Append(text);
                }
                return sb.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
