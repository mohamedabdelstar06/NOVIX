using System.IO;
using System.Threading.Tasks;

namespace NOVIX.Catalog.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(Stream fileStream, string fileName);
        void DeleteFile(string fileUrl);
    }
}
