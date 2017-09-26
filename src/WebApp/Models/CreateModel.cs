using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaginableCollections;

namespace WebApp.Models
{
    public class CreateModel
    {
        public const int page_size = 20;
        public int total_count {get; set;}
        public int page_count {get;set;}
        public string message {get;set;}
        public int selected_option { get; set; }
        public List<SelectListItem> options 
        {
            get{
                return Enumerable.Range(1,100).Select(x => new SelectListItem(){ Text = x.ToString(), Value=  x.ToString(), Selected = (x == 50) }).ToList();
            }
        }  
        
    }
}