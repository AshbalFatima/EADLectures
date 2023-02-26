namespace BSCSF19MVCCore.Models
{
    public class FileHelper
    {
        IWebHostEnvironment _env;
        public FileHelper(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string ImageUpload(IFormFile file, string folder="MISC")
        {
            string filename = null;
            if (file != null)
            {
                string fileDirectory = Path.Combine(_env.WebRootPath, folder);
                filename = Guid.NewGuid() + "_";// + file.FileName;
                string extension = Path.GetExtension(file.FileName);

                    string filepath = Path.Combine(fileDirectory ,filename+extension);
                if (this.IsAllowedImageType(extension))
                {


                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }


                    using (FileStream fs = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(fs);

                    }
                    return filepath;
                }
                else
                {
                    return null;
                }
            }
            return null;   
             

 
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
        private bool IsAllowedPDFType(string extension)
        {
            extension = extension.ToUpper();
            var imagetypes = new List<string>() { ".PDF" };
            foreach (var item in imagetypes)
            {
                if (extension.EndsWith(item))
                    return true;
            }
            return false;


        }
        private bool IsAllowedPDFAndDocType(string extension)
        {
            extension = extension.ToUpper();
            var imagetypes = new List<string>() { ".PDF",".DOC",".DOCX" };
            foreach (var item in imagetypes)
            {
                if (extension.EndsWith(item))
                    return true;
            }
            return false;


        }
    }
}
