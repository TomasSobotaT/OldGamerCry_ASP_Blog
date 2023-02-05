﻿using OldGamerCry_ASP_Blog.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace pokusoblog2.Models
{
    public class Article
    {
       
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content{ get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [NotMapped]
        public byte[] ImageByte { get; set; } = { 1, 2, 3 };
        
        public string ImagePath { get; set; } = "";

       

        /// <summary>
        /// save image from byte[] to *.jpg, save to it images folder and save path 
        /// </summary>
        public void SaveImageAndMapPath() 
        {
            File.WriteAllBytes(@$"wwwroot\images\{Title}-{CreatedDate.ToString("yyyy.MM.dd")}.jpg", ImageByte);
            ImagePath = @$"images\{Title}-{CreatedDate.ToString("yyyy.MM.dd")}.jpg";
        }


    }
}
