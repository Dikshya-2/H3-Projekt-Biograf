using Biograf.Repo.DTOs;
using Biograf.Repo.Interface;
using Biograf.Repo.Models;
using Biograf.Repo.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf.Repo.Repositories
{
    public class PhotoRepo : IPhotoRepo
    {
        private readonly DatabaseContext _context;

        public PhotoRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<Photo> Create(Photo newPhoto)
        {
        //    Photo photo = new Photo
        //    {
        //        //Id = newPhoto.Id,
        //        //Image = newPhoto.Image,
        //        //MovieId = newPhoto.MovieId,
        //    };
            _context.Photos.Add(newPhoto);
            _context.SaveChanges();
            return newPhoto;
        }
        public async Task<Photo?> GetById(int id) 
        {
            return await _context.Photos.FirstOrDefaultAsync(p=>p.Id == id);
        }
        public async Task<List<Photo>> GetAll()
        {
            return await _context.Photos.ToListAsync();
        }

        public async Task<Photo> Delete(int id)
        {
            var obj = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<Photo> Update(int id, Photo photo)
        {
            var obj = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            obj.Image = photo.Image;
            obj.MovieId = photo.MovieId;
            await _context.SaveChangesAsync();
            return obj;
        }

    }
}
