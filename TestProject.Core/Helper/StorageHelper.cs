using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Core.Helper
{
    public static class StorageHelper
    {
        private static readonly string _filePath;

        static StorageHelper()
        {
        }
        public static async Task<bool> WriteByteToFileAsync(string audioFilePath, byte[] audioFileContent)
        {
            string file = GetFullPathFile(audioFilePath);

            try
            {
                using (FileStream sourceStream = new FileStream(file, FileMode.OpenOrCreate))
                {
                    await sourceStream.WriteAsync(audioFileContent, 0, audioFileContent.Length);
                };

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<byte[]> ReadFileAsync(string audioFilePath)
        {
            byte[] result;

            string file = GetFullPathFile(audioFilePath);

            try
            {
                using (FileStream SourceStream = File.Open(file, FileMode.Open))
                {
                    result = new byte[SourceStream.Length];
                    await SourceStream.ReadAsync(result, 0, (int)SourceStream.Length);
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<bool> DeleteFile(string audioFilePath)
        {
            string file = GetFullPathFile(audioFilePath);

            try
            {
                using (FileStream stream = new FileStream(file, FileMode.Truncate, FileAccess.Write, FileShare.Delete, 4096, true))
                {
                    await stream.FlushAsync();
                    File.Delete(file);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string GetFullPathFile(string audioFilePath)
        {
            string file = _filePath + audioFilePath;

            return file;
        }
    }
}
