using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Utilities
{
    public class ImageOperations
    {
        IWebHostEnvironment _env;

        public ImageOperations(IWebHostEnvironment env)
        {
            _env = env;
        }
        /// <summary>
        /// Function will upload the Files to server i.e. wwwroot
        /// </summary>
        /// <param name="file">IForm File</param>
        /// <param name="UploadStatus">Status weather file is uploaded</param>
        /// <param name="prefix">PreFix to file name</param>
        /// <param name="postfix">PostFix to file name</param>
        /// <param name="name">Name of File (if null a GUID will be generated)</param>
        /// <param name="folder">Name of folder in WWW</param>
        /// <returns></returns>
        public string ImageUpload(IFormFile file, ref UploadStatus UploadStatus, string prefix="",string postfix="",string name = null,string folder= "MISC")
        {
            string filename = null;
            if (file != null)
            {
                string fileDirectory = Path.Combine(_env.WebRootPath, folder);
                filename = Guid.NewGuid() + "_";// + file.FileName;
                string extension = Path.GetExtension(file.FileName);
                if (this.IsAllowedImageType(extension))
                {
                    string filepath;
                    if (name == null)
                    {
                        filename = prefix + filename + postfix + extension;
                        filepath = Path.Combine(fileDirectory, filename);
                    }
                    else {
                        filename = prefix + name + postfix + extension;
                        filepath = Path.Combine(fileDirectory, prefix + name +postfix+extension);
                    }
                    
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                    
                    {
                        using (FileStream fs = new FileStream(filepath, FileMode.Create))
                        {
                            file.CopyTo(fs);
                            UploadStatus = UploadStatus.Success;
                        }
                    }
                }
                else {
                    UploadStatus = UploadStatus.InvalidFileType;
                }
                return Path.Combine(folder, filename);
                
            }

            return Path.Combine(folder, filename) ;
        }
        public void DeleteFile(string path)
        {
            string fileDirectory = Path.Combine(_env.WebRootPath , path);
            if (System.IO.File.Exists(fileDirectory) && System.IO.Path.GetFileName(path)!=null && System.IO.Path.GetFileName(path)!= "noImage.png")
            {
                System.IO.File.Copy(fileDirectory, Path.Combine(_env.WebRootPath,"Deleted",System.IO.Path.GetFileName(path)));
                System.IO.File.Delete(fileDirectory);
            }
        }
        private bool IsAllowedImageType(string extension)
        {
            extension = extension.ToUpper();
            var imagetypes = new List<string>() { ".JPG", ".JPEG", ".PNG", ".GIF", ".BMP" };
            foreach (var item in imagetypes)
            {
                if (extension.EndsWith(item))
                    return true;
            }
            return false;
            

        }
    }
    public enum UploadStatus { 
        Started,
        Success, 
        InvalidFileType,
        InvalidSize,
        AlreadyExsist,
    }
    public class FolderHelper
    {
        public static string MISC = "MISC";
        public static string Profile = "Profiles";
        public static string Degrees= "Degrees";
        public static string CNIC = "CNICs";
        public static string PaySlip = "PAYSLIPS";
        public static string AppointmentLetter = "AppointmentLetters";
        public static string Domiciles = "Domiciles";
        public static string NOCs = "NOCs";
        public static string Relievings = "Relievings";

    }
}
