﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Api.Helper
{
    public class Filemanager
    {
        public static string Save(string rootPath, string folder, IFormFile file)
        {
            string filename = file.FileName;
            filename = filename.Length <= 64 ? (filename) : (filename.Substring(filename.Length - 64, 64));
            filename = Guid.NewGuid().ToString() + filename;
            var path = Path.Combine(rootPath, folder, filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }

        public static bool Delete(string rootPath, string folder, string filename)
        {
            var path = Path.Combine(rootPath, folder, filename);

            if (System.IO.File.Exists(filename))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;

        }
    }
}
