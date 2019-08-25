using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        // for new movie
        public MovieFormViewModel()
        {
            Id = 0;
        }

        // for edit movie
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
        }
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }  // string is nullable by default

        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number in Stock")]
        public int? NumberInStock { get; set; }

        public IEnumerable<Genre> GenreList { get; set; }

        public string Title
        {
            get
            {
                return (Id == 0) ? "New Movie" : "Edit Movie";
            }
        }

    }
}