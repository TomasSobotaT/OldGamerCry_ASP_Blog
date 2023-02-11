using OldGamerCry_ASP_Blog.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pokusoblog2.Models
{
    public class Article
    {
       
        public int Id { get; set; }

        [Display(Name ="Title")]
        public string Title { get; set; } = "";

        [Display(Name = "Description")]
        public string Description { get; set; } = "";

        [Display(Name = "Content")]
        public string Content{ get; set; } = "";

        [Display(Name = "Published")]
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
