using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Contracts
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(string bucketName, string fileName, byte[] content);

        Task<string> GenerateSignedUrlAsync(string bucketName, string fileName, TimeSpan expiration);

        Task<Stream> ReadFilesAsync(string bucketName, string directory, string fileName);
        Task<Stream> ReadFileAsync(string bucketName, string fileName);
    }
}
