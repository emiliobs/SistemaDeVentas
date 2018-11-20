namespace SistemaDeVentas.Library
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class UploadImage
    {

        public async Task CopiarImagenAsync(IFormFile avatarImagen,  string fileName, IHostingEnvironment environment)
        {
            try
            {

                if (null == avatarImagen)
                {
                    var archivoOrigen = environment.ContentRootPath + $"/wwwroot/images/foto/Usuarios/default.png";
                    var destinoFileName = environment.ContentRootPath + $"/wwwroot/images/foto/Usuarios/{fileName}";
                    File.Copy(archivoOrigen, destinoFileName, true);
                }
                else
                {
                    var filePath = Path.Combine(environment.ContentRootPath, "wwwroot/images/foto/Usuarios/" + fileName );

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await avatarImagen.CopyToAsync(stream);
                    }
                }
                                                                                       
                       

            }
            catch (Exception ex)
            {



            }


            
        }

    }
}
