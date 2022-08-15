using Microsoft.AspNetCore.Http;

namespace Common
{
    public class ImgManager
    {
        private readonly string _rootPath;

        public ImgManager(string rootPath)
        {
            _rootPath = rootPath;
        }

        public async Task<string> UploudImage(IFormFile file, string storagePath)
        {
            string fileName = file.FileName + DateTime.UtcNow.ToString("ddMMyyyyhhmmssfffffffK");
            fileName += Path.GetExtension(file.FileName);

            string folderName = Path.Combine(_rootPath, storagePath);
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            string fullPath = Path.Combine(folderName, fileName);

            using (FileStream localFile = File.OpenWrite(fullPath))
            using (Stream uploadedFile = file.OpenReadStream())
            {
                await uploadedFile.CopyToAsync(localFile);
            }
            return storagePath + "/" + fileName;
        }

        public void DeleteImage(string FilePath, string DomainName)
        {
            FilePath = FilePath.Replace(DomainName, "");
            string FileFullPath = _rootPath + FilePath;
            // If file with same name exists delete it
            if (File.Exists(FileFullPath))
            {
                File.Delete(FileFullPath);
            }
        }
    }
}