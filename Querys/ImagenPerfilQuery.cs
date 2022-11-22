using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc.Querys
{
    public class ImagenPerfilQuery
    {
        private readonly DBContext _dbContext;
        public ImagenPerfilQuery(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<ImagenPerfil> store(ImagenPerfil imagenPerfil)
        {
            this._dbContext.ImagenPerfil.Add(imagenPerfil);
            await _dbContext.SaveChangesAsync();
            return await this._dbContext.ImagenPerfil.Where(h => h.Id == imagenPerfil.Id).FirstOrDefaultAsync();
        }
        public async Task<ImagenPerfil> Update( int id, ImagenPerfil imagenPerfil)
        {
            _dbContext.Entry(await _dbContext.ImagenPerfil.FirstOrDefaultAsync(x => x.PersonaId == id)).CurrentValues.SetValues(new
            {
                base64 = imagenPerfil.base64,
                ControlIdTimestamp = imagenPerfil.ControlIdTimestamp,
                ControlIdImage = imagenPerfil.ControlIdImage,
                ControlUserId = imagenPerfil.ControlUserId,
                Id = imagenPerfil.Id,
                FechaCreacion = imagenPerfil.FechaCreacion,
                Name = imagenPerfil.Name,
                Caption = imagenPerfil.Caption,
                Size = imagenPerfil.Size,
                Path = imagenPerfil.Path,
                PersonaId = imagenPerfil.PersonaId
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.ImagenPerfil.Where(p => p.Id == imagenPerfil.Id).Include(x => x.Persona).FirstAsync();
        }
        public async Task<ImagenPerfil> UpdateControlId(ImagenPerfil imagenPerfil)
        {
            _dbContext.Entry(await _dbContext.ImagenPerfil.FirstOrDefaultAsync(x => x.Id == imagenPerfil.Id)).CurrentValues.SetValues(new
            {
                Id = imagenPerfil.Id,
                ControlUserId = imagenPerfil.ControlUserId,
                ControlIdTimestamp = imagenPerfil.ControlIdTimestamp,
                ControlIdImage = imagenPerfil.ControlIdImage
            });
            await _dbContext.SaveChangesAsync();
            return await _dbContext.ImagenPerfil.Where(p => p.Id == imagenPerfil.Id).Include(x => x.Persona).FirstAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var imagenPerfil = await _dbContext.ImagenPerfil.Where(x => x.PersonaId == id).FirstAsync();
            if (imagenPerfil != null)
            {
                _dbContext.ImagenPerfil.Remove(imagenPerfil);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }

}